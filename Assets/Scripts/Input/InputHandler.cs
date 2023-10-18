using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputHandler : Initializable
{ 

    private GameInput _gameInput;

    public override void Init()
    {
        _gameInput = new();

        _gameInput.Enable();
    }

    public Vector2 GetPlayerMoveInput()
   {
        return _gameInput.Player.Move.ReadValue<Vector2>();
   }

    public Vector2 GetMouseMoveInput()
    {
        return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }

    public InputAction RunAction { get => _gameInput.Player.Run; }

    public InputAction JumpAction { get => _gameInput.Player.Jump; }
    public  InputAction RightClickAction { get => _gameInput.Player.RightClick; }
}
