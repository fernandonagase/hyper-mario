using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioMixer _mixer = null;

    public void SetSfxVolume(float value)
    {
        _mixer.SetFloat("sfxVol", value);
    }

    public void SetMusicVolume(float value)
    {
        _mixer.SetFloat("musicVol", value);
    }
}
