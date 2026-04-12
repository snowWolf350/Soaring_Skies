using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _bulletLifetime = 10;
    private int _bulletDamage = 20;
    [SerializeField] GameObject _bulletImpactFX;

    enum DamageType
    {
        player,
        enemy
    }

    [SerializeField] DamageType _thisBulletDamageType;
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(_bulletLifetime);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (_thisBulletDamageType)
        {
                case DamageType.player:
                if (collision.transform.TryGetComponent(out Player player))
                {
                    //enemy hit player
                    Debug.Log("Player hit");
                }
                break;
                case DamageType.enemy:
                if (collision.transform.TryGetComponent(out Gun gun))
                {
                    //player hit enemy
                    Debug.Log("Hit enemy");
                    gun.TakeDamage(_bulletDamage);
                    Instantiate(_bulletImpactFX, collision.GetContact(0).point, Quaternion.identity);
                    Destroy(gameObject);
                }
                else if(collision.transform.TryGetComponent(out Player player2))
                {
                    //player Hit Himself
                    return;
                }
                    break;
        }
        
        
        Instantiate(_bulletImpactFX,collision.GetContact(0).point,Quaternion.identity);
    }
}
