using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonView : MonoBehaviour
{
    private Image _buttonImage;
    private Sprite _basicButton;
    [SerializeField] private Sprite _pressedButton;

    private void Awake()
    {
        _buttonImage = GetComponent<Image>();
        _basicButton = _buttonImage.sprite;
    }

    public void OnButtonPressed()
    {
        StartCoroutine(ButtonAnimation());
    }
    public IEnumerator ButtonAnimation()
    {
        _buttonImage.sprite = _pressedButton;
        yield return new WaitForSeconds(0.15f);
        _buttonImage.sprite = _basicButton;
    }
}