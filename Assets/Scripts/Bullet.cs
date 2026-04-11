using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _bulletLifetime = 10;
    private int _bulletDamage = 20;
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
        }
    }
}
