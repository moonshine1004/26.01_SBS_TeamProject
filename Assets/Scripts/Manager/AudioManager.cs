using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<AudioManager>();
            }
            return _instance;
        }
    }

    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private string bgmParam = "BgmVolume";
    [SerializeField] private string sfxParam = "SfxVolume";


    private void Awake()
    {
        SetBgmVolume(GamePrefsRepository.BgmVolume);
        SetSfxVolume(GamePrefsRepository.SfxVolume);
    }
    public void SetBgmVolume(float value)
    {
        value = Mathf.Clamp01(value);
        _audioMixer.SetFloat(bgmParam, ToDecibel(value));
    }

    public void SetSfxVolume(float value)
    {
        value = Mathf.Clamp01(value);
        _audioMixer.SetFloat(sfxParam, ToDecibel(value));
    }
    private float ToDecibel(float value)
    {
        if (value <= 0.0001f) return -80f;
        return Mathf.Log10(value) * 20f;
    }
}