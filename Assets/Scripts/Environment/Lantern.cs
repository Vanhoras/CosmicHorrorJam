using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Lantern : MonoBehaviour
{
    
    [SerializeField]
    private int maxLightStage;
    
    [SerializeField]
    private GameObject lanternUnlit;
    
    [SerializeField]
    private GameObject lanternLit;
    
    [SerializeField]
    private Light2D lanternLight;
    
    private bool lit;

    private int lightStage;
    
    private void Start()
    {
        lightStage = maxLightStage;
    }
    
    public void Light()
    {
        lit = true;
        lanternUnlit.SetActive(false);
        lanternLit.SetActive(true);
        lightStage = maxLightStage;
    }
}
