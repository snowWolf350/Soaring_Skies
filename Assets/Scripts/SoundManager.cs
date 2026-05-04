using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Player Sounds")]
    [SerializeField] AudioSource _propellorSource;
    [SerializeField] AudioClip _gunSound;
    [SerializeField] AudioClip _reloadSound;
    float _shootVolume = 0.5f;
    float _reloadVolume = 0.7f;
    float _defaultPropellorVolume = 0.05f;
    float _volumeIncrease = 0.05f;

    private void Start()
    {
        Player.Instance.onSpeedChanged += Player_onSpeedChanged;
        PlayerShooting.onPlayerShoot += PlayerShooting_onPlayerShoot;
        PlayerShooting.onPlayerReload += PlayerShooting_onPlayerReload;
    }

    private void PlayerShooting_onPlayerReload(object sender, System.EventArgs e)
    {
        _propellorSource.PlayOneShot(_reloadSound,_reloadVolume);
    }

    private void PlayerShooting_onPlayerShoot(object sender, System.EventArgs e)
    {
        _propellorSource.PlayOneShot(_gunSound, _shootVolume);
    }

    private void Player_onSpeedChanged(object sender, IHasProgress.onProgressChangedEventArgs e)
    {
        _propellorSource.volume = _defaultPropellorVolume + e.progressNormalized * _volumeIncrease;
    }
}
