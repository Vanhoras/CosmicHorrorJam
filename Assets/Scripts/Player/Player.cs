using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerInputActions inputActions;

    private PlayerParent playerParent;
    private GameObject dummyTopScale;
    private GameObject dummyBottomScale;

    private void Start()
    {
        playerParent = transform.parent.GetComponent<PlayerParent>();
        dummyTopScale = playerParent.dummyTopScale;
        dummyBottomScale = playerParent.dummyBottomScale;

        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();
        inputActions.Player.Click.performed += OnMouseClick;

        AdjustPerspective();

    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void OnMouseClick(InputAction.CallbackContext input)
    {
        Vector2 inputVector = inputActions.Player.Position.ReadValue<Vector2>();
    }
    

    private void AdjustPerspective()
    {
        Vector3 bottomScale = dummyBottomScale.transform.localScale;
        Vector3 topScale = dummyTopScale.transform.localScale;

        float postionTop = dummyTopScale.transform.position.y;
        float postionBottom = dummyBottomScale.transform.position.y;
        float percentY = (transform.position.y - postionBottom) / (postionTop - postionBottom);

        transform.localScale = Vector3.Lerp(topScale, bottomScale, 1 - percentY);
    }

    private void ChangeDirection(bool left)
    {
        transform.rotation = left ? transform.rotation = Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
    }

    private void OnDestroy()
    {
        inputActions.Player.Click.performed -= OnMouseClick;
    }
}
