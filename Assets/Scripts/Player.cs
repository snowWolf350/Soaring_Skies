
using System.Collections;
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

    //shooting
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] Transform _shootTransform;
    private float _fireRate = 0.25f;
    private float _fireTimer = 0;
    private float _shootForce = 30;

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

    private void HandleShooting()
    {
        Debug.Log(GameInput.Instance.PlayerIsShooting());
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


    private void GameInput_OnPlayerShoot(object sender, System.EventArgs e)
    {
        Instantiate(_bulletPrefab, _shootTransform);
    }
}
