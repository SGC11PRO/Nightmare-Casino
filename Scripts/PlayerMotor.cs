using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    //Variables
    private CharacterController controller;
    private Vector3 playerVelocity;

    [Header("Statements")]
    public bool isGrounded = true;
    public bool canUseHeadbob = true;

    [Header("Debug Values")]
    public float moveX;
    public float moveZ;

    [Header("Movement Settings")]
    public bool sprint = false;

    [Range(0, 10)]
    public float moveSpeed = 4f;

    [Range(0, 10)]
    public float sprintSpeed = 4.5f;

    [Range(0, 5)]
    public float jumpHeigh = 1f;

    [Header("Physics Settings")]
    public float gravity = -9.8f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        //El Charactercontroller ya tiene una variable seteada "isGrounded"
        //Hacemos referencia a esa variable del controller
        isGrounded = controller.isGrounded;
    }

    //Recibe el input del InputManager.cs y las aplica dentro del componente del Character Controller
    public void PlayerMove(Vector2 input) //se requiere una entrada de tipo vector2
    {
        Vector3 moveDirection = Vector3.zero; //Reset vector

        moveDirection.x = input.x; //Asigna el valor del input.x al eje x del vector
        moveDirection.z = input.y; //[!] IMPORTANTE : moveDirection.Z (y no "moveDirection.Y") [!]

        //Variables globales
        moveX = moveDirection.x;
        moveZ = moveDirection.z;

        //Aplicar el vector de moveDirection a una función "Move" ya implementada dentro del Character Controller
        //Movimiento normal
        controller.Move(transform.TransformDirection(moveDirection) * moveSpeed * Time.deltaTime);


        //Movimiento Sprint
        //comprueba el valor de "sprint"  [!] el cual es asignado en el FixedUpdate del InputManager [!]
        if(sprint == true)
        {
            controller.Move(transform.TransformDirection(moveDirection) * sprintSpeed * Time.deltaTime);
        }


        //Aplicar Gravedad
        playerVelocity.y += gravity * Time.deltaTime;

        if (isGrounded && playerVelocity.y < 0) //Solo aplica si no está saltando
        {
            //Aplica un valor fijo, para que no incremente con el paso del tiempo,
            //ya que si no, llegaría un momento en el cual la gravedad sería tan alta que sería imposible saltar
            playerVelocity.y = -2f;
        }

        controller.Move(playerVelocity * Time.deltaTime);

        //debug
        //Debug.Log("[ ! ] Player Sprint : " + sprint);
    }

    //Sprint


    //Salto
    public void Jump()
    {
        //Solo permite saltar si estamos en el suelo
        if (isGrounded)
        {
            //Aplica velocidad en el eje y
            playerVelocity.y = Mathf.Sqrt(jumpHeigh * -3.0f * gravity);
        }
    }
}
