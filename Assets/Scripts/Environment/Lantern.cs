using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Lantern : MonoBehaviour
{
    [SerializeField]
    private bool lit;
    
    [SerializeField]
    public LightStage[] lightStages;
    
    [SerializeField]
    private GameObject lanternUnlit;
    
    [SerializeField]
    private GameObject lanternLit;
    
    [SerializeField]
    private Light2D light2D;
    [SerializeField]
    private Light2D backgroundLight2D;

    private LightStage currentLightStage;
    private LightStage previousLightStage;
    
    [SerializeField]
    private float transitionTime;
    private float transitionTimer;
    private bool transitioning;
    
    [SerializeField]
    private float flickerStages;
    private bool flicker;
    
    private void Start()
    {
        currentLightStage = lightStages[0];
        
        if (lit)
        {
            LightWithoutTransition();
        }
        else
        {
            ExtinquishWithoutTransition();
        }
        
        AdjustLighting(currentLightStage);
    }
    
    private void Update()
    {
        if (!lit) return;
        
        if (transitioning)
        {
            transitionTimer += Time.deltaTime;
            
            LightStage transitionLightStage = new LightStage(currentLightStage, previousLightStage, transitionTimer / transitionTime);
            
            AdjustLighting(transitionLightStage);
            
            if (transitionTimer >= transitionTime)
            {
                transitioning = false;
                transitionTimer = 0;
            }
        }
    }
    
    private void LightWithoutTransition()
    {
        lit = true;
        lanternUnlit.SetActive(false);
        lanternLit.SetActive(true);
        
        previousLightStage = currentLightStage;
        currentLightStage = lightStages[1];
    }
    
    public void Light()
    {
        LightWithoutTransition();

        StartTransition();
    }
    
    private void ExtinquishWithoutTransition()
    {
        lit = false;
        lanternUnlit.SetActive(true);
        lanternLit.SetActive(false);
        
        previousLightStage = currentLightStage;
        currentLightStage = lightStages[0];
    }
    
    public void Extinguish()
    {
        ExtinquishWithoutTransition();

        StartTransition();
    }
    
    public void StartTransition()
    {
        transitioning = true;
        transitionTimer = 0;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Attack") && !lit)
        {
            Light();
        }
    }

    private void AdjustLighting(LightStage lightStage)
    {
        light2D.intensity = lightStage.intensity;
        backgroundLight2D.intensity = lightStage.backgroundIntensity;
        light2D.pointLightInnerRadius = lightStage.innerRange;
        backgroundLight2D.pointLightInnerRadius = lightStage.innerRange;
        light2D.pointLightOuterRadius = lightStage.outerRange;
        backgroundLight2D.pointLightOuterRadius = lightStage.outerRange;
    }
}

[System.Serializable]
public class LightStage
{
    public float intensity;
    public float backgroundIntensity;
    public float innerRange;
    public float outerRange;
    public float falloff;
    
    public LightStage(float intensity, float backgroundIntensity, float innerRange, float outerRange, float falloff)
    {
        this.intensity = intensity;
        this.backgroundIntensity = backgroundIntensity;
        this.innerRange = innerRange;
        this.outerRange = outerRange;
        this.falloff = falloff;
    }
    
    public LightStage(LightStage currentLightStage, LightStage previousLightStage, float position)
    {
        intensity = Mathf.Lerp(previousLightStage.intensity, currentLightStage.intensity, position);
        backgroundIntensity = Mathf.Lerp(previousLightStage.backgroundIntensity, currentLightStage.backgroundIntensity, position);
        innerRange = Mathf.Lerp(previousLightStage.innerRange, currentLightStage.innerRange, position);
        outerRange = Mathf.Lerp(previousLightStage.outerRange, currentLightStage.outerRange, position);
        falloff = Mathf.Lerp(previousLightStage.falloff, currentLightStage.falloff, position);
    }
}