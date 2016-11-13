using UnityEngine;
using System.Collections;

public static class SoundManager {
    
    private static string soundsPath = "Sound/";

	public static void PlaySound(string soundName, bool loop = false, float volume = 1f)
    {
        AudioClip _clip = Resources.Load<AudioClip>(soundsPath + soundName);
        PlaySound(_clip, loop, volume);
    }

    public static void PlaySound(AudioClip sound, bool loop = false, float volume = 1f)
    {
        GameObject _newSoundObject = new GameObject(sound.name);
        _newSoundObject.transform.position = Vector2.zero;
        _newSoundObject.tag = "Sound";

        AudioSource _audioSource = _newSoundObject.AddComponent<AudioSource>();
        _audioSource.playOnAwake = true;
        _audioSource.clip = sound;
        _audioSource.volume = volume;

        if (loop)
        {
            _audioSource.loop = true;
        }
        else
        {
            _newSoundObject.AddComponent<DieAfterFinishedSound>();
        }

        GameObject.Instantiate(_newSoundObject);
    }

    public static void StopSound(string soundName)
    {
        GameObject.Destroy(GameObject.Find(soundName));
    }

    public static void StopSound(AudioClip sound)
    {
        GameObject.Destroy(GameObject.Find(sound.name));
    }

    public static void StopMusic(string musicName)
    {
        StopSound(musicName);
    }

    public static void StopMusic(AudioClip music)
    {
        StopSound(music);
    }

    public static void StopAllSounds()
    {
        GameObject[] sounds = GameObject.FindGameObjectsWithTag("Sound");

        foreach(GameObject sound in sounds)
        {
            GameObject.Destroy(sound);
        }
    }
}
