using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour
{
    public SoundManager instance;

    [SerializeField] Sound[] sfx = null;
    [SerializeField] Sound[] bgm = null;

    [SerializeField] AudioSource[] sfxPlayer = null;
    [SerializeField] AudioSource bgmPlayer;


    private void Start()
    {
        instance = this;
    }

    
    public void PlayBGM(string p_bgmName)
    {
        Debug.Log("Called : " + p_bgmName);
        bgmPlayer.clip = bgm[0].clip;
        bgmPlayer.Play();

        for (int i = 0; i < bgm.Length; i++)
        {
            if (p_bgmName.Equals(bgm[i].name))
            {
                bgmPlayer.clip = bgm[i].clip;
                bgmPlayer.Play();
            }
        }
    }

    public void StopBGM()
    {
        bgmPlayer.Stop();
    }

    public void PlaySFX(string p_sfxName)
    {
        for (int i = 0; i < sfx.Length; i++)
        {
            if (p_sfxName == sfx[i].name && (i == 0 && i == 14))
            {
                if (!sfxPlayer[3].isPlaying)
                {
                    sfxPlayer[3].clip = sfx[i].clip;
                    sfxPlayer[3].Play();
                    return;
                }
            }
            else if (p_sfxName == sfx[i].name && i == 1)
            {
                if (!sfxPlayer[5].isPlaying)
                {
                    sfxPlayer[5].clip = sfx[i].clip;
                    sfxPlayer[5].Play();
                    return;
                }
            }
            else if (p_sfxName == sfx[i].name && i >= 2 && i<=7)
            {
                if (!sfxPlayer[2].isPlaying)
                {
                    sfxPlayer[2].clip = sfx[i].clip;
                    sfxPlayer[2].Play();
                    return;
                }
            }
            else if (p_sfxName == sfx[i].name && i >= 8 && i<=13)
            {
                if (!sfxPlayer[4].isPlaying)
                {
                    sfxPlayer[4].clip = sfx[i].clip;
                    sfxPlayer[4].Play();
                    return;
                }
            }
            /*
            if (p_sfxName == sfx[i].name)
            {
                for (int j = 0; j < sfxPlayer.Length; j++)
                {
                    // SFXPlayer에서 재생 중이지 않은 Audio Source를 발견했다면 
                    
                    if (!sfxPlayer[j].isPlaying)
                    {
                        sfxPlayer[j].clip = sfx[i].clip;
                        sfxPlayer[j].Play();
                        return;
                    }
                    
                }
                Debug.Log("모든 오디오 플레이어가 재생중입니다.");
                return;
            }
             */
        }
        Debug.Log(p_sfxName + " 이름의 효과음이 없습니다.");
        return;
    }
    
    public void StopSFX(string p_sfxName)
    {
        for (int i = 0; i < sfx.Length; i++)
        {
            if (p_sfxName == sfx[i].name)
            {
                for (int j = 0; j < sfxPlayer.Length; j++)
                {
                    // SFXPlayer에서 재생 중이지 않은 Audio Source를 발견했다면 
                    if (sfxPlayer[j].isPlaying)
                    {
                        sfxPlayer[j].Pause();
                        return;
                    }
                }
                return;
            }
        }
    }
}
