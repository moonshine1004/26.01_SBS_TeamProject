using UnityEngine;
using UnityEngine.UI;

public class UIView : MonoBehaviour
{
    private UIPresenter _uiPresenter;
    
    public void InitUIView(UIPresenter uiPresenter)
    {
        _uiPresenter = uiPresenter;
    }
    
    private PlayerPresenter _playerPresenter;
    
    
    
    
    
    
    [SerializeField] private Slider _healthBar;


    public void Start()
    {
    
    }
    public void Update()
    {
        UpdateHealthBar(PlayerPresenter.Instance.UpdateHP(), PlayerPresenter.Instance.GetMaxHP());
    }






    public void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        _healthBar.value = (float)currentHealth / maxHealth;
    }
}