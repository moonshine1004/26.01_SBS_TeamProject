using UnityEngine;
using UnityEngine.UI;
using System;

public class UIView : MonoBehaviour
{
    private IUIPresenter _uiPresenter;
    
    public void InitUIView(IUIPresenter uiPresenter)
    {
        _uiPresenter = uiPresenter;
    }
    
    #region References
    private IPlayerPresenter _playerPresenter;
    #endregion
    
    
    
    
    
    [SerializeField] private Slider _healthBar;
    
    public void OnFlipButtonInput()
    {
        _uiPresenter.OnFlip();   
    }
    public void OnMoveButtonInput()
    {
        _uiPresenter.OnMove();
    }





    public void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        _healthBar.value = (float)currentHealth / maxHealth;
    }
}