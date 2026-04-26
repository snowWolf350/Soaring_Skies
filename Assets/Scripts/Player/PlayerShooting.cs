
using System;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] Transform _shootTransform;
    public static event EventHandler<onPlayerShootEventArgs> onAmmoChange;
    public class onPlayerShootEventArgs : EventArgs
    {
        public int bulletAmount;
    }
    private float _fireRate = 0.25f;
    private float _fireTimer = 0;
    private float _reloadTimer = 0;
    private float _reloadTimeMax = 2.5f;
    private float _shootForce = 70;
    private int _bulletCapacityMax = 20;
    private int _bulletCapacity;
    private bool isReloading;

    private void Start()
    {
        _bulletCapacity = _bulletCapacityMax;
        GameInput.OnPlayerReload += GameInput_OnPlayerReload;
    }

    private void GameInput_OnPlayerReload(object sender, EventArgs e)
    {
        if(_bulletCapacity != _bulletCapacityMax && !isReloading)
        {
            //can reload
            isReloading = true;
        }
    }

    private void Update()
    {
        HandleShooting();
    }

    #region|---Shooting---|

    private void HandleShooting()
    {
        Debug.Log("Reload timer = " + _reloadTimer);
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
                onAmmoChange?.Invoke(this, new onPlayerShootEventArgs
                {
                    bulletAmount = _bulletCapacity,
                });
            }
            return;
        }

        if (!GameInput.Instance.PlayerIsShooting())
        {
            if (_fireTimer != 0) _fireTimer = 0;
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
                onAmmoChange?.Invoke(this, new onPlayerShootEventArgs
                {

                    bulletAmount = _bulletCapacity
                });
                _bulletCapacity--;
            }
        }
        else
        {
            //play empty mag sound
        }
    }

    #endregion
}
