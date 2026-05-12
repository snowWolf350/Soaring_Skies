using UnityEngine;


public class Portal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player entered the collider");
        LocalSceneManager.Instance.LoadNextScene();
    }
}
