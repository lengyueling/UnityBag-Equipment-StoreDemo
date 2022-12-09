using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MusicMgr : Singleton<MusicMgr>
{
    private AudioSource bkMusic = null;
    private GameObject soundObj = null;
    private List<AudioSource> soundList = new List<AudioSource>();
    private float bkValue = 1;
    private float soundValue = 1;

    public MusicMgr()
    {
        MonoMgr.Instance.AddUpdateListener(Update);
    }

    void Update()
    {
        for (int i = soundList.Count - 1; i >= 0; i--)
        {
            if (!soundList[i].isPlaying)
            {
                GameObject.Destroy(soundList[i]);
                soundList.RemoveAt(i);
            }
        }
    }

    public void PlayBkMusic(string name)
    {
        if (bkMusic == null)
        {
            GameObject obj = new GameObject();
            obj.name = "BkMusic";
            bkMusic = obj.AddComponent<AudioSource>();
        }
        ResMgr.Instance.LoadAsync<AudioClip>("Music/" + name, (clip)=> 
        {
            bkMusic.clip = clip;
            bkMusic.loop = true;
            bkMusic.volume = bkValue;
            bkMusic.Play();
        });
    }
    public void StopBkMusic()
    {
        if (bkMusic == null)
        {
            return;
        }
        bkMusic.Stop();
    }

    public void PauseBkMusic()
    {
        if (bkMusic == null)
        {
            return;
        }
        bkMusic.Pause();
    }

    public void ChangeBkVolume(float v)
    {
        bkValue = v;
        if (bkMusic == null)
        {
            return;
        }
        bkMusic.volume = bkValue;
    }

    public void PlaySound(string name, bool isLoop, UnityAction<AudioSource> callback = null)
    {
        if (soundObj == null)
        {
            soundObj = new GameObject();
            soundObj.name = "Sound";
        }
        for (int i = 0; i < soundList.Count; i++)
        {
            if (soundList[i].clip.name == name)
            {
                return;
            }
        }
        ResMgr.Instance.LoadAsync<AudioClip>("Sound/" + name, (clip) =>
        {
            AudioSource source = soundObj.AddComponent<AudioSource>();
            source.clip = clip;
            source.loop = isLoop;
            source.volume = soundValue;
            source.Play();
            soundList.Add(source);
            callback(source);
        });
    }

    public void ChangeSoundValue(float value)
    {
        soundValue = value;
        for (int i = 0; i < soundList.Count; ++i)
        {
            soundList[i].volume = value;
        }
    }

    public void StopSound(AudioSource source)
    {
        if (soundList.Contains(source))
        {
            soundList.Remove(source);
            source.Stop();
            GameObject.Destroy(source);
        }
    }
}
