using UnityEngine;
using Game.Events;

public enum ButtonType
{
    PlayerSelect,
    StageSelect
}

public class SelectButtonView : ButtonView
{
    [SerializeField] private bool _isPressed = false;
    [SerializeField] private int _id;
    [SerializeField] private ButtonType _buttonType;
    
    private void OnEnable()
    {
        switch (_buttonType)
        {
            case ButtonType.PlayerSelect:
                EventBus.Instance.Subscribe<OnPlayerSelectButtonPressed>(_ => ResetButtonImage());
                if (GamePrefsRepository.CurrentPlayPlayer == _id)
                {
                    _isPressed = true;
                    _buttonImage.sprite = _pressedButton;
                }
                break;
            case ButtonType.StageSelect:
                EventBus.Instance.Subscribe<OnStageSelectButtonPressed>(_ => ResetButtonImage());
                if (GamePrefsRepository.CurrentPlayMap == _id)
                {
                    _isPressed = true;
                    _buttonImage.sprite = _pressedButton;
                }
                break;
            default:
                ResetButtonImage();
                break;
        }
    }

    public override void OnButtonPressed()
    {
        if (_isPressed) return;

        switch (_buttonType)
        {
            case ButtonType.PlayerSelect:
                EventBus.Instance.Publish(new OnPlayerSelectButtonPressed());
                break;
            case ButtonType.StageSelect:
                EventBus.Instance.Publish(new OnStageSelectButtonPressed());
                break;
        }
        _isPressed = true;
        GameManager.Instance.Switch(_buttonType, _id);
        _buttonImage.sprite = _pressedButton;
    }
    public void ResetButtonImage()
    {
        _isPressed = false;
        _buttonImage.sprite = _basicButton;
    }

}
