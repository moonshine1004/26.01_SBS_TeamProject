using System;
using System.Collections.Generic;
using UnityEngine;

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

    public class MoveUseCase
    {
        private readonly WalkDomain _domain;
        public Action<string> OnMove; // 프레젠테이션에 이벤트 전달

        public MoveUseCase(WalkDomain domain)
        {
            _domain = domain;
        }

        public void Move(Side input)
        {
            bool canMove = _domain.ApplyMove(input, out var moveApplyEvent);

            OnMove?.Invoke(moveApplyEvent); // 프레젠테이션에 이벤트 전달
        }

        public Side Facing => _domain.facing;
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
            _moveUseCase.OnMove += OnDomainEvent;
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