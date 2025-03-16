using UnityEngine;
using CosmicHorrorJam.Util;

public class Player : MonoBehaviour
{
    
    [SerializeField]
    private float speed;
    
    [SerializeField]
    private Dialogue dialogue;
    [SerializeField]
    private GameObject sprite;
    [SerializeField]
    private Stars stars;
    [SerializeField]
    private Braid braid;
    [SerializeField]
    private IfritArm ifritArm;
    
    private PlayerInputActions inputActions;

    private PlayerParent playerParent;
    private GameObject dummyTopScale;
    private GameObject dummyBottomScale;
    
    private Rigidbody2D rb;
    
    private DirectionFaced directionFaced;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        playerParent = transform.parent.GetComponent<PlayerParent>();
        dummyTopScale = playerParent.dummyTopScale;
        dummyBottomScale = playerParent.dummyBottomScale;

        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();

        AdjustPerspective();

        FaceDirection(playerParent.startDirectionFaced);
    }

    // Update is called once per frame
    private void Update()
    {
        Vector2 inputVector = inputActions.Player.Move.ReadValue<Vector2>();
        
        if (inputVector == Vector2.zero) return;

        Move(inputVector);

        if (inputVector.x > 0 && directionFaced != DirectionFaced.Right)
        {
            FaceDirection(DirectionFaced.Right);
        } else if (inputVector.x < 0 && directionFaced != DirectionFaced.Left)
        {
            FaceDirection(DirectionFaced.Left);
        }
    }
    
    private void Move(Vector2 inputVector)
    {
        float currentSpeed = speed * Time.deltaTime;
        
        rb.AddForce(inputVector * currentSpeed);
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
    
    public void FaceDirection(DirectionFaced directionFaced)
    {
        this.directionFaced = directionFaced;
        ifritArm.SetDirection(directionFaced);
        
        switch (directionFaced)
        {
            case DirectionFaced.Left:
                sprite.transform.rotation = Quaternion.Euler(0, 180, 0);
                break;
            case DirectionFaced.Right:
                sprite.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
        }
    }
}
