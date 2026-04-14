using TMPro;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _ammoText;

    private void Start()
    {
        Player.onPlayerShoot += Player_onPlayerShoot;
    }

    private void Player_onPlayerShoot(object sender, Player.onPlayerShootEventArgs e)
    {
        _ammoText.text = e.bulletAmount;
    }
}
