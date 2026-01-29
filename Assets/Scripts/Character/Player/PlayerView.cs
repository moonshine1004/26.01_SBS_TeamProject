using System.Collections;
using System.Threading.Tasks;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IPlayerView
{
    void InstallPlayerView(IPlayerPresenter playerPresenter, RuntimeAnimatorController animatorController);
    void SetDiraction();
    Task SetPosition();
    void SetStartGame();
    Task SetClearGame();
    void SetRestartGame();
    void SetReturnLobby();
    Task SetDeath();
    void SetCameraFollowOff(bool isLeft);
}

public class PlayerView : MonoBehaviour, IPlayerView
{
    private IPlayerPresenter _playerPresenter;
    
    #region Components
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private CinemachineFollow _cinemachineFollow;
    [SerializeField] private CinemachineCamera _cinemachineCamera;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _deathSound;
    [SerializeField] private AudioClip _clearSound;
    #endregion
    #region Fields
    private float _xMove = ConstVariable.xDistance;
    private float _yMove = ConstVariable.yDistance;
    private bool _canMove = false;
    private bool _isMoving = false;
    private TaskCompletionSource<bool> _moveTcs;
    private Vector3 _lobbyPosition;
    private Vector3 _sceneStartPosition;
    private Vector3 _targetPosition;
    private bool _isLeft = true;
    #endregion


    public void InstallPlayerView(IPlayerPresenter playerPresenter, RuntimeAnimatorController animatorController)
    {
        _playerPresenter = playerPresenter;
        _animator.runtimeAnimatorController = animatorController;
    }
    #region Unity LifeCycle
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        _lobbyPosition = transform.position;
        _cinemachineFollow.FollowOffset = new Vector3(0, 1.7f, -10);
    }
    private void Update()
    {
        Move(); 
    }
    #endregion

    #region Methods
    private void Move()
    {
        if(_isMoving == false) return;
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, 40f * Time.deltaTime);
        if (Vector3.SqrMagnitude(transform.position - _targetPosition) < 0.0001f)
        {
            transform.position = _targetPosition;
            _moveTcs.TrySetResult(true);
            _moveTcs = null;
            _isMoving = false;
        }
    }
    private IEnumerator MoveCameraToTarget(Vector3 targetPosition)
    {
        while (Vector3.Distance(_cinemachineCamera.transform.position, targetPosition) > 0.0001f)
        {
            _cinemachineCamera.transform.position = Vector3.MoveTowards(_cinemachineCamera.transform.position, targetPosition, 40f * Time.deltaTime);
            yield return null;
        }
        _cinemachineCamera.transform.position = targetPosition;
    }
    private IEnumerator StartGameAnimation(Vector3 targetPosition, float duration)
    {
        new WaitForSeconds(1f);
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            _cinemachineFollow.FollowOffset = Vector3.Lerp(new Vector3(0, 1.7f, -10), new Vector3(0, -4.5f, -10), t);
            transform.position = Vector3.Lerp(_lobbyPosition, targetPosition, t);
            yield return null;
        }
        transform.position = targetPosition;
        _sceneStartPosition = new Vector3(-0.15f, 1.07f, 0);
        _targetPosition = new Vector3(-0.15f, 1.07f, 0);
        _animator.SetBool("isRunning", false);
        _canMove = true;
    }
    private IEnumerator GameClearAnimation()
    {
        if(_isLeft == false) SetDiraction();
        _animator.SetBool("isRunning", true);
        float distance = Vector3.Distance(transform.position, new Vector3(-10, transform.position.y, 0));
        float elapsed = 0f;
        float duration = distance/10f;
        Vector3 startPos = transform.position;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            transform.position = Vector3.Lerp(startPos, new Vector3(-10, transform.position.y, 0), t);
            yield return null;
        }
        transform.position = new Vector3(-10, transform.position.y, 0);
        _moveTcs.TrySetResult(true);
        _moveTcs = null;
        _canMove = true;
    }
    private async Task DeathAnimation()
    {
        _animator.SetBool("isDeath", true);
        _cinemachineCamera.Follow = null;
        Vector3 startPos = transform.position;
        Vector3 targetPos = startPos + Vector3.down * 15f;
        float distance = Vector3.Distance(startPos, targetPos);
        float duration = distance / 10f;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            transform.position = Vector3.Lerp(startPos, targetPos, t);
            await Task.Yield();
        }
        _moveTcs = null;
        _canMove = true;
    }
    #endregion

    #region IPlayerView Interface Implemetation
    public void SetDiraction()
    {
        _isLeft = !_isLeft;
        _spriteRenderer.flipX = !_isLeft;
    }
    public Task SetPosition()
    {
        if(_canMove == false) return Task.FromCanceled(new System.Threading.CancellationToken(canceled: true));
        if (_isMoving) return _moveTcs.Task ?? Task.CompletedTask;
        if(_isLeft)
        {
            _targetPosition = transform.position + new Vector3(-_xMove, -_yMove, 0);
            _isMoving = true;
            _animator.SetTrigger("isMoving");
            _moveTcs = new TaskCompletionSource<bool>();
            return _moveTcs.Task;
        } 
        else if(!_isLeft)
        {
            _targetPosition = transform.position + new Vector3(_xMove, -_yMove, 0);
            _isMoving = true;
            _animator.SetTrigger("isMoving");
            _moveTcs = new TaskCompletionSource<bool>();
            return _moveTcs.Task;
        }
        return _moveTcs.Task;
    }

    public void SetRestartGame()
    {
        _isLeft = true;
        _targetPosition = _sceneStartPosition;
        transform.position = _sceneStartPosition;
        _spriteRenderer.flipX = !_isLeft;
        _animator.SetBool("isDeath", false);
        _animator.SetBool("isRunning", false);
        _cinemachineCamera.Follow = transform;
        _cinemachineFollow.FollowOffset = new Vector3(0, -4.5f, -10);
    }
    public void SetReturnLobby()
    {
        _animator.SetBool("isRunning", false);
        _animator.SetBool("isDeath", false);
        if(_isLeft == false) SetDiraction();
        transform.position = _lobbyPosition;
        _cinemachineCamera.Follow = transform;
        _cinemachineFollow.FollowOffset = new Vector3(0, 1.7f, -10);
    }
    public void SetStartGame()
    {
        _animator.SetBool("isRunning", true);
        StartCoroutine(StartGameAnimation(new Vector3(-0.15f, 1.07f, 0), ConstVariable.startAnimationTime));
    }
    public async Task SetDeath()
    {
        _audioSource.PlayOneShot(_deathSound);
        await DeathAnimation();
        _animator.SetBool("isDeath", true);
    }
    public void SetCameraFollowOff(bool isLeft)
    {
        if(_canMove == false) return;
        _cinemachineCamera.Follow = null;
        if(isLeft)
        {
            StartCoroutine(MoveCameraToTarget(_cinemachineCamera.transform.position + new Vector3(-_xMove,0, 0)));
        }
        else if(!isLeft)
        {
            StartCoroutine(MoveCameraToTarget(_cinemachineCamera.transform.position + new Vector3(_xMove,0, 0)));
        }
    }
    public Task SetClearGame()
    {
        _audioSource.PlayOneShot(_clearSound);
        _canMove = false;
        _cinemachineFollow.FollowOffset = new Vector3(0, 4.05f, -10);
        _cinemachineCamera.Follow = transform;
        _moveTcs = new TaskCompletionSource<bool>();
        StartCoroutine(GameClearAnimation());
        return _moveTcs.Task;
    }
    #endregion
}
