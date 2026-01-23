using UnityEngine;
using UnityEngine.UI;

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
        if(_isPressed)
        {
            OnButtonPressed();
        }
    }

    public override void OnButtonPressed()
    {
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
