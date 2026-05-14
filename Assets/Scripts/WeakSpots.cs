using UnityEngine;

public class weakSpots : MonoBehaviour
{
    private Health _spotHealth;

    private void Awake()
    {
        _spotHealth = new Health(100);
    }

    private void Start()
    {
        _spotHealth.onDeath += _spotHealth_onDeath;
    }

    private void _spotHealth_onDeath(object sender, System.EventArgs e)
    {
        Destroy(gameObject);
        Debug.Log("enemy hit big time");
    }

    public Health GetHealth()
    {
        return _spotHealth;
    }
}
