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

                // Parar de focusear cualquier objeto 
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

            // Si el rayo colisiona, almacenamos la información en la variable hit
            // y procedemos con la interaccion
            if (Physics.Raycast(ray, out hit, hitRange))
            {
                // Comprobamos si hemos hecho hit sobre un objeto interactivo
                // Si lo hemos hecho, este será nuestro focus
            }
        }
    }
}
