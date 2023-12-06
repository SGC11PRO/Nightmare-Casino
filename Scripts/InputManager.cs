using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    //Variables
    private PlayerInput playerInput; //Referencia a la clase de C# generada en el InputActions
    private PlayerInput.OnFootActions onFoot; //Referencia a las acciones asignadas en ese mapa de acción

    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
    }

    void Update()
    {

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
