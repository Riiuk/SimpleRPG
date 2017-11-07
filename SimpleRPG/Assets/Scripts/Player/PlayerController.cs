using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    [Header("Variables de movimiento")]
    [Tooltip("Rango máximo en el cual puede interactuar el jugador")]
    public float hitRange = 100f;
    [Tooltip("Variable para almacenar la 'LayerMask' por la cual puede moverse el jugador")]
    public LayerMask movementMask;

    [Header("Variables de HitMarker")]
    [Tooltip("Variable que almacena el efecto de particulas para cuando hacemos click de movimiento")]
    public GameObject hitEffect;

    [Header("Variables de interactividad")]
    [Tooltip("Aquí almacenaremos el focus actual de nuestra acción")]
    public Interactable focus;
    Camera cam;     // Variable para almacenar la cámara
    PlayerMotor motor;  // variable que almacena el componente PlayerMotor para poder usarlo
    
	// Use this for initialization
	void Start ()
    {
        // Guardamos la información de la cámara principal en la variable cam
        cam = Camera.main;
        // Guardamos la información que contiene PlayerMotor en la variable motor
        motor = GetComponent<PlayerMotor>();
	}
	
	// Update is called once per frame
	void Update () 
    {   
        // Si pulsamos el boton izquierdo del raton
        if (Input.GetMouseButtonDown(0))
        {
            // Creamos un rayo desde la posición de la camara hasta la posición del ratón
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            // Creamos una variable de RaycastHit donde almacenaremos la información 
            // del objeto con el que impacta el rayo
            RaycastHit hit;

            // Si el rayo colisiona, almacenamos la información en la variable hit
            // y procedemos con la interaccion
            if (Physics.Raycast(ray, out hit, hitRange, movementMask))
            {
                // Llamamos a la función que se encarga del movimiento
                // pasandole como Vector3 de dirección el punto donde estamos clicando del mundo
                motor.MoveToPoint(hit.point);
                // Llamamos a la corrutina encargada del efecto de clic
                StartCoroutine(HitEffect(hit.point));
                // Quitamos nuestro focus actual, ya que vamos a movernos.
                RemoveFocus();
            }
        }

        // Si pulsamos el boton derecho del raton
        if (Input.GetMouseButtonDown(1))
        {
            // Creamos un rayo desde la posición de la camara hasta la posición del ratón
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            // Creamos una variable de RaycastHit donde almacenaremos la información 
            // del objeto con el que impacta el rayo
            RaycastHit hit;

            // Si el rayo colisiona dentro del rango, almacena la informacion del objeto en la variable hit
            if (Physics.Raycast(ray, out hit, hitRange))
            {
                // Recuperamos el componente Interactable del objeto colisionado
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                // Si este componente existe
                if (interactable != null)
                {   
                    // Establecemos el focus del player pasando como referencia dicho objeto interactivo
                    SetFocus(interactable);
                }
            }
        }
    }

    /// <summary>
    /// Función que usamos para marcar el focus a donde tiene que dirigirse el player
    /// </summary>
    /// <param name="newFocus"></param>
    void SetFocus(Interactable newFocus)
    {
        // Si el nuevo foco es diferente al foco actual
        if (newFocus != focus)
        {
            // Y si el antiguo foco no estaba vacio
            if (focus != null)
                // Desmarcamos el focus del objeto
                focus.OnDefocused();

            // Asignamos el focus del player al que nos viene dado cuando hacemos clic sobre un objeto interactivo
            focus = newFocus;
            // Llamamos a la función de PlayerMotor encargada de seguir al target asignado
            motor.FollowTarget(newFocus);
        }
        // Pasamos el transform del player al objeto interactivo
        newFocus.OnFocused(transform);
    }

    /// <summary>
    /// Función llamada cuando hacemos clic sobre cualquier objeto que no sea interactivo
    /// </summary>
    void RemoveFocus()
    {
        if (focus != null)
            focus.OnDefocused();

        // Asignamos desasignamos nuestro foco
        focus = null;
        // Llamamos a la función de PlayerMotor encargada de dejar de seguir al target
        motor.StopFollowingTarget();
    }

    /// <summary>
    /// Corrutina encargada de generar el efecto al hacer clic de movimiento
    /// </summary>
    /// <param name="hit"></param>
    /// <returns></returns>
    IEnumerator HitEffect(Vector3 hit)
    {
        // Instanciamos el efecto de particulas y la textura de marca
        GameObject effect = Instantiate(hitEffect, hit, Quaternion.identity);
        // Esperamos 1 segundo
        yield return new WaitForSeconds(1f);
        // Destruimos el objeto
        Destroy(effect);
    }
}
