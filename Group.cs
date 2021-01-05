using System;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;

namespace PGB
{
    using PGB_Range = Tuple<Single, Single, Single, Single>;

    class Group
    {
        Int32 _numAgent;
        List<Agent> _agents = new List<Agent>();
        Vector3 _goalPosition;

        public Group(Int32 NumAgent, PGB_Range Range, Func<Single, Single, Single, Agent> DelSpawnAgent)
        {
            var (x, y, width, height) = Range;

            _numAgent = NumAgent;
            for(var idx = 0; idx < _numAgent; idx++)
            {
                var X = x + width - Library.NextSingle() * width * 0.5f;
                var Y = y + height - Library.NextSingle() * height * 0.5f;
                var Z = 0.0f;

                var agent = DelSpawnAgent(X, Y, Z);

                _agents.Add(agent);
            }
        }

        public void Move(PGB_Vector3 NextPosition)
        {
            _goalPosition = Library.ToVector3(NextPosition);

            while(true)
            {
                var leader = SelectLeader();
            }
        }

        public Agent SelectLeader()
        {
            return _agents.MinBy<Agent, Single>(Agent => Vector3.Distance(Agent.Position, _goalPosition));
        }
    }
}