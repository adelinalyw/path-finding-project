using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Splines;

public class SteeringController : MonoBehaviour
{
    [SerializeField] private Transform currentTarget;
    [SerializeField] private SplineContainer currentPath;

    private SteeringContext context;
    private CharacterMovement movement;
    
    private readonly List<SteeringBehaviour> behaviours =
        new List<SteeringBehaviour>();

    private void Start()
    {
        behaviours.Add(new SeekBehaviour());
        context = new SteeringContext();
        movement = GetComponent<CharacterMovement>();
        context.Character = this.gameObject;
    }

    private void FixedUpdate()
    {
        UpdateContext();
        Vector3 acceleration = CalculateAcceleration();
        movement.ApplyAcceleration(acceleration);
        
    }

    private Vector3 CalculateAcceleration()
    {
        Vector3 totalAcceleration = Vector3.zero;

        foreach (SteeringBehaviour behaviour in behaviours)
        {
            totalAcceleration +=
                behaviour.CalculateAcceleration(context);
        }

        return totalAcceleration;
    }

    public void UpdateContext()
    {
        context.MaxSpeed = movement.MaxSpeed;
        context.CharacterVelocity = movement.Velocity;

        context.Target = currentTarget;
        context.Path = currentPath;
    }
}
