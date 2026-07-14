using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Rigidbody rb; 
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float maxAcceleration = 5f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public Vector3 Velocity => rb.linearVelocity;

    public Vector3 Position => this.transform.position;

    public float MaxSpeed => maxSpeed;

    public float MaxAcceleration => maxAcceleration;

    public void ApplyAcceleration(Vector3 acceleration)
    {
        rb.linearVelocity += acceleration * Time.fixedDeltaTime;

        rb.linearVelocity =
            Vector3.ClampMagnitude(
                rb.linearVelocity,
                maxSpeed);
    }

}
