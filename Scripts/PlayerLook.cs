using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    //Variables
    public Camera cam;
    private float xRotation = 0f;

    [Header("Camera Settings")]
    [Range(0, 30)]
    public float xSensitivity = 20f;

    [Range(0, 30)]
    public float ySensitivity = 20f;


    //Player look function
    public void Look(Vector2 input)
    {
        //Asignar input a las variables
        float mouseX = input.x;
        float mouseY = input.y;

        //Calcular la rotación de la cámara arriba/abajo
        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -50f, 50f); //Limitar el giro de la camara en el eje Y

        //Aplicar rotacion arriba/abajo
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        //Rotar el jugador derecha/izquierda
        this.transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }
}
