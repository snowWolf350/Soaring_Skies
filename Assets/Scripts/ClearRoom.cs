using UnityEngine;

public class ClearRoom : MonoBehaviour
{
    [SerializeField] float _enemiesToClear;

    private void Start()
    {
        Gun.OnGunDeath += Gun_OnGunDeath;
    }

    private void Gun_OnGunDeath(object sender, System.EventArgs e)
    {
        if(_enemiesToClear > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
            _enemiesToClear--;
        }
    }
}
