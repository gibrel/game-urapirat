using UnityEngine;

public class PlayerPreferences : MonoBehaviour
{
    private float volume;
    private int spawnFreq;
    private int gameTime;

    public float Volume { get => volume; set { if (value is >= 0f and <= 1f) { volume = value; SavePlayerPreferences(); } } }
    public int SpawnFreq { get => spawnFreq; set { if (value is >= 3 and <= 10) { spawnFreq = value; SavePlayerPreferences(); } } }
    public int GameTime { get => gameTime; set { if (value is >= 1 and <= 3) { gameTime = value; SavePlayerPreferences(); } } }

    private void Awake()
    {
        LoadPlayerPreferences();
    }

    public void SavePlayerPreferences()
    {
        GameAssets.Instance.audioMixer.GetFloat("volume", out float volume);
        volume = Mathf.InverseLerp(-80f,20f,volume);

        PlayerPrefs.SetFloat("volume", volume);
        PlayerPrefs.SetInt("spawnFrequency", spawnFreq);
        PlayerPrefs.SetInt("gameTime", gameTime);
    }

    private void LoadPlayerPreferences()
    {
        volume = PlayerPrefs.GetFloat("volume");
        spawnFreq = PlayerPrefs.GetInt("spawnFrequency");
        gameTime = PlayerPrefs.GetInt("gameTime");

        GameAssets.Instance.audioMixer.SetFloat("volume",Mathf.Lerp(-80f,20f,volume));
    }
}
