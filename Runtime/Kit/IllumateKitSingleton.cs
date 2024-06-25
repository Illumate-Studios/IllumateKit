using Illumate.Kit;
using UnityEngine;

public class IllumateKitSingleton : MonoBehaviour
{
    public static IllumateKitSingleton Instance { get; private set; }
    
    [Header("References")]
    public Modals modals;

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
