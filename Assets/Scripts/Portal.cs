using UnityEngine;


public class Portal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        LocalSceneManager.Instance.LoadNextScene();
    }
}
