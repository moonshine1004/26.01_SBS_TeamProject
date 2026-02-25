using System;
using System.Collections.Generic;
using Domain.Player;
using UnityEngine;

namespace UseCase.Player
{
    public class MoveUseCase
    {
        private readonly WalkDomain _domain;
        public Action<string> OnMoveEvent; // 프레젠테이션에 이벤트 전달

        public MoveUseCase(WalkDomain domain)
        {
            _domain = domain;
        }

        public void Move(Side input)
        {
            bool canMove = _domain.ApplyMove(input, out var moveApplyEvent);

            OnMoveEvent?.Invoke(moveApplyEvent); // 프레젠테이션에 이벤트 전달
        }

        public Side Facing => _domain.facing;
    }

}

namespace Domain.TileDrawer
{
    public class TileDrawer
    {
        private List<Vector2> _tileGrid = new List<Vector2>();
        private int minX = 0;
        private int maxX = 10;
        private int _tilePoolIndex = 0;

        public void CreateTiles() // 논리적 위치 생성
        {
            int x = 2; // 시작 칸
            for (int y = 0; y > -_tilePoolIndex; y--)
            {
                _tileGrid.Add(new Vector2Int(x, y));

                if (x <= minX)
                    x++;
                else if (x >= maxX)
                    x--;
                else
                    x += UnityEngine.Random.Range(0, 2) == 0 ? -1 : 1;
            }
        }
        public bool CheckTile(Vector2 position)
        {
            return _tileGrid.Contains(position);
        }
    }
}