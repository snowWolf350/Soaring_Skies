using TMPro;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _ammoText;
    int _ammoMax = 20;

    private void Start()
    {
        PlayerShooting.onAmmoChange += Player_onAmmoChane;
    }

    private void Player_onAmmoChane(object sender, PlayerShooting.onPlayerShootEventArgs e)
    {
        if(e.bulletAmount == 1)
        {
            _ammoText.text = "Reload !";
            return;
        }

        _ammoText.text = e.bulletAmount.ToString() + "/" + _ammoMax.ToString();
    }
}
