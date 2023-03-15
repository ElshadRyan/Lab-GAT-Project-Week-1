using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputAction PlayerInputAction;
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;

    private void Awake()
    {
        PlayerInputAction = new PlayerInputAction();
        PlayerInputAction.Player.Enable();

        PlayerInputAction.Player.Interact.performed += Interact_performed;
        PlayerInputAction.Player.InteractAlternate.performed += InteractAlternate_performed;
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 InputVector = PlayerInputAction.Player.Move.ReadValue<Vector2>();

        InputVector = InputVector.normalized;
        return InputVector;
    }
    
}
