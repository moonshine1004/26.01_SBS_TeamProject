public static class GamePrefsRepository
{
    public static int CurrentPlayPlayer
    {
        get => UnityEngine.PlayerPrefs.GetInt("CurrentPlayPlayer", 1);
        set
        {
            UnityEngine.PlayerPrefs.SetInt("CurrentPlayPlayer", value);
            UnityEngine.PlayerPrefs.Save();
        }
    }
    public static int CurrentPlayMap
    {
        get => UnityEngine.PlayerPrefs.GetInt("CurrentPlayMap", 1);
        set
        {
            UnityEngine.PlayerPrefs.SetInt("CurrentPlayMap", value);
            UnityEngine.PlayerPrefs.Save();
        }
    }
    public static int CurrentHighScore
    {
        get => UnityEngine.PlayerPrefs.GetInt("CurrentHighScore", 0);
        set
        {
            UnityEngine.PlayerPrefs.SetInt("CurrentHighScore", value);
            UnityEngine.PlayerPrefs.Save();
        }
    }
}