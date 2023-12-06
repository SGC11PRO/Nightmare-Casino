using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    //Variables
    private PlayerInput playerInput; //Referencia a la clase de C# generada en el InputActions
    private PlayerInput.OnFootActions onFoot; //Referencia a las acciones asignadas en ese mapa de acción

    private PlayerMotor playerMotor; //Referencia al script de PlayerMotor
    private PlayerLook playerLook; //Referencia al script de PlayerLook

    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;

        //[!] Scripts Referenciados [!]
        playerMotor = GetComponent<PlayerMotor>();
        playerLook = GetComponent<PlayerLook>();

        //Asignar acción "Jump"
        //Cada vez que llamamos a la acción jump (pulsando espacio) => Ejecuta la funcion jump del playermotor
        onFoot.Jump.performed += ctx => playerMotor.Jump(); //ctx = "Callback Context"
    }

    void FixedUpdate()
    {
        //Envía al script de PlayerMotor los valores del input para que lo utilice en la función de "PlayerMove"
        playerMotor.PlayerMove(onFoot.Movement.ReadValue<Vector2>()); //llama a la función de "PlayerMove" asignandole los valores del action map ("onFoot")
    }

    private void LateUpdate()
    {
        //Envia al script de PlayerLook los valores del map action "Look"
        playerLook.Look(onFoot.Look.ReadValue<Vector2>());
    }

    //Enable Inputs
    private void OnEnable()
    {
        onFoot.Enable(); //Enable action map
    }

    private void OnDisable()
    {
        onFoot.Disable(); //Disable action map
    }
}
