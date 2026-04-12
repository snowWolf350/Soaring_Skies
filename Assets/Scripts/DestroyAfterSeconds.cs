using System.Collections;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    public float _destroyTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator Start()
    {
        yield return new WaitForSeconds(_destroyTime);
        Destroy(gameObject);
    }

}
