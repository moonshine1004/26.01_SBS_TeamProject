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
    private string BgmVolume = "BGM_Volume";
    private string SfxVolume = "SFX_Volume";


    private void Awake()
    {
        SetBgmVolume(GamePrefsRepository.BgmVolume);
        SetSfxVolume(GamePrefsRepository.SfxVolume);
    }
    public void SetBgmVolume(float value)
    {
        value = Mathf.Clamp01(value);
        GamePrefsRepository.BgmVolume = value;
        _audioMixer.SetFloat(BgmVolume, ToDecibel(GamePrefsRepository.BgmVolume));
    }

    public void SetSfxVolume(float value)
    {
        value = Mathf.Clamp01(value);
        GamePrefsRepository.SfxVolume = value;
        _audioMixer.SetFloat(SfxVolume, ToDecibel(GamePrefsRepository.SfxVolume));
    }

    public void OnBgmSliderChanged(float value)
    {
        GamePrefsRepository.BgmVolume = value;
        SetBgmVolume(value);
    }

    public void OnSfxSliderChanged(float value)
    {
        GamePrefsRepository.SfxVolume = value;
        SetSfxVolume(value);
    }

    private float ToDecibel(float value)
    {
        if (value <= 0.0001f) return -80f;
        return Mathf.Log10(value) * 20f;
    }
}