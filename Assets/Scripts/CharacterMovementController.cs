using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Splines;
using BehaviourTrees;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private Transform currentTarget;
    [SerializeField] private SplineContainer currentPath;

    private SteeringContext context;
    private CharacterMovement movement;
    private PathFollowing followSpline;
    private Seek seekTarget;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        context = new SteeringContext();
        movement = GetComponent<CharacterMovement>();
        context.Character = this.gameObject;
        movement.SetInitialVelocity(new Vector3(5, 0, 0));

        followSpline = new PathFollowing(context);
        seekTarget = new Seek(context);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        UpdateContext();
        Vector3 acceleration = followSpline.CalculateAcceleration();
        Debug.Log($"Acceleration: {acceleration}");
        Debug.Log($"Before: {movement.Velocity}");
        movement.ApplyAcceleration(acceleration);
        Debug.Log($"After: {movement.Velocity}");
        
    }
    public void UpdateContext()
    {
        context.MaxSpeed = movement.MaxSpeed;
        context.CharacterVelocity = movement.Velocity;

        context.Target = currentTarget;
        context.Path = currentPath;
    }
}
