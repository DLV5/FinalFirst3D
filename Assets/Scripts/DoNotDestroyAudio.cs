using UnityEngine;

public class DoNotDestroyAudio : MonoBehaviour
{
    public static DoNotDestroyAudio Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
