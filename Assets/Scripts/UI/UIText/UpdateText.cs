using UnityEngine;

public enum TextType
{
    HighScore,
    CurrentScore
}

public class UpdateText : MonoBehaviour
{
    [SerializeField] private TextType _textType;
    private void OnEnable()
    {
        var text = GetComponent<UnityEngine.UI.Text>();
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
