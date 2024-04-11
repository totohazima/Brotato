using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AudioManager : MonoBehaviour, UI_Upadte
{
    public static AudioManager instance;

    [Header("#BGM")]
    public AudioClip[] bgmClip;
    public float bgmVolume;
    AudioSource[] bgmPlayer;
    AudioHighPassFilter bgmEffect;

    [Header("#SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume;
    //public int channels;
    AudioSource[] sfxPlayers;
    int channelIndex;
    public enum Bgm
    {
        MAIN,

    }
    public enum Sfx
    {
        DEAD,
        HIT,
        LEVELUP,
        LOSE,
        MELEE,
        RANGE,
        SELECT,
        WIN
    }

    void Awake()
    {
        instance = this;
        Init();
    }

    void OnEnable()
    {
        UIUpdateManager.uiUpdates.Add(this);
    }
    void OnDisable()
    {
        UIUpdateManager.uiUpdates.Remove(this);
    }
    void Init()
    {
        //배경음 플레이어 초기화
        bgmPlayer = new AudioSource[bgmClip.Length];
        for (int i = 0; i < bgmPlayer.Length; i++)
        {
            bgmPlayer[i].playOnAwake = false;
            bgmPlayer[i].loop = true;
            bgmPlayer[i].clip = bgmClip[i];
        }
        bgmEffect = Camera.main.GetComponent<AudioHighPassFilter>();
        //효과음 플레이어 초기화
        sfxPlayers = new AudioSource[sfxClips.Length];
        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            sfxPlayers[i].playOnAwake = false;
            sfxPlayers[i].bypassListenerEffects = true;
        }
    }

    public void UI_Update()
    {
        bgmVolume = OptionManager.instance.bgmVolume;
        sfxVolume = OptionManager.instance.sfxVolume;
        for (int i = 0; i < bgmPlayer.Length; i++)
        {
            bgmPlayer[i].volume = bgmVolume;
        }
        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            sfxPlayers[i].volume = sfxVolume;
        }

    }

    public void PlayBgm(Bgm bgm ,bool isPlay)
    {
        if (isPlay)
        {
            bgmPlayer[(int)bgm].Play();
        }
        else
        {
            bgmPlayer[(int)bgm].Stop();
        }
    }

    public void EffectBgm(bool isPlay)
    {
        bgmEffect.enabled = isPlay;
    }

    public void PlaySfx(Sfx sfx)
    {
        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            int loopIndex = (i + channelIndex) % sfxPlayers.Length;

            if (sfxPlayers[loopIndex].isPlaying)
            {
                continue;
            }
            int ranIndex = 0;
            if (sfx == Sfx.HIT || sfx == Sfx.MELEE)//효과음이 2개 인건 랜덤으로 하나 출력
            {
                ranIndex = Random.Range(0, 2);
            }

            channelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx + ranIndex];
            sfxPlayers[loopIndex].Play();
            break;
        }
    }
}
