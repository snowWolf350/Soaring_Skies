using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance;

    PlayerInput _playerInput;
    public static event EventHandler OnPlayerReload;

    private void Awake()
    {
        _playerInput = new PlayerInput();

        if(Instance != null)
        {
            Destroy(Instance.gameObject);
        }
        Instance = this;
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _playerInput.player.reload.performed += Reload_performed;
    }

    private void Reload_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPlayerReload?.Invoke(this,EventArgs.Empty);
    }

    private void OnEnable()
    {
        _playerInput.player.move.Enable();
        _playerInput.player.tilt.Enable();
        _playerInput.player.pitch.Enable();
        _playerInput.player.shoot.Enable();
        _playerInput.player.reload.Enable();
    }


    public Vector2 GetInputVector()
    {
        Vector2 inputVector = _playerInput.player.move.ReadValue<Vector2>();
        return inputVector;
    }
    public float GetTiltInt()
    {
        float tiltInt = _playerInput.player.tilt.ReadValue<float>();
        return -tiltInt;
    }

    public Vector2 GetMouseDelta()
    {
        return -_playerInput.player.pitch.ReadValue<Vector2>();  
    }

    public bool PlayerIsShooting()
    {
        return _playerInput.player.shoot.IsInProgress();
    }
}
