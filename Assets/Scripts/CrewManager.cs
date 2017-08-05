using System;
using System.Collections.Generic;
using System.Linq;
using Aima.AgentSystems;
using Aima.Search;
using Aima.Search.Methods;
using Aima.Utilities;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts
{
    public class CrewManager : Singleton<CrewManager>
    {
        private List<CrewWaypoint> _crewWaypoints;
        
        void Start ()
        {
            _crewWaypoints = FindObjectsOfType<CrewWaypoint>().ToList();
        }
        
        void Update ()
        {
		
        }

        void OnDrawGizmos()
        {
            
        }

        public CrewWaypoint GetRandomWaypoint()
        {
            return _crewWaypoints[Mathf.FloorToInt(UnityEngine.Random.value*_crewWaypoints.Count)];
        }

        public IEnumerable<CrewWaypoint> FindRoute(CrewWaypoint from, CrewWaypoint to)
        {
            Debug.LogFormat("Finding route from {0} to {1}", from, to);

            var problem = new PathFindingProblem(from, to);
            //var search = new AStarSearch<CrewWaypoint>(waypoint => problem.DistanceToTarget(waypoint));
            var search = new BroadSearch<CrewWaypoint>();
            var solution = search.Search(problem);
            if (solution != null)
            {
                Debug.Log("Route found");
                return solution.States;
            }

            Debug.Log("Route NOT found");
            return null;
        }

        class PathFindingProblem : IProblem<CrewWaypoint>
        {
            private readonly CrewWaypoint _target;

            public PathFindingProblem(CrewWaypoint state, CrewWaypoint target)
            {
                InitialState = state;
                _target = target;
            }

            public IEnumerable<Tuple<IAction, CrewWaypoint>> SuccessorFn(CrewWaypoint state)
            {
                var action = new SimpleAction("Move");
                return state.Connections.Select(waypoint => new Tuple<IAction, CrewWaypoint>(action, waypoint));
            }

            public bool GoalTest(CrewWaypoint state)
            {
                return state.Equals(_target);
            }

            public double Cost(IAction action, CrewWaypoint @from, CrewWaypoint to)
            {
                return (to.transform.position - from.transform.position).magnitude;
            }

            public CrewWaypoint InitialState { get; private set; }

            public double DistanceToTarget(CrewWaypoint state)
            {
                return (_target.transform.position - state.transform.position).magnitude;
            }
        }
    }
}
