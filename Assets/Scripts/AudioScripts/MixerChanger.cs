using UnityEngine;

public class MixerChanger : MonoBehaviour
{
    public void ChangeVolume(float vol)
    {
        AudioListener.volume = vol;
    }
}
