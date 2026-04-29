using System;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UIElements;

public class Gun : MonoBehaviour,IHasProgress
{
    private Health _gunHealth;

    public event EventHandler<IHasProgress.onProgressChangedEventArgs> onSpeedChanged;
    public static event EventHandler OnGunDeath;

    Player _playerRef;

    [SerializeField] GameObject _gunBullet;
    [SerializeField] Transform _shootTransform;
    [SerializeField] Transform _visualTransform;
    float _playerShootRange = 80;
    private float _fireRate = 3f;
    private float _fireTimer = 0;
    private float _shootForce = 100;

    private void Awake()
    {
        _gunHealth = new Health(100);
    }

    private void Start()
    {
        _playerRef = GlobalReferences.Instance.player;
        _gunHealth.onDeath += _gunHealth_onDeath;
    }

    private void Update()
    {
        float playerDistance = Vector3.Distance(transform.position, _playerRef.transform.position);
        if (playerDistance < _playerShootRange)
        {
            Shoot();
        }
    }
    void Shoot()
    {
        Vector3 shootDirection = _playerRef.transform.position - _shootTransform.position;

        _fireTimer += Time.deltaTime;

        if (_fireTimer > _fireRate)
        {
            GameObject spawnedBullet = Instantiate(
                _gunBullet,
                _shootTransform.position,
                Quaternion.LookRotation(shootDirection, Vector3.up)
            );

            spawnedBullet.GetComponent<Rigidbody>().AddForce(shootDirection.normalized * _shootForce, ForceMode.Impulse);

            _fireTimer = 0;
        }
        _visualTransform.up = shootDirection;
    }

    private void _gunHealth_onDeath(object sender, EventArgs e)
    {
        OnGunDeath?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject);
    }

    public void TakeDamage(int damageAmount)
    {
        _gunHealth.TakeDamage(damageAmount);
        onSpeedChanged?.Invoke(this, new IHasProgress.onProgressChangedEventArgs
        {
            progressNormalized = _gunHealth.GetHealthNormalized()
        });
    }
}
