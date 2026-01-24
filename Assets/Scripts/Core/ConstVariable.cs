using UnityEngine;

public class ConstVariable : MonoBehaviour
{
    private static ConstVariable _instance;
    public static ConstVariable Instance
    {
        get
        {
            if (_instance == null)
            {
                    _instance = FindFirstObjectByType<ConstVariable>();
            }
            return _instance;
        }
    }
    
    public const float xDistance = 2.44f;
    public const float yDistance = 1.22f;
    
    public const float startAnimationTime = 1f;

    public const int tilesOnFloor = 20;
}