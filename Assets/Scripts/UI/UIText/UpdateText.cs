using TMPro;
using UnityEngine;

public enum TextType
{
    HighScore,
    CurrentScore,
    TileScore,
    Timer
}

public class UpdateScoreText : MonoBehaviour
{
    [SerializeField] private TextType _textType;
    private void OnEnable()
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
            case TextType.TileScore:
                break;
            case TextType.Timer:
                break;
            default:
                break;
        }
    }
}
