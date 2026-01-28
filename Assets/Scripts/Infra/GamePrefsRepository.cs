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
        get
        {
            switch (CurrentPlayMap)
            {
                case 1:
                    return UnityEngine.PlayerPrefs.GetInt("Map1HighScore", 0);
                case 2:
                    return UnityEngine.PlayerPrefs.GetInt("Map2HighScore", 0);
                case 3:
                    return UnityEngine.PlayerPrefs.GetInt("Map3HighScore", 0);
                default:
                    return 0;
            }
        }
        set
        {
            switch (CurrentPlayMap)
            {
                case 1:
                    UnityEngine.PlayerPrefs.SetInt("Map1HighScore", value);
                    break;
                case 2:
                    UnityEngine.PlayerPrefs.SetInt("Map2HighScore", value);
                    break;
                case 3:
                    UnityEngine.PlayerPrefs.SetInt("Map3HighScore", value);
                    break;
            }
            UnityEngine.PlayerPrefs.Save();
        }
    }
    public static float BgmVolume
    {
        get => UnityEngine.PlayerPrefs.GetFloat("BgmVolume", 1f);
        set
        {
            UnityEngine.PlayerPrefs.SetFloat("BgmVolume", value);
            UnityEngine.PlayerPrefs.Save();
        }
    }
    public static float SfxVolume
    {
        get => UnityEngine.PlayerPrefs.GetFloat("SfxVolume", 1f);
        set
        {
            UnityEngine.PlayerPrefs.SetFloat("SfxVolume", value);
            UnityEngine.PlayerPrefs.Save();
        }
    }
}