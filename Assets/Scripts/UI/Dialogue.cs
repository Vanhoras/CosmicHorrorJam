using UnityEngine;

public class Dialogue: MonoBehaviour
{

    [SerializeField] private TMPro.TextMeshProUGUI textField;

    private string text = "";
    private Color color;
    
    private float fadeoutDuration = 0.5f;
    private float timer;

    private bool showText;
    
    private void Start()
    {
        textField.text = text;
    }

    private void Update()
    {
        if (!showText) return;
        
        timer -= Time.deltaTime;
        
        if (timer < fadeoutDuration)
        {
            textField.color = new Color(textField.color.r, textField.color.g, textField.color.b, timer / fadeoutDuration);
        }
        
        if (timer <= 0)
        {
            HideText();
        }
    }

    public void SetColor(Color color)
    {
        textField.color = color;
        this.color = color;
    }
    
    public void SetText(string text, float duration)
    {
        this.text = text;
        textField.text = text;
        timer = duration;
        showText = true;
    }
    
    private void HideText()
    {
        textField.text = "";
        textField.color = color;
        timer = 0;
        showText = false;
    }
}
