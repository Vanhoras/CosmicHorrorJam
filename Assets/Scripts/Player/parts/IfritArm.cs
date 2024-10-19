using UnityEngine;
using UnityEngine.InputSystem;
using CosmicHorrorJam.Util;

public class IfritArm: MonoBehaviour
{
    
    [SerializeField]
    private float minAngle;
    
    [SerializeField]
    private float maxAngle;
    
    [SerializeField]
    private float cooldownDuration;
    private bool inCooldown;
    private float cooldownTimer;
    
    [SerializeField]
    private Animator animator;
    
    [SerializeField]
    private Fireball fireball;
    
    [SerializeField]
    private GameObject spawnPoint;
    
    private PlayerInputActions inputActions;
    private static readonly int ShootTrigger = Animator.StringToHash("shoot");
    
    private DirectionFaced directionFaced;

    private void Start()
    {
        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();
        inputActions.Player.Click.performed += OnMouseClick;
    }

    private void Update()
    {
        if (inCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0)
            {
                inCooldown = false;
            }
        }
        
        Vector2 inputVector = inputActions.Player.Position.ReadValue<Vector2>();
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(inputVector);

        Vector2 direction = CalculateDirection(mousePosition);
        float angle = CalculateLocalAngle(direction);
        
        transform.eulerAngles = new (transform.eulerAngles.x, transform.eulerAngles.y, angle);
    }

    private void OnDestroy()
    {
        inputActions.Player.Click.performed -= OnMouseClick;
    }
    
    private void OnMouseClick(InputAction.CallbackContext input)
    {
        if (inCooldown) return;
        
        Vector2 inputVector = inputActions.Player.Position.ReadValue<Vector2>();
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(inputVector);
        
        Vector2 direction = CalculateDirection(mousePosition);
        
        Shoot(direction);
    }

    public void Shoot(Vector2 direction)
    {
        animator.SetTrigger(ShootTrigger);
        inCooldown = true;
        cooldownTimer = cooldownDuration;

        float angle = CalculateGlobalAngle(direction);
        
        float radians = angle * Mathf.Deg2Rad;
        Vector2 clampedDirection = new (Mathf.Cos(radians), Mathf.Sin(radians));
        
        Fireball fireball = Instantiate(this.fireball, spawnPoint.transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
        fireball.SetDirection(clampedDirection);
    }
    
    public void SetDirection(DirectionFaced directionFaced)
    {
        this.directionFaced = directionFaced;
    }
    
    private Vector2 CalculateDirection(Vector2 mousePosition)
    {
        Vector2 direction = Vector2.zero;
        
        switch (directionFaced)
        {
            case DirectionFaced.Left:
                direction = ((Vector2) transform.position - mousePosition).normalized;
                break;
            case DirectionFaced.Right:
                direction = (mousePosition - (Vector2) transform.position).normalized;
                break;
        }

        return direction;
    }
    
    private float CalculateAngle(Vector2 direction)
    {
        return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }
    
    private float CalculateLocalAngle(Vector2 direction)
    {
        float angle = CalculateAngle(direction);
        
        angle = directionFaced == DirectionFaced.Left ? angle * -1 : angle;
        
        angle = Mathf.Clamp(angle, minAngle, maxAngle);
        
        return angle;
    }
    
    private float CalculateGlobalAngle(Vector2 direction)
    {
        float angle = CalculateAngle(direction);
        
        angle = Mathf.Clamp(angle, minAngle, maxAngle);
        
        if (DirectionFaced.Left == directionFaced)
        {
            angle += 180;
        }

        return angle;
    }
}