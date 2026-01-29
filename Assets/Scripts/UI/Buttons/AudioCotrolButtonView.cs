using UnityEngine;
using UnityEngine.UI;

public enum AudioControlType
{
    Bgm,
    Sfx
}

public class AudioControlButtonView : MonoBehaviour
{
    [SerializeField] private AudioControlType _audioControlType;
    [SerializeField] private Slider _slider;
    private Image _image;
    private Sprite _defaultImage;
    [SerializeField] private Sprite _changeImage;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _defaultImage = _image.sprite;
        switch(_audioControlType)
        {
            case AudioControlType.Bgm:
                _slider.value = GamePrefsRepository.BgmVolume;
                break;
            case AudioControlType.Sfx:
                _slider.value = GamePrefsRepository.SfxVolume;
                break;
        }
    }

    public void OnClick()
    {
        SwitchImage();
        switch(_audioControlType)
        {
            case AudioControlType.Bgm:
                _slider.value = 0f;
                GamePrefsRepository.BgmVolume = 0f;
                AudioManager.Instance.SetBgmVolume(0f);
                break;
            case AudioControlType.Sfx:
                _slider.value = 0f;
                GamePrefsRepository.SfxVolume = 0f;
                AudioManager.Instance.SetSfxVolume(0f);
                break;
        }
    }

    private void SwitchImage()
    {
        if (_image.sprite == _defaultImage)
            _image.sprite = _changeImage;
        else
        _image.sprite = _defaultImage;
    }
}