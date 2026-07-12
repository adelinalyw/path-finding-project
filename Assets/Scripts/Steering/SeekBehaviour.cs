using UnityEngine;

public class SeekBehaviour : SteeringBehaviour
{
    public override Vector3 CalculateAcceleration(
        SteeringContext context)
    {
        if (context.Target == null)
            return Vector3.zero;

        Vector3 desiredVelocity =
            (context.Target.position - context.CharacterPosition).normalized
            * context.MaxSpeed;

        Vector3 desiredAcceleration =
            (desiredVelocity - context.CharacterVelocity);

        return desiredAcceleration;
    }
}
