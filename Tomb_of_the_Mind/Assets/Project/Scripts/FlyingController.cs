
using UnityEngine;
public class BeeFlyingAround : MonoBehaviour
{
    public Transform user;        
    public float radius = 2.0f;    
    public float speed = 2.0f;     
    public float height = 1.0f;    
    public float waveAmplitude= 1.0f;
    public float waveFrequency= 1.0f;
    private float angle = 0.0f;    

    void Start()
    {
        if (user == null)
        {
            Debug.LogError("User transform not assigned in BeeFlyingAround script.");
        }
    }

    void Update()
    {
        if (user != null)
        {
            
            angle -= speed * Time.deltaTime;

            
            float x = user.position.x + Mathf.Cos(angle) * radius;
            float z = user.position.z + Mathf.Sin(angle) * radius;
            float y = user.position.y + height + Mathf.Sin(Time.time * waveFrequency) * waveAmplitude;

            
            transform.position = new Vector3(x, y, z);

            
            transform.LookAt(user);
        }
    }
}
