using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text;


public class Sound : Single<Sound>
{
    public string resourceDir = "";

    private AudioSource bgSound;
    private AudioSource effectSound;

    //重写Awake，加载audiosource
    protected override void Awake()
    {
        base.Awake();
        bgSound = this.gameObject.AddComponent<AudioSource>();
        bgSound.playOnAwake = false;
        bgSound.loop = true;

        effectSound = this.gameObject.AddComponent<AudioSource>();

    }

    //音乐大小
    public float bgVolume
    {
        get { return bgSound.volume; }
        set { bgSound.volume = value; }
    }

    //音效大小
    public float effectVolume
    {
        get { return effectSound.volume; }
        set { effectSound.volume = value; }
    }

    //播放音乐
    public void playBg(string name)
    {
        bgSound.volume = 0.5f;

        //判断是不是同一首音乐
        string oldName = "";
        if (bgSound.clip != null)
            oldName = bgSound.clip.name;

        if (name != oldName)
        {
            //获取路径加载音乐
            string path = "";
            if (!string.IsNullOrEmpty(resourceDir))
                path = resourceDir + "/" + name;
            else
                path = name;

            AudioClip clip = Resources.Load<AudioClip>(path);

            if (clip != null)
            {
                bgSound.clip = clip;
                bgSound.Play();
            }
        }

    }

    //停止音乐
    public void stopBg()
    {
        bgSound.Stop();
    }

    //播放音效
    public void playEffect(string name,float volume)
    {
        //获取路径加载音效
        string path = "";
        if (!string.IsNullOrEmpty(resourceDir))
            path = resourceDir + "/" + name;
        else
            path = name;

        AudioClip clip = Resources.Load<AudioClip>(path);

        effectSound.volume = volume;
        effectSound.PlayOneShot(clip);
    }
}

