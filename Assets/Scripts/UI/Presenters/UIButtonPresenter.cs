public class UIButtonPresenter
{
    public UIButtonPresenter(IUIButtonView startButton)
    {
        startButton.onPressed += Test;
    }
    private void Test()
    {
        
    }
}