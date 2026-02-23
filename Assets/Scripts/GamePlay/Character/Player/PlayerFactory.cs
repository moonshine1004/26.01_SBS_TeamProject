public sealed class PlayerFactory
{
    public (PlayerModel model, IPlayerPresenter presenter) CreatePlayer(IPlayerView view)
    {
        var model = new PlayerModel();
        var presenter = new PlayerPresenter(model, view);
        return (model, presenter);
    }

    
}