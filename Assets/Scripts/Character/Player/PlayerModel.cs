using UnityEngine;

public enum PlayerType
{
    Player01,
    Player02
}


public class PlayerModel
{
    private string _name;
    private bool _isAlive = true;
    private readonly int _maxHP;
    private int _hp;

    public bool IsAlive{ get => _isAlive; set => _isAlive = value; }
    public int HP
    {
        get => _hp;
        set 
        {
            _hp = value;
            if (_hp <= 0)
            {
                IsAlive = false;
            }
        }
    }

    public int MaxHP => _maxHP;

    public PlayerModel(int maxHP)
    {
        _maxHP = maxHP;
    }

}