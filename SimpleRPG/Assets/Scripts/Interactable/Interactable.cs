using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    [Tooltip("Radio de interacción del objeto")]
    public float radius = 3f;
    [Tooltip("Punto donde se posicionará el jugador para interactuar con el objeto")]
    public Transform interactionPosition;

    private bool isFocus = false;   // Variable que usamos para saber si el objeto esta siendo focuseado o no
    private bool hasInteracted = false;     // Variable que dice si estamos o no interactuando

    Transform player;       // Variable que almacena el Transform del player

    /// <summary>
    /// Método usado para realizar la acción de interactuar, el cual será sobreescrito
    /// </summary>
    public virtual void Interact()
    {
        // Este metodo será sobreescrito
        Debug.Log("Interact with: " + transform.name);
    }

    void Update()
    {
        // Si el objeto esta siendo focuseado y no ha sido interaccionado todavia
        if (isFocus && !hasInteracted)
        {   
            // Calculamos la distancia entre el jugador y el objeto y la almacenamos
            float distance = Vector3.Distance(player.transform.position, interactionPosition.position);
            // Si dicha distancia es menor o igual que el radio de acción
            if (distance <= radius)
            {
                // Sacamos un aviso por consola de que estamos interactuando
                Interact();
                // activamos la interaccion
                hasInteracted = true;
            }
        }    
    }

    /// <summary>
    /// Función llamada para hacer focus sobre el player
    /// </summary>
    /// <param name="playerTransform"></param>
    public void OnFocused(Transform playerTransform)
    {
        // Decimos que esta siendo focuseado
        isFocus = true;
        // Y le pasamos el valor del transform del player a la variable interna
        player = playerTransform;
        // paramos la interaccion
        hasInteracted = false;
    }

    /// <summary>
    /// Función que llamamos cuando dejamos de hacer focus sobre esta objeto
    /// </summary>
    public void OnDefocused()
    {
        // Cambiamos la variable de control a false
        isFocus = false;
        // Desasignamos el Transform del player
        player = null;
        // paramos la interaccion
        hasInteracted = false;
    }

    /// <summary>
    /// Función que dibuja el Gizmo de radio sobre el objeto interactuable
    /// </summary>
    void OnDrawGizmosSelected()
    {

        if (interactionPosition == null)
            interactionPosition = transform;
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionPosition.position, radius);
    }
}
