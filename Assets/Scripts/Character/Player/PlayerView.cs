using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IPlayerView
{
    void InstallPlayerView(IPlayerPresenter playerPresenter, RuntimeAnimatorController animatorController);
    void forDebug(string msg);
    void SetDiraction();
    void SetPosition();
    void SetStartGame();
    void SetRestartGame();
    void SetReturnLobby();
    void SetDeath();
}

public class PlayerView : MonoBehaviour, IPlayerView
{
    private IPlayerPresenter _playerPresenter;
    
    #region Components
    private PlayerInput _playerInput;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private RuntimeAnimatorController _animatorController;
    [SerializeField] private CinemachineFollow _cinemachineFollow;
    #endregion
    #region Fields
    private float _xMove = ConstVariable.xDistance;
    private float _yMove = ConstVariable.yDistance;
    private bool _canMove = false;
    private Vector3 _lobbyPosition;
    private Vector3 _sceneStartPosition;
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
        _cinemachineFollow.FollowOffset = new Vector3(0, 1.5f, -10);
        _playerInput = GetComponent<PlayerInput>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        _lobbyPosition = transform.position;
        _animator.runtimeAnimatorController = _animatorController;
    }
    private void Update()
    {
        if (_canMove)
        {
            _canMove = false;
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, 40f * Time.deltaTime);
            _canMove = true;
        }
        
    }
    #endregion

    #region Methods
    private IEnumerator StartGameAnimation(Vector3 targetPosition, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            _cinemachineFollow.FollowOffset = Vector3.Lerp(new Vector3(0, 1.5f, -10), new Vector3(0, -4.5f, -10), t);
            transform.position = Vector3.Lerp(_lobbyPosition, targetPosition, t);
            yield return null;
        }
        transform.position = targetPosition;
        _sceneStartPosition = new Vector3(-0.15f, 1.07f, 0);
        _targetPosition = new Vector3(-0.15f, 1.07f, 0);
        _animator.SetBool("isRunning", false);
        _canMove = true;
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
        if(_canMove == false) return;
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

    public void SetRestartGame()
    {
        _isLeft = true;
        _targetPosition = _sceneStartPosition;
        transform.position = _sceneStartPosition;
        _spriteRenderer.flipX = !_isLeft;
        _animator.SetBool("isDeath", false);
    }
    public void SetReturnLobby()
    {
        transform.position = _lobbyPosition;
    }
    public void SetStartGame()
    {
        _animator.SetBool("isRunning", true);
        StartCoroutine(StartGameAnimation(new Vector3(-0.15f, 1.07f, 0), ConstVariable.startAnimationTime));
    }

    public void SetDeath()
    {
        _animator.SetBool("isDeath", true);
    }
    #endregion
}
