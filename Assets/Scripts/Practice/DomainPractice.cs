using System;
using System.Collections.Generic;
using UnityEngine;

using UseCase.Player;

namespace Domain.Player
{
    public enum Side{ left, right }
    public class WalkDomain
    {
        public Vector2 position = new Vector2(2, 0);
        public Side facing = Side.left;
        public bool ApplyMove(Side input, out string eventName)
        {
            facing = input;
            // 이동 가능한지 체크해서 맞으면
            if (true)
            {
                position += input == Side.left ? Vector2.left : Vector2.right;
                eventName = "MoveSuccess";
                return true;
            }
            else
            {
                eventName = "MoveFailed";
                return false;
            }
        }
    }

    public class BootStrap : MonoBehaviour
    {
        public GameServices Services { get; private set; }
        [SerializeField] private PlayerView _playerView;

        private void Awake()
        {
            // Infra

            // Domain
            var walkDomain = new WalkDomain();
            // Application
            var moveUseCase = new MoveUseCase(walkDomain);

            Services = new GameServices();
            Services.Register(moveUseCase);
            _playerView.Install(moveUseCase);
        }
    }
    public class GameServices
    {
        private readonly Dictionary<Type, object> _services = new();

        public void Register<T>(T instance) where T : class
            => _services[typeof(T)] = instance;

        public T Get<T>() where T : class
            => (T)_services[typeof(T)];
    }

    public class PlayerView : MonoBehaviour
    {
        public void Install(MoveUseCase moveUseCase)
        {
            _moveUseCase = moveUseCase;
            _moveUseCase.OnMoveEvent += OnDomainEvent;
        }
        private MoveUseCase _moveUseCase;
        private const float _xDistance = 2.44f;
        private const float _yDistance = -1.22f;

        private void OnDomainEvent(string eventName)
        {
            switch (eventName)
            {
                case "MoveSuccess":
                    if(_moveUseCase.Facing == Side.left)
                        transform.position += new Vector3(-_xDistance, _yDistance, 0);
                    else
                        transform.position += new Vector3(_xDistance, _yDistance, 0);
                    break;
                case "MoveFailed":
                    
                    break;
            }
        }

    }
}