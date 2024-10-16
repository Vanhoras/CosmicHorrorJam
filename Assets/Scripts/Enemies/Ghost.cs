using UnityEngine;
using CosmicHorrorJam.Util;

public class Ghost : MonoBehaviour
{
    
    [SerializeField]
    private GhostDeathAnimation deathAnimation;

    [SerializeField] private GameObject sprite;
    
    [SerializeField]
    private Dialogue dialogue;

    private bool dead;
    
    private DirectionFaced directionFaced;
    
    private void Start()
    {
        
    }

    private void Update()
    {
        if (dead) return;
        
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D " + other.tag);
        
        if (other.CompareTag("Attack") && !dead)
        {
            Debug.Log("Tag correct");
            OnHit();
        }
    }

    private void OnHit()
    {
        dead = true;
        deathAnimation.PlayDeath();
        sprite.SetActive(false);
    }
    
    public void FaceDirection(DirectionFaced directionFaced)
    {
        this.directionFaced = directionFaced;
        
        switch (directionFaced)
        {
            case DirectionFaced.Left:
                sprite.transform.localScale = new Vector3(sprite.transform.localScale.x, 180, sprite.transform.localScale.z);
                deathAnimation.transform.localScale = new Vector3(sprite.transform.localScale.x, 180, sprite.transform.localScale.z);
                break;
            case DirectionFaced.Right:
                sprite.transform.localScale = new Vector3(sprite.transform.localScale.x, 0, sprite.transform.localScale.z);
                deathAnimation.transform.localScale = new Vector3(sprite.transform.localScale.x, 180, sprite.transform.localScale.z);
                break;
        }
    }
}
