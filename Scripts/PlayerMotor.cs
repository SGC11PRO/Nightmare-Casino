using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    //Variables
    private CharacterController controller;
    private Vector3 playerVelocity;

    public float speed = 5f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        
    }

    //Recibe el input del InputManager.cs y las aplica dentro del componente del Character Controller
    public void PlayerMove(Vector2 input) //se requiere una entrada de tipo vector2
    {
        Vector3 moveDirection = Vector3.zero; //Reset vector

        moveDirection.x = input.x; //Asigna el valor del input.x al eje x del vector
        moveDirection.z = input.y; //[!] IMPORTANTE : moveDirection.Z (y no "moveDirection.Y") [!]

        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime); //Aplica el vector de moveDirection a una función "Move" ya implementada dentro del Character Controller
    }
}
