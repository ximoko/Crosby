﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public enum MixerGroup { Master, Music, SFX };
    public MixerGroup mixerGroup;
    
    public AudioClip[] sfxClips;
    public AudioClip[] ambientClips;
    public AudioClip[] musicClips;
    private AudioSource musicSource;
    private AudioSource ambientSource;

    #region SFX

    //3D
    public void PlaySFX(int clip)
    {
        PlaySFX(clip, 1, 1);
    }

    public void PlaySFX(int clip, float volume, float pitch)
    {
        Play(sfxClips[clip], volume, pitch, false, false, "SFX");
    }

    public void PlaySFX(int clip, float volume, float pitch, GameObject targetObject)     //Overload to play sound on a certain object
    {
        AudioSource source = targetObject.AddAudioSource();
        source.Play(sfxClips[clip], volume, pitch, false, false, "SFX");

        Destroy(source,  sfxClips[clip].length);
    }

    //2D
    public void Play2DSFX(int clip)
    {
        Play2DSFX(clip, 1, 1);
    }
    public void Play2DSFX(int clip, float volume, float pitch)
    {
        Play(sfxClips[clip], volume, pitch, false, true, "SFX");
    }    

    #endregion

    #region Music

    public void PlayMusic(int clip)
    {
        PlayMusic(clip, 1, true);
    }

    public void PlayMusic(int clip, float volume, bool loop)
    {
        Play(musicClips[clip], volume, 1, loop, true, "Music");
    }

    public void StopMusic()
    {
        musicSource.TryStop();
        Destroy(musicSource);
    }

    #endregion

    #region Ambient // SFX Loop

    public void PlayAmbient(int clip)
    {
        PlayAmbient(clip, 1);
    }

    public void PlayAmbient(int clip, float volume)
    {
        Play(ambientClips[clip], volume, 1, true, true, "Ambient");
    }

    public void StopAmbient()
    {
        ambientSource.TryStop();
        Destroy(ambientSource);
    }

    #endregion

    public void Play(AudioClip audio, float volume, float pitch, bool loop, bool audio2D, string groupName)
    {
        AudioSource source = this.gameObject.AddAudioSource();
        source.Play(audio, volume, pitch, loop, audio2D, groupName);

        if(!loop) Destroy(source, audio.length);
        else
        {
            if(groupName == "Music")
            {
                if(musicSource != null) StopMusic();
                musicSource = source;
            }
            else
            {
                if(ambientSource != null) StopAmbient();
                ambientSource = source;
            }
        }
    }

    public void Play(int clipNumber, float volume, float pitch, bool loop, bool audio2D, string groupName)
    {
        AudioSource source = this.gameObject.AddAudioSource();
        if (groupName == "SFX") source.Play(sfxClips[clipNumber], volume, pitch, loop, audio2D, groupName);
        if (groupName == "Ambient") source.Play(ambientClips[clipNumber], volume, pitch, loop, audio2D, groupName);
        if (groupName == "Music") source.Play(ambientClips[clipNumber], volume, pitch, loop, audio2D, groupName);

        if (!loop) Destroy(source, source.clip.length);
        else
        {
            if (groupName == "Music")
            {
                if (musicSource != null) StopMusic();
                musicSource = source;
            }
            else
            {
                if (ambientSource != null) StopAmbient();
                ambientSource = source;
            }
        }
    }

    public void Play(GameObject targetObject, AudioClip audio, float volume, float pitch, bool loop, bool audio2D, string groupName)
    {
        AudioSource source = targetObject.AddAudioSource();
        source.Play(audio, volume, pitch, loop, audio2D, groupName);

        if (!loop) Destroy(source, audio.length);
        else
        {
            if (groupName == "Music")
            {
                if (musicSource != null) StopMusic();
                musicSource = source;
            }
            else
            {
                if (ambientSource != null) StopAmbient();
                ambientSource = source;
            }
        }
    }

    public void Play(GameObject targetObject, int clipNumber, float volume, float pitch, bool loop, bool audio2D, string groupName)
    {
        AudioSource source = targetObject.AddAudioSource();
        if (groupName == "SFX") source.Play(sfxClips[clipNumber], volume, pitch, loop, audio2D, groupName);
        if (groupName == "Ambient") source.Play(ambientClips[clipNumber], volume, pitch, loop, audio2D, groupName);
        if (groupName == "Music") source.Play(ambientClips[clipNumber], volume, pitch, loop, audio2D, groupName);

        if (!loop) Destroy(source, source.clip.length);
        else
        {
            if (groupName == "Music")
            {
                if (musicSource != null) StopMusic();
                musicSource = source;
            }
            else
            {
                if (ambientSource != null) StopAmbient();
                ambientSource = source;
            }
        }
    }
}
