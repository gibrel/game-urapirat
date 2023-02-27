using UnityEngine;
using UnityEngine.Audio;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _instance;

    //[Header("Music")]
    public MusicAudioClip[] musicAudioClips;

    //[Header("Sound FX")]
    public SoundAudioClip[] soundAudioClips;

    public AudioMixer audioMixer;

    public static GameAssets Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (Instantiate(Resources.Load("GameAssets")) as GameObject).GetComponent<GameAssets>();
            }
            return _instance;
        }
    }

    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }

    [System.Serializable]
    public class MusicAudioClip
    {
        public SoundManager.Music music;
        public AudioClip audioClip;
    }
}
