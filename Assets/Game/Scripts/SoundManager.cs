using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static SoundManager;

public static class SoundManager
{
    public enum Sound
    {
        CannonFire,
        CannonballExplosion,
        ShipExplosion,
        ShipWoodCreek,
        ButtonClick,
        ButtonOver,
        ButtonSwitch
    }
    public enum Music
    {
        MenuMusic,
        GameMusic
    }

    private static Dictionary<Sound, float> soundTimerDictionary = new Dictionary<Sound, float>();
    private static Dictionary<Sound, float> soundMaxDurationDictionary = new Dictionary<Sound, float>();
    private static GameObject oneShotGameObject;
    private static AudioSource oneShotAudioSource;

    private static Dictionary<Music, float> musicTimerDictionary = new Dictionary<Music, float>();
    private static Dictionary<Music, float> musicMaxDurationDictionary = new Dictionary<Music, float>();
    private static GameObject musicGameObject;
    private static AudioSource musicAudioSource;

    public static void PlayMusic(Music music)
    {
        if (CanPlayMusic(music))
        {
            if (musicGameObject == null)
            {
                musicGameObject = new GameObject("Music Player");
                musicAudioSource = musicGameObject.AddComponent<AudioSource>();
            }
            musicAudioSource.clip = GetAudioClip(music);
            musicAudioSource.loop = true;
            musicAudioSource.volume = GetVolume();
            musicAudioSource.Play();
        }
    }

    public static void PlaySound(Sound sound)
    {
        if (CanPlaySound(sound))
        {
            if (oneShotGameObject == null)
            {
                oneShotGameObject = new GameObject("One Shot Sound");
                oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
            }
            oneShotAudioSource.volume = GetVolume();
            oneShotAudioSource.PlayOneShot(GetAudioClip(sound));
        }
    }

    public static void PlaySound(Sound sound, Vector3 position)
    {
        if (CanPlaySound(sound))
        {
            GameObject soundGameObject = new GameObject("Sound");
            soundGameObject.transform.position = position;
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.clip = GetAudioClip(sound);
            audioSource.maxDistance = 100f;
            audioSource.spatialBlend = 1f;
            audioSource.rolloffMode = AudioRolloffMode.Linear;
            audioSource.dopplerLevel = 0f;
            audioSource.volume = GetVolume();
            audioSource.Play();

            Object.Destroy(soundGameObject, audioSource.clip.length);
        }
    }

    private static void AddAudioClipDicts(Music music)
    {
        if (ExistsAudioClip(music))
        {
            musicTimerDictionary[music] = Time.time;
            musicMaxDurationDictionary[music] = GetAudioClipMaximumDuration(music);
        }
    }

    private static void AddAudioClipDicts(Sound sound)
    {
        if (ExistsAudioClip(sound))
        {
            soundTimerDictionary[sound] = Time.time;
            soundMaxDurationDictionary[sound] = GetAudioClipMaximumDuration(sound);
        }
    }

    private static bool CanPlayMusic(Music music)
    {
        if (musicTimerDictionary.ContainsKey(music))
        {
            float lastTimePlayed = musicTimerDictionary[music];
            if (lastTimePlayed + musicMaxDurationDictionary[music] < Time.time)
            {
                musicTimerDictionary[music] = Time.time;
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            AddAudioClipDicts(music);
            return true;
        }
    }

    private static bool CanPlaySound(Sound sound)
    {
        switch (sound)
        {
            case Sound.ShipWoodCreek:
                if (soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    if (lastTimePlayed + soundMaxDurationDictionary[sound] < Time.time)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    AddAudioClipDicts(sound);
                    return true;
                }
            default: return true;
        }
    }

    private static float GetAudioClipMaximumDuration(Music music)
    {
        var soundAudioClip = GameAssets.Instance.musicAudioClips.Where(m => m.music == music).OrderByDescending(s => s.audioClip.length).First();

        if (soundAudioClip == null)
        {
            Debug.LogError("Music " + music + " could not be found!");
            return 0;
        }

        return soundAudioClip.audioClip.length;
    }

    private static float GetAudioClipMaximumDuration(Sound sound)
    {
        var soundAudioClip = GameAssets.Instance.soundAudioClips.Where(s => s.sound == sound).OrderByDescending(s => s.audioClip.length).First();

        if (soundAudioClip == null)
        {
            Debug.LogError("Sound " + sound + " could not be found!");
            return 0;
        }

        return soundAudioClip.audioClip.length;
    }

    private static AudioClip GetAudioClip(Music music)
    {
        var audioClips = GameAssets.Instance.musicAudioClips.Where(m => m.music == music).ToArray();

        if (audioClips.Length == 0)
        {
            Debug.LogError("Music " + music + " could not be found!");
            return null;
        }

        return audioClips[Random.Range(0, audioClips.Length)].audioClip;
    }

    private static AudioClip GetAudioClip(Sound sound)
    {
        var audioClips = GameAssets.Instance.soundAudioClips.Where(s => s.sound == sound).ToArray();

        if (audioClips.Length == 0)
        {
            Debug.LogError("Sound " + sound + " could not be found!");
            return null;
        }

        return audioClips[Random.Range(0, audioClips.Length)].audioClip;
    }

    private static bool ExistsAudioClip(Sound sound) => GameAssets.Instance.soundAudioClips.Any(s => s.sound == sound);

    private static bool ExistsAudioClip(Music music) => GameAssets.Instance.musicAudioClips.Any(m => m.music == music);

    private static float GetVolume()
    {
        GameAssets.Instance.audioMixer.GetFloat("volume", out float volume);
        volume = Mathf.InverseLerp(-80f, 20f, volume);
        return volume;
    }
}
