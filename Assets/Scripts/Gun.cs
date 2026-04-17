using System;
using UnityEngine;

public class Gun : MonoBehaviour,IHasProgress
{
    private Health _gunHealth;

    public event EventHandler<IHasProgress.onProgressChangedEventArgs> onProgressChanged;

    Player _playerRef;

    [SerializeField] GameObject _gunBullet;
    [SerializeField] Transform _shootTransform;
    [SerializeField] Transform _visualTransform;
    float _playerShootRange = 80;
    private float _fireRate = .5f;
    private float _fireTimer = 0;
    private float _shootForce = 10;

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
        _fireTimer += Time.deltaTime;
        if (_fireTimer > _fireRate)
        {
            GameObject spawnedBullet = Instantiate(_gunBullet, _shootTransform.transform.position, Quaternion.LookRotation(_shootTransform.forward, _shootTransform.up));
            Vector3 shootDirection = _playerRef.transform.position - transform.position;
            spawnedBullet.GetComponent<Rigidbody>().AddForce(shootDirection * _shootForce, ForceMode.Impulse);
            _fireTimer = 0;
        }
    }

    private void _gunHealth_onDeath(object sender, EventArgs e)
    {
        Destroy(gameObject);
    }

    public void TakeDamage(int damageAmount)
    {
        _gunHealth.TakeDamage(damageAmount);
        onProgressChanged?.Invoke(this, new IHasProgress.onProgressChangedEventArgs
        {
            progressNormalized = _gunHealth.GetHealthNormalized()
        });
    }
}
