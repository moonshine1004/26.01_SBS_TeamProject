using System;
using UnityEngine;

public interface IUIButtonView
{
    public event Action onPressed;
    void OnPressed();
}

public class UIButtonView : MonoBehaviour, IUIButtonView
{
    public event Action onPressed;
    
    public void OnPressed()
    {
        onPressed.Invoke();
    }
}