using UnityEngine;

public abstract class SteeringBehaviour
{

    public abstract Vector3 CalculateAcceleration(
        SteeringContext context);
}
