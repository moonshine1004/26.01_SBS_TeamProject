using System.Collections;
using Game.Events;
using UnityEngine;
using UnityEngine.UI;

public class ButtonView : MonoBehaviour
{
    protected Image _buttonImage;
    protected Sprite _basicButton;
    private bool _canPress = true;
    [SerializeField] protected Sprite _pressedButton;

    private void Awake()
    {
        _buttonImage = GetComponent<Image>();
        _basicButton = _buttonImage.sprite;
        EventBus.Instance.Subscribe<OnRestartGame>(_ => ActiveButton());
        EventBus.Instance.Subscribe<OnGameClear>(_ => InactiveButton());
        EventBus.Instance.Subscribe<OnGameOver>(_ => InactiveButton());
    }

    public virtual void OnButtonPressed()
    {
        if (!_canPress) return;
        StartCoroutine(ButtonAnimation());
    }
    public IEnumerator ButtonAnimation()
    {
        _buttonImage.sprite = _pressedButton;
        yield return new WaitForSeconds(0.15f);
        _buttonImage.sprite = _basicButton;
    }
    public void ActiveButton()
    {
        _buttonImage.sprite = _basicButton;
        _canPress = true;
    }
    public void InactiveButton()
    {
        _buttonImage.sprite = _basicButton;
        _canPress = false;
    }
}