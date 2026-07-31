using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Rigidbody rb; 
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float maxAcceleration = 1f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public Vector3 Velocity => rb.linearVelocity;

    public Vector3 Position => this.transform.position;

    public float MaxSpeed => maxSpeed;

    public float MaxAcceleration => maxAcceleration;

    public void ApplyAcceleration(Vector3 acceleration)
    {
        rb.linearVelocity += acceleration.normalized * maxAcceleration * Time.fixedDeltaTime;
        rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
            // Vector3.ClampMagnitude(
            //     rb.linearVelocity,
            //     maxSpeed);
    }

    public void SetInitialVelocity (Vector3 velocity)
    {
        if (rb == null)
        rb = GetComponent<Rigidbody>();
        
        rb.linearVelocity = velocity;
    }

}
