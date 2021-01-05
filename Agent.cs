using System;
using System.Collections.Generic;
using System.Numerics;

namespace PGB
{
    public abstract class Agent
    {
        Vector3 _currentPosition;
        Single _speed;

        public Vector3 Position => _currentPosition;

        protected Agent(PGB_Vector3 initialPosition, Single speed) 
        {
            _currentPosition = Library.ToVector3(initialPosition);
            _speed = speed;
        }

        public abstract void MoveStep(PGB_Vector3 nextPosition);

        public abstract void MoveToGoal();
    }

    public class PGB_Agent : Agent
    {
        Vector3 _currentPosition;
        Single _speed;

        Queue<PGB_Vector3> _movePlan;

        public PGB_Agent(PGB_Vector3 initialPosition, Single speed) : base(initialPosition, speed) 
        { }

        // step by interval
        public override void MoveStep(PGB_Vector3 nextPosition)
        {
            var curr = _currentPosition;
            var next = Library.ToVector3(nextPosition);

            curr -= next;

            var ceta = MathF.Atan2(curr.Y, curr.X);

            var dx = MathF.Cos(ceta) * _speed;
            var dy = MathF.Sin(ceta) * _speed;

            while(curr.Equals(next))
            {
                curr.X += dx;
                curr.Y += dy;
            }

            _currentPosition = curr;
        }

        public override void MoveToGoal()
        {
            while (_movePlan.Count > 0)
            {
                var goal = _movePlan.Dequeue();
                // call by time interval
                MoveStep(goal);
            }
        }
    }
}
