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
    [SerializeField] Sound[] Monster_sfx = null;
    [SerializeField] Sound[] bgm = null;

    [SerializeField] AudioSource[] sfxPlayer = null;
    [SerializeField] AudioSource bgmPlayer;
    public Dictionary<int, AudioSource> MonstersfxPlayer;

    private void Awake()
    {
        instance = this;
        MonstersfxPlayer = new Dictionary<int, AudioSource>();
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
            if (p_sfxName == sfx[0].name)
            {
                if (!sfxPlayer[3].isPlaying)
                {
                    sfxPlayer[3].clip = sfx[0].clip;
                    sfxPlayer[3].Play();
                    return;
                }
            }
            else if (p_sfxName == sfx[1].name || p_sfxName == sfx[2].name || p_sfxName == sfx[3].name)
            {
                if (!sfxPlayer[0].isPlaying)
                {
                    sfxPlayer[0].clip = sfx[i].clip;
                    sfxPlayer[0].Play();
                    return;
                }
            }
            else
            {
                if (!sfxPlayer[1].isPlaying)
                {
                    sfxPlayer[1].clip = sfx[i].clip;
                    sfxPlayer[1].Play();
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

    public void Add_Monster_audio(AudioSource audioSource, int ID)
    {
        MonstersfxPlayer.Add(ID, audioSource);
        MonstersfxPlayer[ID].Pause();
        Debug.Log(MonstersfxPlayer[ID].isPlaying);
    }

    public void Monster_PlaySFX(string p_sfxName, int ID)
    {
        if (MonstersfxPlayer.Count != 0)
        {
            for (int i = 0; i < Monster_sfx.Length; i++)
            {
                if (p_sfxName == Monster_sfx[i].name)
                {
                    MonstersfxPlayer[ID].clip = Monster_sfx[i].clip;
                    MonstersfxPlayer[ID].Play();
                    return;
                }
            }

            //Debug.Log(p_sfxName + " 이름의 효과음이 없습니다.");
            return;
        }
    }

    public void Monster_StopSFX(int id)
    {
        if (MonstersfxPlayer.Count != 0)
        {
            MonstersfxPlayer[id].Pause();
            return;
        }
    }
}
