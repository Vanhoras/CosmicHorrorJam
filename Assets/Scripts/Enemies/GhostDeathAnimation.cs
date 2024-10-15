using UnityEngine;

public class GhostDeathAnimation: MonoBehaviour
{

    [SerializeField]
    private GameObject[] ghostParts;
    
    
    private void Start()
    {
        foreach (var ghostPart in ghostParts)
        {
            ghostPart.SetActive(false);
        }
    }

    private void Update()
    {
        
    }
    
    public void Play()
    {
        foreach (var ghostPart in ghostParts)
        {
            ghostPart.SetActive(true);
        }
    }
}