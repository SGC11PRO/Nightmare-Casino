using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    //Variables
    public Camera cam;
    private float xRotation = 0f;

    private PlayerMotor playerMotor;

    [Header("Camera Settings")]

    [Range(0, 30)] public float xSensitivity = 20f;
    [Range(0, 30)] public float ySensitivity = 20f;

    [Header("Headbob Settings")]
    [Range(0, 20f)]public float walkBobSpeed = 15f;
    [Range(0, 0.25f)] public float walkBobAmount = 0.1f;

    [Range(0, 20f)] public float sprintBobSpeed = 20f;
    [Range(0, 0.5f)] public float sprintBobAmount = 0.15f;

    private float defaultYpos = 0.6f;
    private float timer;

    private void Awake()
    {
        defaultYpos = cam.transform.localPosition.y;

        //ASIGNAR SCRIPTS
        playerMotor = GetComponent<PlayerMotor>();
    }

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

    public void Headbob()
    {
        if (playerMotor.isGrounded != true) return; //si el jugador no esta en el suelo, omite el código a continuación
        
        //Comprueba si el personaje se esta moviendo
        if(Mathf.Abs(playerMotor.moveX) > 0.1f || Mathf.Abs(playerMotor.moveZ) > 0.1f)
        {
            //Aplica la velocidad de sprint al headbob cuando esta sprintando
            if (playerMotor.sprint == true) timer += Time.deltaTime * sprintBobSpeed;

            //Aplica la velocidad normal al headbob cuando no esta sprintando
            if (playerMotor.sprint != true) timer += Time.deltaTime * walkBobSpeed;

            //Aplica el headbob a la camara
            cam.transform.localPosition = new Vector3(
                cam.transform.localPosition.x,
                defaultYpos + Mathf.Sin(timer) * (playerMotor.sprint ? sprintBobAmount : walkBobAmount), //Comprueba si estamos sprintando ("?") o no (":"), y aplica las respectivas cantindades
                cam.transform.localPosition.z
                );
        }
    }
}
