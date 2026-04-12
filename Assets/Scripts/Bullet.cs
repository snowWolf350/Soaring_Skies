using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _bulletLifetime = 10;
    private int _bulletDamage = 20;
    [SerializeField] GameObject _bulletImpactFX;
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(_bulletLifetime);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Gun gun))
        {
            Debug.Log("Hit enemy");
            gun.TakeDamage(_bulletDamage);
            Destroy(gameObject);
            return;
        }
        if (collision.transform.TryGetComponent(out Player player))
        {
            Debug.Log("Player hit");
        }
        Instantiate(_bulletImpactFX,collision.GetContact(0).point,Quaternion.identity);
    }
}
