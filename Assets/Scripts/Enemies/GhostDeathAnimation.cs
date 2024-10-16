using UnityEngine;

public class GhostDeathAnimation: MonoBehaviour
{

    [SerializeField]
    private GameObject[] ghostParts;
    
    private Animator animator;
    
    private static readonly int DeathTrigger = Animator.StringToHash("Death");
    
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        
        foreach (var ghostPart in ghostParts)
        {
            ghostPart.SetActive(false);
        }
    }
    
    public void PlayDeath()
    {
        Debug.Log("PlayDeath");
        animator.SetTrigger(DeathTrigger);
    }
}