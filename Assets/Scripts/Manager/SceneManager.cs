using UnityEngine;

public class SceneManager : MonoBehaviour
{
    private static SceneManager _instance;
    public static SceneManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<SceneManager>();
            }
            return _instance;
        }
    }


    #region Unity Lifecycle
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        
    }
    #endregion

}