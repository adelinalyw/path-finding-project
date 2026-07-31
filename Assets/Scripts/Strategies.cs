using UnityEngine;
using Unity.Mathematics;
using UnityEngine.Splines;

namespace BehaviourTrees {
     public interface IStrategy
    {
        Node.Status Process();
        void Reset(){}
    }

    public class Seek: IStrategy
    {
        SteeringContext context;

        public Seek (SteeringContext context)
        {
            this.context = context;
        }

        public Vector3 CalculateAcceleration()
        {
            if (context.Target == null)
                return Vector3.zero;

            Vector3 desiredVelocity =
                (context.Target.position - context.CharacterPosition).normalized
                * context.MaxSpeed;

            Vector3 desiredAcceleration = desiredVelocity - context.CharacterVelocity;

            return desiredAcceleration;
        }

        public Vector3 CalculateAcceleration(Vector3 target)
        {
            Vector3 desiredVelocity =
                (target - context.CharacterPosition).normalized
                * context.MaxSpeed;

            Vector3 desiredAcceleration = desiredVelocity - context.CharacterVelocity;

            return desiredAcceleration;
        }

        public Node.Status Process(){
            return Node.Status.Success;
        }

    }

    public class PathFollowing : IStrategy
    {
        SteeringContext context;

        public PathFollowing (SteeringContext context)
        {
            this.context = context;
        }

        private Vector3 NearestPointOnSpline(Vector3 worldPosition, out float t)
        {
            float3 localPoint = context.Path.transform.InverseTransformPoint(worldPosition);

            SplineUtility.GetNearestPoint(
                context.Path.Spline,
                localPoint,
                out float3 nearestLocal,
                out t
            );

            Vector3 nearestWorld = context.Path.transform.TransformPoint(nearestLocal);
            return nearestWorld;
        }

        public Vector3 CalculateAcceleration()
        {
            Vector3 futurePosition = context.CharacterPosition + context.CharacterVelocity*Time.fixedDeltaTime;
            Vector3 nearestPointOnSpline = NearestPointOnSpline(futurePosition, out float t);
            float distance = Vector3.Distance(futurePosition, nearestPointOnSpline);

            Debug.Log($"Distance to spline: {distance}");

            Seek seek = new Seek(context);  

            // if (distance > 1f)
            // {
            //     return seek.CalculateAcceleration(nearestPointOnSpline);;
            // }

            float lookAhead = 0.03f;

            float targetT = Mathf.Clamp01(t + lookAhead);

            float3 targetLocal =
                context.Path.Spline.EvaluatePosition(targetT);

            Vector3 targetWorld =
                context.Path.transform.TransformPoint(targetLocal);

            Vector3 acceleration = seek.CalculateAcceleration(targetWorld);
            return acceleration;
        }

        public Node.Status Process(){
            return Node.Status.Success;
        }

    }
}