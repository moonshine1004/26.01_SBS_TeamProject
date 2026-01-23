using UnityEngine;

public class StairSizeDebugger : MonoBehaviour
{
    void Start()
    {
        var sr = GetComponent<SpriteRenderer>();
        Debug.Log($"계단 가로 길이: {sr.bounds.size.x}");
        Debug.Log($"계단 세로 길이: {sr.bounds.size.y}");
    }
}
