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
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    #endregion
    #region Fields
    private float _xMove = 2.5f;
    private float _yMove = 2.5f;
    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private bool _isLeft = true;
    #endregion


    public void InitPlayerView(IPlayerPresenter playerPresenter)
    {
        _playerPresenter = playerPresenter;
    }
    #region Unity LifeCycle
    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        _startPosition = transform.position;
        _targetPosition = _startPosition;
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, 10f * Time.deltaTime);
    }
    #endregion




    #region IPlayerView Interface Implemetation
    public void SetDiraction()
    {
        _isLeft = !_isLeft;
        _spriteRenderer.flipX = !_isLeft;
    }
    public void SetPosition()
    {
        if(_isLeft)
        {
            _targetPosition = transform.position + new Vector3(-_xMove, -_yMove, 0);
            _animator.SetTrigger("isMoving");
            // this.gameObject.transform.position += new Vector3(-_xMove, -_yMove, 0);
        } 
        else if(!_isLeft)
        {
            _targetPosition = transform.position + new Vector3(_xMove, -_yMove, 0);
            _animator.SetTrigger("isMoving");
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
        _targetPosition = _startPosition;
        transform.position = _startPosition;
        _spriteRenderer.flipX = !_isLeft;
    }
    #endregion
}
