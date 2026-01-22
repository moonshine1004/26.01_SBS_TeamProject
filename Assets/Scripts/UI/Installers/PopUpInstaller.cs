using UnityEngine;

public class PopUpInstaller : MonoBehaviour
{
    [SerializeField] private GameObject _startButton;
    private UIButtonPresenter uIButtonPresenter;

    private void Awake()
    {
        uIButtonPresenter = new UIButtonPresenter(_startButton.GetComponent<IUIButtonView>());
    }
}