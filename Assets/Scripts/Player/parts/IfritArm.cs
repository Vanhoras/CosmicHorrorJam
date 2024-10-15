using UnityEngine;
using UnityEngine.InputSystem;

public class IfritArm: MonoBehaviour
{
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
    
    private void OnDestroy()
    {
        inputActions.Player.Click.performed -= OnMouseClick;
    }
    
    private void OnMouseClick(InputAction.CallbackContext input)
    {
        Vector2 inputVector = inputActions.Player.Position.ReadValue<Vector2>();
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(inputVector);
        
        // turn inputAction into a direction vector
        Vector2 direction = (mousePosition - (Vector2) spawnPoint.transform.position);

        direction = direction.normalized;
        
        Shoot(direction);
    }

    public void Shoot(Vector2 direction)
    {
        animator.SetTrigger(ShootTrigger);
        
        Fireball fireball = Instantiate(this.fireball, spawnPoint.transform.position, Quaternion.identity);
        fireball.SetDirection(direction);
    }
    
    
}