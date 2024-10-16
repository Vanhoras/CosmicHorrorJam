using UnityEngine;

public class Fireball: MonoBehaviour
{
    
    [SerializeField]
    private float speed = 6f;
    
    [SerializeField]
    private float destroyTime = 10f;
    private float timer = 0f;
    
    private Vector2 direction;

    private void Update()
    {
        timer += Time.deltaTime;
        
        transform.position += (Vector3) direction * (speed * Time.deltaTime);
        
        if (timer >= destroyTime)
        {
            Destroy(gameObject);
        }
    }

    public void SetDirection(Vector2 direction)
    {
        this.direction = direction;
    }
}