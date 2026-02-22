using UnityEngine;
using Game.Events;

public static class QuitService 
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Init()
    {
        EventBus.Instance.Subscribe<OnQuitGame>(_ => Quit());
    }


    public static void Quit()
    {
        PlayerPrefs.Save();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
