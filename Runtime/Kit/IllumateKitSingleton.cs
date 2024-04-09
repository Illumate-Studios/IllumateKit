using UnityEngine;

public class IllumateKitSingleton : MonoBehaviour
{
    public static IllumateKitSingleton Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
