using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SoundManager : Singleton<SoundManager>
{
    List<AudioSource> audioSourceMusics = new List<AudioSource>();
    AudioSource audioSourceSfx;
    Dictionary<string, float> saveTime = new Dictionary<string, float>();

    void Start()
    {
        audioSourceSfx = gameObject.AddComponent<AudioSource>();
        audioSourceSfx.playOnAwake = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySfx(AudioClip clip)
    {
        audioSourceSfx.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        AudioSource audioSource = GetAudioSource(clip);
        audioSource.Play();
        audioSource.volume = 0;
        audioSource.time = saveTime[audioSource.name];
        audioSource.DOFade(1, 1);
    }
    public void StopMusic(AudioClip clip)
    {
        AudioSource audioSource = GetAudioSource(clip);
        
        audioSource.DOFade(0, 1).OnComplete(()=>{
            saveTime[audioSource.name] = audioSource.time;
            audioSource.Stop();
        });
    }

    AudioSource GetAudioSource(AudioClip clip)
    {
        foreach(var ads in audioSourceMusics)
        {
            if(ads.clip == clip)
            {
                return ads;
            }
        }
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.name = audioSourceMusics.Count.ToString();
        audioSourceMusics.Add(audioSource);
        saveTime.Add(audioSource.name, 0);
        return audioSource;
    }
}
