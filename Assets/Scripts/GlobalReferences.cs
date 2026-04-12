using UnityEngine;

public class GlobalReferences : MonoBehaviour
{
    public static GlobalReferences Instance;

    public Player player;

    private void Awake()
    {
        Instance = this;
    }
}
