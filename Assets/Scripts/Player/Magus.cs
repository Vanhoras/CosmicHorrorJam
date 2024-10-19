using UnityEngine;
using CosmicHorrorJam.Util;

public class Magus: MonoBehaviour
{
    
    [SerializeField] public DirectionFaced startDirectionFaced = DirectionFaced.Right;
    
    [SerializeField]
    private Dialogue dialogue;
    
    [SerializeField]
    private GameObject spriteFront;
    [SerializeField] 
    private GameObject spriteBack;
    
    private DirectionFaced directionFaced;
    
    private bool faceFront = true;

    private void Start()
    {
        spriteFront.SetActive(faceFront);
        spriteBack.SetActive(!faceFront);
        
        FaceDirection(startDirectionFaced);
    }
    
    public void Flip(bool faceFront)
    {
        this.faceFront = faceFront;
        spriteFront.SetActive(faceFront);
        spriteBack.SetActive(!faceFront);
    }
    
    public void FaceDirection(DirectionFaced directionFaced)
    {
        this.directionFaced = directionFaced;
        
        switch (directionFaced)
        {
            case DirectionFaced.Left:
                spriteFront.transform.rotation = Quaternion.Euler(0, 180, 0);
                spriteBack.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case DirectionFaced.Right:
                spriteFront.transform.rotation = Quaternion.Euler(0, 0, 0);
                spriteBack.transform.rotation = Quaternion.Euler(0, 180, 0);
                break;
        }
    }
}
