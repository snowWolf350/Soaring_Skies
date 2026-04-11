
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Player speed variables
    private float _playerSpeed;
    private float _playerSpeedDefault = 10f;
    private float _playerSpeedMaximum = 20;
    private float _playerAcceleration = 2f;

    //player rotation
    float _yawRotation;
    float _yawRotationRate = 50;
    float _pitchRotation;
    float _pitchRotationRate = 20;
    float _tiltRotation;
    float _tiltRotationRate = 20;

    //shooting
    [Header("Shooting")]
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] Transform _shootTransform;
    private float _fireRate = 0.25f;
    private float _fireTimer = 0;
    private float _shootForce = 70;

    [Header("Visuals")]
    [SerializeField] Transform _propellorTransform;
    [SerializeField] Transform _planeVisual;
    private float _propellorSpeedScaler = 150;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerSpeed = _playerSpeedDefault;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleShooting();

    }
    private void LateUpdate()
    {
        HandlePropellor();
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
    }

    private void HandleShooting()
    {
        if (!GameInput.Instance.PlayerIsShooting()) {
            if (_fireTimer != 0) _fireTimer = 0;
            return; }

        _fireTimer += Time.deltaTime;
        if (_fireTimer > _fireRate)
        {
            GameObject spawnedBullet = Instantiate(_bulletPrefab, _shootTransform.transform.position,Quaternion.LookRotation(_shootTransform.forward,_shootTransform.up));
            spawnedBullet.GetComponent<Rigidbody>().AddForce(_shootTransform.forward * _shootForce,ForceMode.Impulse);
            _fireTimer = 0;
        }
       
    }

    private void HandlePropellor()
    {
        _propellorTransform.Rotate(0,0,_playerSpeed * Time.deltaTime * _propellorSpeedScaler);
    }


    private void GameInput_OnPlayerShoot(object sender, System.EventArgs e)
    {
        Instantiate(_bulletPrefab, _shootTransform);
    }
}
