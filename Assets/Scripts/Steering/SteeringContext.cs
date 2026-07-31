using UnityEngine;
using UnityEngine.Splines;

public class SteeringContext
{
    public Transform Target;

    public SplineContainer Path;

    public Vector3 CharacterVelocity;

    public float MaxSpeed;

    public GameObject Character;

    public Vector3 CharacterPosition => Character.transform.position;

}