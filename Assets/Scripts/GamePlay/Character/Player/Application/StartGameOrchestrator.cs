using System.Collections;
using UnityEngine;

public class StartGameOrchestrator
{
    public StartGameOrchestrator(PlayerView playerView, IPlayerMoveUseCase playerMoveService)
    {
        _playerMoveService = playerMoveService;
        _playerView = playerView;
    }

    #region Feilds
    private readonly PlayerView _playerView;
    private readonly IPlayerMoveUseCase _playerMoveService;
    #endregion

    public void Execute()
    {
        var playerMovePlan = _playerMoveService.Execute();
        // var playerCameraPlan = 
        //_playerView.ApplyPlayerPlan(playerMovePlan);
    }

    
}
