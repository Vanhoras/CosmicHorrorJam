using UnityEngine;
using CosmicHorrorJam.Util;

public class Ghost : MonoBehaviour
{
    
    [SerializeField] public DirectionFaced startDirectionFaced = DirectionFaced.Left;
    
    [SerializeField]
    private GhostDeathAnimation deathAnimation;

    [SerializeField] private GameObject sprite;
    
    [SerializeField]
    private Dialogue dialogue;

    private bool dead;
    
    private DirectionFaced directionFaced;
    
    private void Start()
    {
        FaceDirection(startDirectionFaced);
    }

    private void Update()
    {
        if (dead) return;
        
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Attack") && !dead)
        {
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
                sprite.transform.rotation = Quaternion.Euler(0, 180, 0);
                deathAnimation.transform.rotation = Quaternion.Euler(0, 180, 0);
                break;
            case DirectionFaced.Right:
                sprite.transform.rotation = Quaternion.Euler(0, 0, 0);
                deathAnimation.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
        }
    }
}
