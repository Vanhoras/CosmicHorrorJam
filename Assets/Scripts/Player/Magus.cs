using UnityEngine;
using CosmicHorrorJam.Util;

public class Magus: MonoBehaviour
{
    
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
                spriteFront.transform.localScale = new Vector3(spriteFront.transform.localScale.x, 180, spriteFront.transform.localScale.z);
                spriteBack.transform.localScale = new Vector3(spriteFront.transform.localScale.x, 0, spriteFront.transform.localScale.z);
                break;
            case DirectionFaced.Right:
                spriteFront.transform.localScale = new Vector3(spriteFront.transform.localScale.x, 0, spriteFront.transform.localScale.z);
                spriteBack.transform.localScale = new Vector3(spriteFront.transform.localScale.x, 180, spriteFront.transform.localScale.z);
                break;
        }
    }
}
