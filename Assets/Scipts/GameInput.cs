using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private const string PLAYER_PREFS_BINDINGS = "InputBindings";
    
    public static GameInput Instance { get; private set; }

    public event EventHandler<OnDashEventsArgs> OnDashAction;
    public class OnDashEventsArgs : EventArgs
    {
        public Vector2 dashDirection;
    }

    public event EventHandler OnAttackAction;
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternativeAction;
    public event EventHandler OnOpenInventoryAction;
    public event EventHandler OnPauseAction;
    
    private PlayerInputActions playerInputActions;
    
    private Dictionary<string, Vector2> keyToDirectionMapping = new Dictionary<string, Vector2>()
    {
        { "W", new Vector2(0f, 1f) },
        { "A", new Vector2(-1f, 0f) },
        { "S", new Vector2(0f, -1f) },
        { "D", new Vector2(1f, 0f) }
    };
    
    private void Awake()
    {
        Instance = this;
        
        playerInputActions = new PlayerInputActions();
        
        if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
        {
            playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
        }
        
        playerInputActions.Player.Enable();

        playerInputActions.Player.Dash.performed += Dash_OnPerformed;
        playerInputActions.Player.Attack.performed += Attack_OnPerformed;
        playerInputActions.Player.Interact.performed += Interact_OnPerformed;
        playerInputActions.Player.InteractAlternative.performed += InteractAlternative_OnPerformed;
        playerInputActions.Player.OpenInventory.performed += OpenInventory_OnPerformed;
        playerInputActions.Player.Pause.performed += Pause_OnPerformed;

        SetCursorMode(CursorLockMode.Locked);
    }

    private void OnDestroy()
    {
        playerInputActions.Player.Dash.performed -= Dash_OnPerformed;
        playerInputActions.Player.Attack.performed -= Attack_OnPerformed;
        playerInputActions.Player.Interact.performed -= Interact_OnPerformed;
        playerInputActions.Player.InteractAlternative.performed -= InteractAlternative_OnPerformed;
        playerInputActions.Player.OpenInventory.performed -= OpenInventory_OnPerformed;
        playerInputActions.Player.Pause.performed -= Pause_OnPerformed;
        
        playerInputActions.Dispose();
    }
    
    private void Dash_OnPerformed(InputAction.CallbackContext obj)
    {
        string keyPressed = obj.control.displayName;

        if (keyToDirectionMapping.TryGetValue(keyPressed, out Vector2 dashDirection))
        {
            OnDashAction?.Invoke(this, new OnDashEventsArgs()
            {
                dashDirection = dashDirection
            });
        }
    }

    private void Interact_OnPerformed(InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternative_OnPerformed(InputAction.CallbackContext obj)
    {
        OnInteractAlternativeAction?.Invoke(this, EventArgs.Empty);
    }

    private void Attack_OnPerformed(InputAction.CallbackContext obj)
    {
        OnAttackAction?.Invoke(this, EventArgs.Empty);
    }

    private void OpenInventory_OnPerformed(InputAction.CallbackContext obj)
    {
        OnOpenInventoryAction?.Invoke(this, EventArgs.Empty);
    }

    private void Pause_OnPerformed(InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }

    public Vector2 GetInputCameraMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.MoveCamera.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }

    public static void SetCursorMode(CursorLockMode lockMode)
    {
        Cursor.lockState = lockMode;
    }
    
}
