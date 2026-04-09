
using UnityEngine;

public class Player : MonoBehaviour
{
    //Player speed variables
    private float _playerSpeed;
    private float _playerSpeedDefault = 2f;
    private float _playerSpeedMaximum = 10;
    private float _playerAcceleration = 0.1f;

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
        Vector2 pitchVector = GameInput.Instance.GetMouseDelta();
        float tiltInt = GameInput.Instance.GetTiltInt();

        if (inputVector.y == 1 && _playerSpeed < _playerSpeedMaximum)
        {
            //accelerating
            _playerSpeed += _playerAcceleration;
        }
        else if (inputVector.y == -1 && _playerSpeed > _playerSpeedDefault)
        {
            _playerSpeed -= _playerAcceleration;
        }

        //y axis
        float yawRotation = _playerRotation *inputVector.x * Time.deltaTime;
        //x axis
        float pitchRotation = _playerRotation * pitchVector.y * Time.deltaTime;
        //z axis
        float tiltRotation = _playerRotation * tiltInt * Time.deltaTime;
        transform.Rotate(pitchRotation, yawRotation, tiltRotation);
        /*
        transform.Rotate(Vector3.up, _playerRotation * inputVector.x * Time.deltaTime);
        transform.Rotate(Vector3.forward, _playerRotation * tiltInt * Time.deltaTime);
        transform.Rotate(Vector3.right,_playerRotation * pitchVector.y * Time.deltaTime);
        */

        transform.position += Time.deltaTime * _playerSpeed * transform.forward;
    }
}
