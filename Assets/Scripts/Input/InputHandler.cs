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

        StartCoroutine(CursorVisibleChanger());
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private IEnumerator CursorVisibleChanger()
    {
        while(true)
        {
            yield return null;
            if(Input.GetKey(KeyCode.Escape))
            {
                CursorShow();
            }
            if(Input.anyKey && !Input.GetKey(KeyCode.Escape))
            {
                CursorHide();
            }
        }
    }

    public void CursorHide()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void CursorShow()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public Vector2 GetPlayerMoveInput() => _gameInput.Player.Move.ReadValue<Vector2>();

    public Vector2 GetMouseMoveInput() => new(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

    public InputAction RunAction => _gameInput.Player.Run; 

    public InputAction JumpAction => _gameInput.Player.Jump; 
    public  InputAction RightClickAction => _gameInput.Player.RightClick; 
}
