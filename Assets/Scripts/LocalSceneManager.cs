using UnityEngine;
using UnityEngine.SceneManagement;

public class LocalSceneManager : MonoBehaviour
{
    public static LocalSceneManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void LoadNextScene()
    {
        int buildIndex = SceneManager.GetActiveScene().buildIndex + 1;
        Debug.Log(buildIndex);
        SceneManager.LoadScene(buildIndex);
    }
}
