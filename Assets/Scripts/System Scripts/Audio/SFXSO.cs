using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "SFX", menuName = "Audio Data")]
[Serializable]
public class SFXSO : ScriptableObject
{
    //public enum ClipPlayType { OneShot, LoopForSeconds, Interval }
    [SerializeField] internal SFXLibrary.SFXType type;
    [SerializeField] List<AudioClip> clips;
    [SerializeField] AudioMixerGroup outputMixerGroup;

    //[Header("Playing Clip")]
    //[SerializeField] private ClipPlayType clipType = ClipPlayType.OneShot;
    //public ClipPlayType ClipType
    //{
    //    get => clipType;
    //}
    [SerializeField] private float seconds = 1f;
    public float Seconds { get { return seconds; } }
    //[SerializeField] internal bool fadeIn = false;
    //public bool FadesIn { get { return fadeIn; } }
    //[SerializeField] internal bool fadeOut = false;
    //public bool FadesOut { get { return fadeOut; } }

    [Header("Pitch")]
    [SerializeField][Range(0f, 3f)] float basePitch = 1.0f;
    [SerializeField][Range(0f, 1f)] float pitchModPercent = .1f;

    [Header("Volume")]
    [SerializeField][Range(0f, 1f)] float baseVolume = 1.0f;
    [SerializeField][Range(0f, 1f)] float volumeModPercent = 0f;



    public AudioMixerGroup GetAudioGroup()
    {
        return outputMixerGroup;
    }

    public float GetBaseVolume()
    {
        return baseVolume;
    }

    public float GetVolume()
    {
        return Mathf.Clamp(UnityEngine.Random.Range(-volumeModPercent, volumeModPercent) + baseVolume, 0f, 1f);
    }

    public float GetPitch()
    {
        return Mathf.Clamp(UnityEngine.Random.Range(-pitchModPercent, pitchModPercent) + basePitch, 0f, 3f);
    }

    public AudioClip GetClip()
    {
        if(clips.Count > 0f)
        {
            return clips[UnityEngine.Random.Range(0, clips.Count - 1)];
        }
        return null;
    }


}
