using System;
using UnityEngine;

public class Player : MonoBehaviour,IHasProgress
{
    public static Player Instance;

    //Player speed variables
    private float _playerSpeed;
    private float _playerSpeedDefault = 10f;
    private float _playerSpeedMaximum = 20;
    private float _playerAcceleration = 2f;

    //player rotation
    float _yawRotation;
    float _yawRotationRate = 20;
    float _pitchRotation;
    float _pitchRotationRate = 20;
    float _tiltRotation;
    float _tiltRotationRate = 20;

    //shooting
    

    [Header("Visuals")]
    [SerializeField] Transform _propellorTransform;
    [SerializeField] Transform _planeVisual;
    private float _propellorSpeedScaler = 150;

    public event EventHandler<IHasProgress.onProgressChangedEventArgs> onSpeedChanged;


    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(Instance.gameObject);
        }
        Instance = this;
    }

    void Start()
    {
        _playerSpeed = _playerSpeedDefault;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }
    private void LateUpdate()
    {
        HandlePropellor();
    }

    #region |---Movement---|

    /// <summary>
    /// Accelerates, deccelerates the player,decides yaw(y axis rotation)
    /// </summary>
    private void HandleMovement()
    {
        //front and back is y, left and right is x
        Vector2 inputVector = GameInput.Instance.GetInputVector();
        Vector2 pitchVector = GameInput.Instance.GetMouseDelta();
        float tiltInt = GameInput.Instance.GetTiltInt();

        if (inputVector.y != 0)
        {
            _playerSpeed += inputVector.y * _playerAcceleration * Time.deltaTime;
            _playerSpeed = Mathf.Clamp(_playerSpeed, _playerSpeedDefault, _playerSpeedMaximum);
        }
        //y axis
        _yawRotation += _yawRotationRate * -pitchVector.x * Time.deltaTime;
        //x axis
        _pitchRotation += _pitchRotationRate * pitchVector.y * Time.deltaTime;
        //z axis
        _tiltRotation += _tiltRotationRate * tiltInt * Time.deltaTime;

        _pitchRotation = Mathf.Clamp(_pitchRotation, -85, 85);

        transform.rotation = Quaternion.Euler(_pitchRotation, _yawRotation, _tiltRotation);
        transform.position += transform.forward * Time.deltaTime * _playerSpeed;
        onSpeedChanged?.Invoke(this, new IHasProgress.onProgressChangedEventArgs
        {
            progressNormalized = (_playerSpeed - _playerSpeedDefault) / (_playerSpeedMaximum - _playerSpeedDefault)
        });
    }

    private void HandlePropellor()
    {
        _propellorTransform.Rotate(0,0,_playerSpeed * Time.deltaTime * _propellorSpeedScaler);
    }
    #endregion

   
}
