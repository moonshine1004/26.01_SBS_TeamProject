using UnityEngine;
using UnityEngine.InputSystem;

public interface IPlayerView
{
    void InitPlayerView(IPlayerPresenter playerPresenter);
    void forDebug(string msg);
    void SetDiraction();
    void SetPosition();
    void SetStartPosition();
}

public class PlayerView : MonoBehaviour, IPlayerView
{
    private IPlayerPresenter _playerPresenter;
    
    #region Components
    private PlayerInput _playerInput;
    private InputAction _touchedPosition;
    #endregion
    #region Fields
    private int _xMove = 1;
    private int _yMove = 1;
    private Vector3 _targetPosition;
    private bool _isLeft = true;
    #endregion


    public void InitPlayerView(IPlayerPresenter playerPresenter)
    {
        _playerPresenter = playerPresenter;
    }

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, 10f * Time.deltaTime);
    }




    #region IPlayerView Interface Implemetation
    public void SetDiraction()
    {
        _isLeft = !_isLeft;
        transform.localScale = -transform.localScale; // 애니메이터 추가에 따라 수정 요망
    }
    public void SetPosition()
    {
        if(_isLeft)
        {
            _targetPosition = transform.position + new Vector3(-_xMove, -_yMove, 0);
            // this.gameObject.transform.position += new Vector3(-_xMove, -_yMove, 0);
        } 
        else if(!_isLeft)
        {
            _targetPosition = transform.position + new Vector3(_xMove, -_yMove, 0);
            // this.gameObject.transform.position += new Vector3(_xMove, -_yMove, 0);
        }
    }

    public void forDebug(string msg)
    {
        Debug.Log(msg);
    }

    public void SetStartPosition()
    {
        _isLeft = true;
        _targetPosition = new Vector3(0, 0, 0);
        transform.position = new Vector3(0, 0, 0);
    }
    #endregion
}
