using TMPro;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _ammoText;

    private void Start()
    {
        PlayerShooting.onAmmoChange += Player_onPlayerShoot;
    }

    private void Player_onPlayerShoot(object sender, PlayerShooting.onPlayerShootEventArgs e)
    {
        _ammoText.text = e.bulletAmount;
    }
}
