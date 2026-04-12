using UnityEngine;
using UnityEngine.UI;


public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] Image BarImage;
    [SerializeField] GameObject HasProgressGameObject;
    [SerializeField] bool _hideOn0 = true;

    IHasProgress HasProgress;

    private void Start()
    {
        HasProgress = HasProgressGameObject.GetComponent<IHasProgress>();
        HasProgress.onProgressChanged += HasProgress_onProgressChanged;
        BarImage.fillAmount = 1;
        Hide();
    }

    private void HasProgress_onProgressChanged(object sender, IHasProgress.onProgressChangedEventArgs e)
    {
        if (e.progressNormalized == 0 && _hideOn0)
        {
            Hide();
        }
        else
        {
            Show();
        }

        BarImage.fillAmount = e.progressNormalized;
    }
    void Hide()
    {
        gameObject.SetActive(false);
    }
    void Show()
    {
        gameObject.SetActive(true);
    }
}
