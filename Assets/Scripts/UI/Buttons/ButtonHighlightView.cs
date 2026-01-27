using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHighlightView : MonoBehaviour
{
    private Image _basicButtonImage;
    [SerializeField] private Sprite _basicButtonSprite;
    [SerializeField] private Sprite _highlightButtonSprite;
    [SerializeField] private float _interval = 0.5f;

    private void Awake()
    {
        _basicButtonImage = GetComponent<Image>();
        _basicButtonSprite = GetComponent<Image>().sprite;
        StartCoroutine(Blink());
    }

    private IEnumerator Blink()
    {
        while (true)
        {
            _basicButtonImage.sprite = _highlightButtonSprite;
            yield return new WaitForSeconds(_interval);
            _basicButtonImage.sprite = _basicButtonSprite;
            yield return new WaitForSeconds(_interval);
        }
    }
}
