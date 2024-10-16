using UnityEngine;
using UnityEngine.InputSystem;

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
        
        Vector2 direction = (mousePosition - (Vector2) transform.position).normalized;
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        angle = Mathf.Clamp(angle, minAngle, maxAngle);
        
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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
        
        // turn inputAction into a direction vector
        Vector2 direction = (mousePosition - (Vector2) transform.position).normalized;
        
        Shoot(direction);
    }

    public void Shoot(Vector2 direction)
    {
        animator.SetTrigger(ShootTrigger);
        inCooldown = true;
        cooldownTimer = cooldownDuration;
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        angle = Mathf.Clamp(angle, minAngle, maxAngle);
        
        float radians = angle * Mathf.Deg2Rad;
        Vector2 clampedDirection = new (Mathf.Cos(radians), Mathf.Sin(radians));
        
        Fireball fireball = Instantiate(this.fireball, spawnPoint.transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
        fireball.SetDirection(clampedDirection);
    }
    
    
}