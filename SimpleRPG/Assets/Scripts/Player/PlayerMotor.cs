using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{
    NavMeshAgent agent;     // Variable que usaremos para almacenar el contenido del NavMeshAgent

	// Use this for initialization
	void Start ()
    {
        // Guardamos toda la información del NavMeshAgent en la variable agent
        agent = GetComponent<NavMeshAgent>();
	}

    /// <summary>
    /// Variable pública encargada de mover el jugador por el NavMesh
    /// La llamamos desde el PlayerController asignandole un Vector3 de destino
    /// </summary>
    /// <param name="point"></param>
    public void MoveToPoint(Vector3 point)
    {
        // Le decimos al NavMesh que mueva al objeto que tenga este script a la posicion de destino
        agent.SetDestination(point);
    }
}
