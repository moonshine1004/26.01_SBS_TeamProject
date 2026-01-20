using UnityEngine;
using UnityEngine.InputSystem;

public interface IPlayerView
{
    void InstallPlayerView(IPlayerPresenter playerPresenter, RuntimeAnimatorController animatorController);
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
    [SerializeField] private RuntimeAnimatorController _animatorController;
    #endregion
    #region Fields
    private float _xMove = ConstVariable.xDistance;
    private float _yMove = ConstVariable.yDistance;
    private bool _canMove = true;
    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private bool _isLeft = true;
    #endregion


    public void InstallPlayerView(IPlayerPresenter playerPresenter, RuntimeAnimatorController animatorController)
    {
        _playerPresenter = playerPresenter;
        _animatorController = animatorController;
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
        _animator.runtimeAnimatorController = _animatorController;
    }
    private void Update()
    {
        if (_canMove)
        {
            _canMove = false;
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, 30f * Time.deltaTime);
            _canMove = true;
        }
        
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
