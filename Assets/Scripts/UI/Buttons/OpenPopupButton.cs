using UnityEngine;

public class OpenPopupButton : MonoBehaviour
{
    [SerializeField] private CanvasType _canvasType;
    [SerializeField] private UIView _uiView;

    public void OnPressed()
    {
        _uiView.PopUpCanvas(_canvasType);
    } 
}