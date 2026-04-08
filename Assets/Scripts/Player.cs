
using UnityEngine;

public class Player : MonoBehaviour
{
    //Player speed variables
    private float _playerSpeed;
    private float _playerSpeedDefault = 0.5f;
    private float _playerSpeedMaximum = 2;
    private float _playerAcceleration = 0.01f;

    //player rotation
    private float _playerRotation = 20f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerSpeed = _playerSpeedDefault;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    /// <summary>
    /// Accelerates, deccelerates the player,decides yaw(y axis rotation)
    /// </summary>
    private void HandleMovement()
    {
        //front and back is y, left and right is x
        Vector2 inputVector = GameInput.Instance.GetInputVector();
        float tiltInt = GameInput.Instance.GetTiltInt();
        Debug.Log(tiltInt);

        if (inputVector.y == 1 && _playerSpeed < _playerSpeedMaximum)
        {
            //accelerating
            _playerSpeed += _playerAcceleration;
        }
        else if (inputVector.y == -1 && _playerSpeed > _playerSpeedDefault)
        {
            _playerSpeed -= _playerAcceleration;
        }

        transform.Rotate(transform.up, _playerRotation * inputVector.x * Time.deltaTime);
        transform.Rotate(transform.forward, _playerRotation * tiltInt * Time.deltaTime);

        transform.position += Time.deltaTime * _playerSpeed * transform.forward;
    }
}
