using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance;

    PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = new PlayerInput();

        if(Instance != null)
        {
            Destroy(Instance.gameObject);
        }
        Instance = this;
    }

    private void OnEnable()
    {
        _playerInput.player.move.Enable();
        _playerInput.player.tilt.Enable();
    }

    public Vector2 GetInputVector()
    {
        Vector2 inputVector = _playerInput.player.move.ReadValue<Vector2>();
        return inputVector;
    }
    public float GetTiltInt()
    {
        float tiltInt = _playerInput.player.tilt.ReadValue<float>();
        return tiltInt;
    }
}
