using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class CrewMember : MonoBehaviour
    {
        enum State
        {
            Idle, 
            Moving
        }

        private State _state = State.Idle;
        private CrewWaypoint[] _currentRoute;
        private float _progress = 0f;
        private CrewWaypoint _currentWaypoint;
        private CrewWaypoint _targetWaypoint;
        private int _targetWaypointIndex = 0;

        void Start ()
        {
        }
        
        void Update ()
        {
            if (_state == State.Idle)
            {
                var a = CrewManager.Instance.GetRandomWaypoint();
                var b = CrewManager.Instance.GetRandomWaypoint();
                var route = CrewManager.Instance.FindRoute(a, b);
                if (route != null)
                {
                    _currentRoute = route.ToArray();

                    if (_currentRoute.Length > 0)
                    {
                        _currentWaypoint = a;
                        _targetWaypoint = _currentRoute[0];
                        _targetWaypointIndex = 0;
                        _state = State.Moving;
                        _progress = 0f;
                    }
                }
            }

            if (_state == State.Moving)
            {
                _progress += Time.deltaTime*0.5f;
                transform.position = Vector3.Lerp(_currentWaypoint.transform.position,
                    _targetWaypoint.transform.position, _progress);

                if (_progress > 1f)
                {
                    _progress = 0f;
                    _targetWaypointIndex += 1;
                    if (_targetWaypointIndex > _currentRoute.Length - 1)
                    {
                        _state = State.Idle;
                    }
                    else
                    {
                        _currentWaypoint = _targetWaypoint;
                        _targetWaypoint = _currentRoute[_targetWaypointIndex];
                    }
                }
            }
        }
    }
}
