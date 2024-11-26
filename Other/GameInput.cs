using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;


    private PlayerInputAction playerInputActions;


    private void Awake()
    {
        playerInputActions = new PlayerInputAction();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Interact.performed += Interact_performed; //Thực hiện event (Interact E)
        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) //Nhấn E thực hiện
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVec = playerInputActions.Player.Move.ReadValue<Vector2>();
        inputVec = inputVec.normalized;
        return inputVec;
    }

}
