using System.Collections.Generic;
using Aima.AgentSystems;
using UnityEngine;

namespace Assets.Scripts
{
    public class CrewWaypoint : MonoBehaviour, IState
    {
        [SerializeField]
        public List<CrewWaypoint> Connections;

        private MachineSlot _machineSlot;

        void Start ()
        {
            _machineSlot = GetComponent<MachineSlot>();
        }
	
        void Update ()
        {
		
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(transform.position, new Vector3(0.1f, 0.1f, 0.1f));
            foreach (var crewWaypoint in Connections)
            {
                if(crewWaypoint != null)
                    Gizmos.DrawLine(transform.position, crewWaypoint.transform.position);
            }
        }
    }
}
