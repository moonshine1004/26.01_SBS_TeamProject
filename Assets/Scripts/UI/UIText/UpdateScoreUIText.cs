using TMPro;
using UnityEngine;

public class UpdateScoreUIText : MonoBehaviour
{
    [SerializeField] private TextType _textType;
    private void Update()
    {
        var text = GetComponent<TextMeshProUGUI>();
        switch (_textType)
        {
            case TextType.CurrentScore:
                text.text = $"{ScoreManager.Instance.CurrentScore}";
                break;
            case TextType.HighScore:
                text.text = $"{ScoreManager.Instance.HighScore}";
                break;
        }
    }
}