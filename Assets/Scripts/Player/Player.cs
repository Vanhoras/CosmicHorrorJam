using UnityEngine;
using UnityEngine.InputSystem;
using CosmicHorrorJam.Util;

public class Player : MonoBehaviour
{
    
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
    
    private DirectionFaced directionFaced;

    private void Start()
    {
        playerParent = transform.parent.GetComponent<PlayerParent>();
        dummyTopScale = playerParent.dummyTopScale;
        dummyBottomScale = playerParent.dummyBottomScale;

        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();

        AdjustPerspective();

    }

    // Update is called once per frame
    private void Update()
    {
        
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

    private void OnDestroy()
    {
        
    }
    
    public void FaceDirection(DirectionFaced directionFaced)
    {
        this.directionFaced = directionFaced;
        
        switch (directionFaced)
        {
            case DirectionFaced.Left:
                sprite.transform.localScale = new Vector3(sprite.transform.localScale.x, 180, sprite.transform.localScale.z);
                break;
            case DirectionFaced.Right:
                sprite.transform.localScale = new Vector3(sprite.transform.localScale.x, 0, sprite.transform.localScale.z);
                break;
        }
    }
}
