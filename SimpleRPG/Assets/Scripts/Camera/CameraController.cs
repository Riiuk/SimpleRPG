using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    [Header("Zoom")]
    [Tooltip("Velocidad del zoom")]
    public float zoomSpeed = 4f;
    [Tooltip("Zoom mínimo")]
    public float minZoom = 5f;
    [Tooltip("Zoom máximo")]
    public float maxZoom = 15f;

    [Header("Rotation")]
    [Tooltip("Variable que usamos para almacenar la rotación vertical de la cámara")]
    public float pitch = 2f;
    [Tooltip("Velocidad de rotación de la cámara")]
    public float rotationSpeed = 100f;

    public bool isInverted = false;

    [Header("Unity Settings")]
    [Tooltip("Objeto al que seguirá la cámara")]
    public Transform target;
    [Tooltip("Posición de la cámara respecto al objeto que sigue")]
    public Vector3 offset;

    private float currentZoom = 10f;    // Variable que usamos para almacenar el zoom actual
    private float rotationInput = 0f;

    void Update()
    {
        // Modificamos el valor del zoom actual con la rueda del raton,
        // Si la movemos hacia atras, subiremos la cámara y si la movemos hacia adelante, la bajaremos
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        // Ademas, le damos unos margenes mínimos y máximos
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        if (isInverted)
        {
            rotationInput -= Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        } else
        {
            rotationInput -= -Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        }
        
    }

    /// <summary>
    /// Función que es llamada justo desde del Update
    /// </summary>
    void LateUpdate()
    {
        // Cambiamos la posición de la cámara a la nueva posición del target,
        // sumandole los valores de offset y el zoom actual
        transform.position = target.position - offset * currentZoom;
        // Rotamos la cámara hacia el objetivo, sumandole una rotación vertical
        transform.LookAt(target.position + Vector3.up * pitch);

        transform.RotateAround(target.position, Vector3.down, rotationInput);
    }
}
