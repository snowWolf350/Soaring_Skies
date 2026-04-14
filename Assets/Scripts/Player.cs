
using System;
using UnityEngine;

public class Player : MonoBehaviour,IHasProgress
{
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
    [Header("Shooting")]
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] Transform _shootTransform;
    public static event EventHandler<onPlayerShootEventArgs> onPlayerShoot;
    public class onPlayerShootEventArgs : EventArgs
    {
        public string bulletAmount;
    }
    private float _fireRate = 0.25f;
    private float _fireTimer = 0;
    private float _reloadTimer = 0;
    private float _reloadTimeMax = 2.5f;
    private float _shootForce = 70;
    private int _bulletCapacityMax = 20;
    private int _bulletCapacity;
    private bool isReloading;

    [Header("Visuals")]
    [SerializeField] Transform _propellorTransform;
    [SerializeField] Transform _planeVisual;
    private float _propellorSpeedScaler = 150;

    public event EventHandler<IHasProgress.onProgressChangedEventArgs> onProgressChanged;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerSpeed = _playerSpeedDefault;
        _bulletCapacity = _bulletCapacityMax;
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
        onProgressChanged?.Invoke(this, new IHasProgress.onProgressChangedEventArgs
        {
            progressNormalized = (_playerSpeed - _playerSpeedDefault) / (_playerSpeedMaximum - _playerSpeedDefault)
        });
    }

    private void HandlePropellor()
    {
        _propellorTransform.Rotate(0,0,_playerSpeed * Time.deltaTime * _propellorSpeedScaler);
    }
    #endregion

    #region|---Shooting---|

    private void HandleShooting()
    {
        if (!GameInput.Instance.PlayerIsShooting())
        {
            if (_fireTimer != 0) _fireTimer = 0;
            return;
        }
        Debug.Log("Reload timer = " + _reloadTimer + "Ammo Left = " + _bulletCapacity);
        //reloading logic
        if (isReloading)
        {
            _reloadTimer += Time.deltaTime;
            if (_reloadTimer > _reloadTimeMax)
            {
                //done reloading 
                isReloading = false;
                _reloadTimer = 0;
                _bulletCapacity = _bulletCapacityMax;
            }
            return;
        }
        //shooting logic

        if (_bulletCapacity > 0)
        {
            //bullets left to shoot
            _fireTimer += Time.deltaTime;
            if (_fireTimer > _fireRate)
            {
                GameObject spawnedBullet = Instantiate(_bulletPrefab, _shootTransform.transform.position, Quaternion.LookRotation(_shootTransform.forward, _shootTransform.up));
                spawnedBullet.GetComponent<Rigidbody>().AddForce(_shootTransform.forward * _shootForce, ForceMode.Impulse);
                _fireTimer = 0;
                _bulletCapacity--;
                onPlayerShoot?.Invoke(this, new onPlayerShootEventArgs
                {
                    bulletAmount = _bulletCapacity.ToString() + "/" + _bulletCapacityMax.ToString(),
                });
            }
        }
        else
        {
            isReloading = true;
        }
    }

    #endregion
}
