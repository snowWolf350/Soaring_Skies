using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] Button _playButton;
    [SerializeField] Button _quitButton;

    private void Awake()
    {
        _playButton.onClick.AddListener(() =>
        {
            int targetScene = SceneManager.GetActiveScene().buildIndex;
            Debug.Log(targetScene);
            SceneManager.LoadScene(++targetScene);
        });
        _quitButton.onClick.AddListener(() => {
            Application.Quit();
        });
    }
}
