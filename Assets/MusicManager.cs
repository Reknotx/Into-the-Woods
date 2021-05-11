using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : SingletonPattern<MusicManager>
{
    [System.Serializable]
    public struct MusicClips
    {
        public AudioClip bossMusic;
        public AudioClip winClip;
        public AudioClip loseClip;
    }


    public AudioSource source;

    public MusicClips musicClips;


    protected override void Awake()
    {
        base.Awake();
    }

    public void PlayEndClip(bool gameWon)
    {
        if (gameWon)
        {
            source.clip = musicClips.winClip;
        }
        else
        {
            source.clip = musicClips.loseClip;
        }

        source.Play();
        source.loop = false;
    }

    public void PlayBossMusic()
    {
        source.clip = musicClips.bossMusic;
        source.Play();
    }
}
