using System;
using UnityEngine;

public class Gun : MonoBehaviour,IHasProgress
{
    private Health _gunHealth;

    public event EventHandler<IHasProgress.onProgressChangedEventArgs> onProgressChanged;

    private void Awake()
    {
        _gunHealth = new Health(100);
    }

    private void Start()
    {
        _gunHealth.onDeath += _gunHealth_onDeath;
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
