using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource sfxPitchSource;
    [Header("BGM")]
    [SerializeField] private List<SoundClip> bgmClips = new List<SoundClip>();
    [Header("SFX")]
    [SerializeField] private List<SoundClip> sfxClips;

    public static AudioManager instance { get; private set; }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }
    public void PlaySound(string name, SoundOutput output)
    {
        PlaySound(name, output, false);
    }

    public void PlaySound(string name, SoundOutput output, bool forcedRestart)
    {
        if (output == SoundOutput.bgm)
        {
            AudioClip playingBgm = GetPlayingClip(bgmSource);
            if (playingBgm != null)
            {
                if (playingBgm.name == name)
                {
                    return;
                }
            }

            SoundClip s = bgmClips.Find((sound) => sound.name == name);
            SetSourceValues(s, bgmSource);
            if (!bgmSource.clip.Equals(playingBgm) || forcedRestart)
            {
                bgmSource.Play();
            }
        }
        else if (output == SoundOutput.sfx)
        {
            SoundClip s = sfxClips.Find((sound) => sound.name == name);
            SetSourceValues(s, sfxSource);
            sfxSource.PlayOneShot(s.Clip);
        }
    }

    public void PlaySound(string name, SoundOutput output, Vector2 randomPitch)
    {
        float pitch = UnityEngine.Random.Range(randomPitch.x, randomPitch.y);
        if (output == SoundOutput.bgm)
        {
            SoundClip s = bgmClips.Find((sound) => sound.name == name);
            SetSourceValues(s, bgmSource);
            bgmSource.pitch = pitch;
            bgmSource.Play();
        }
        else if (output == SoundOutput.sfx)
        {
            SoundClip s = sfxClips.Find((sound) => sound.name == name);
            SetSourceValues(s, sfxPitchSource);
            sfxPitchSource.pitch = pitch;
            sfxPitchSource.PlayOneShot(s.Clip);
        }
    }

    //public void PlaySound(AudioClip clip, SoundOutput output)
    //{
    //    if (output == SoundOutput.bgm)
    //    {
    //        SoundClip s = bgmClips.Find((sound) => sound.Clip == clip);
    //        if (s != null) SetSourceValues(s, bgmSource);
    //        else bgmSource.clip = clip;
    //        bgmSource.Play();
    //    }
    //    else if (output == SoundOutput.sfx)
    //    {
    //        SoundClip s = sfxClips.Find((sound) => sound.Clip == clip);
    //        if(s != null) SetSourceValues(s, sfxSource);
    //        sfxSource.PlayOneShot(clip);
    //    }
    //}

    private void SetSourceValues(SoundClip s, AudioSource source)
    {
        source.clip = s.Clip;
        source.volume = s.Volume;
        source.pitch = s.Pitch;
        source.loop = s.loop;
    }

    private AudioClip GetPlayingClip(AudioSource source)
    {
        return source.clip;
    }
}
