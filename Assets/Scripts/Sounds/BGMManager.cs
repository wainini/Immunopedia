using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BGMManager : MonoBehaviour
{
    AudioSource audioSrc;
    public AudioMixerGroup bgm, sfx;
    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            audioSrc.outputAudioMixerGroup = bgm;
            audioSrc.PlayOneShot(audioSrc.clip);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            audioSrc.outputAudioMixerGroup = sfx;
            audioSrc.PlayOneShot(audioSrc.clip);
        }
    }
}
