using UnityEngine;
using UnityEngine.UI;

public class ControllerSwitchButtonView : MonoBehaviour
{
    private Image _buttonImage;
    private Sprite _basicButton;
    [SerializeField] private Sprite _pressedButton;

    [SerializeField] private GameObject _flipButton;
    [SerializeField] private GameObject _moveButton;
    private Vector3 _flipButtonPosition;
    private Vector3 _moveButtonPosition;
    private bool _isSwitched = false;

    private void Awake()
    {
        _buttonImage = GetComponent<Image>();
        _basicButton = _buttonImage.sprite;
        _flipButtonPosition = _flipButton.transform.position;
        _moveButtonPosition = _moveButton.transform.position;
    }

    public void OnButtonPressed()
    {
        switch (_isSwitched)
        {
            case false:
                {
                    _buttonImage.sprite = _pressedButton;
                    _flipButton.transform.position = _moveButtonPosition;
                    _moveButton.transform.position = _flipButtonPosition;
                    _isSwitched = true;
                    break;
                }
            case true:
                {
                    _buttonImage.sprite = _basicButton;
                    _flipButton.transform.position = _flipButtonPosition;
                    _moveButton.transform.position = _moveButtonPosition;
                    _isSwitched = false;
                    break;
                }
        }
    }
}