using TMPro;
using UnityEngine;
using Game.Events;

public class UpdateInGameUIText : MonoBehaviour
{
    [SerializeField] private TextType _textType;
    private void Awake()
    {
        EventBus.Instance.Subscribe<OnUpdateTileScore>(_ => OnUpdateUIText());
        EventBus.Instance.Subscribe<OnTimeChange>(_ => OnUpdateUIText());
    }
    private void OnUpdateUIText()
    {
        var text = GetComponent<TextMeshProUGUI>();
        switch (_textType)
        {
            case TextType.TileScore:
                text.text = $"{ScoreManager.Instance.TileScore}";
                break;
            case TextType.Timer:
                text.text = $"{GameStageManager.Instance.RemainingTime}";
                break;
        }
    }
}