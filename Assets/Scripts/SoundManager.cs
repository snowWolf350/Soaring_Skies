using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Player Sounds")]
    [SerializeField] AudioSource _propellorSource;
    float _defaultPropellorVolume = 0.15f;
    float _volumeIncrease = 0.05f;

    private void Start()
    {
        Player.Instance.onSpeedChanged += Player_onSpeedChanged;
    }

    private void Player_onSpeedChanged(object sender, IHasProgress.onProgressChangedEventArgs e)
    {
        _propellorSource.volume = _defaultPropellorVolume + e.progressNormalized * _volumeIncrease;
    }
}
