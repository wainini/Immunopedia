using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SoundClip
{
    public string name;

    public AudioClip Clip;

    [Range(0f, 1f)]
    public float Volume = 1f;

    [Range(.1f, 3f)]
    public float Pitch = 1f;

    public bool loop;
}

public enum SoundOutput
{
    bgm, sfx
}
