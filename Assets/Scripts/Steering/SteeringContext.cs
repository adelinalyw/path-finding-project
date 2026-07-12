using UnityEngine;
using UnityEngine.Splines;

public class SteeringContext
{

    // Optional target
    public Transform Target;

    // Optional path
    public Spline Path;

    public Vector3 CharacterVelocity;

    public Vector3 CharacterPosition;

    public float MaxSpeed;

}