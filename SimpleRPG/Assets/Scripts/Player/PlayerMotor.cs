using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{
    [Tooltip("Variable que almacena la velocidad a la que giramos cuando miramos hacia un objeto interactivo")]
    public float rotationInteractableSpeed = 5f;

    Transform target;       // Variable donde almacenamos el target hacia donde movernos
    NavMeshAgent agent;     // Variable que usaremos para almacenar el contenido del NavMeshAgent

	// Use this for initialization
	void Start ()
    {
        // Guardamos toda la información del NavMeshAgent en la variable agent
        agent = GetComponent<NavMeshAgent>();
	}

    void Update()
    {
        // Siempre que el target no sea null
        if (target != null)
        {   
            // Nos moveremos en la dirección del target
            agent.SetDestination(target.position);
            // Llamamos a la función que se encarga de rotarnos hacia el target
            FaceTarget();
        }    
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

    /// <summary>
    /// Función que se encarga de pasar el foco del PlayerControler al PlayerMotor
    /// </summary>
    /// <param name="newFocus"></param>
    public void FollowTarget(Interactable newFocus)
    {
        // Modificamos la distancia de frenado del NavMesh al radio del foco del objeto interactivo
        // Pero lo multiplicamos por un numero menor de 1 para reducir un poco este radio y que así entre dentro
        agent.stoppingDistance = newFocus.radius * .5f;
        // Desactivamos la actualizacion de rotacion del NavMesh
        agent.updateRotation = false;
        // Asignamos como target del movimiento el foco dado desde el PlayerController
        target = newFocus.interactionPosition;
    }

    /// <summary>
    /// Función que se encarga de parar el movimiento hacia el foco
    /// </summary>
    public void StopFollowingTarget()
    {
        // Volvemos a asignar el valor por defecto de frenado
        agent.stoppingDistance = 0f;
        // Reactivamos la actualizacion de rotacion del NavMesh
        agent.updateRotation = true;
        // Desasignamos el target de movimiento hacia el foco
        target = null;
    }

    /// <summary>
    /// Función que llamamos en el update cuando tenemos un target para mirar hacia el
    /// </summary>
    void FaceTarget()
    {
        // Declaramos un Vector3 con la direccion hacia el objetivo normalizada
        Vector3 direcction = (target.position - transform.position).normalized;
        // Declaramos los grados necesarios para rotarnos hacia la posicion del target
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direcction.x, 0f, direcction.z));
        // Rotamos el player de forma suavizada hacia el target a una velocidad declarada
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationInteractableSpeed);
    }
}
