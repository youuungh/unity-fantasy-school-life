using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Sound
{
    public string name;         // 곡의 이름
    public AudioClip clip;      // 곡
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource bgmSource = null;
    public AudioSource[] seSource = null;
    [SerializeField] public Sound[] bgm = null;
    [SerializeField] public Sound[] se = null;
    
    void Awake()
    {
        // 싱글톤
        DontDestroyOnLoad(this.gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(this.gameObject);
    }

    // BGM 재생
    public void PlayBgm(string bgmName)
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            if(bgmName == bgm[i].name)
            {
                bgmSource.clip = bgm[i].clip;
                bgmSource.Play();
            }
        }
    }


    public void PauseBgm()
    {
        bgmSource.Pause();
    }


    public void StopBgm()
    {
        bgmSource.Stop();
    }

    // SE 재생
    public void PlaySe(string seName)
    {
        for (int i = 0; i < se.Length; i++)
        {
            if (seName == se[i].name)
            {
                for (int j = 0; j < seSource.Length; j++)
                {
                    if(!seSource[j].isPlaying)
                    {
                        seSource[j].clip = se[i].clip;
                        seSource[j].Play();
                        return;
                    }
                }
                return;
            }
        }
    }

}
