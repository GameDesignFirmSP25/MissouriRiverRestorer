using Ink.Parsed;
using System;
using System.Collections;
using System.Diagnostics.Tracing;
using UnityEngine;

public class SFXMaker : MonoBehaviour
{
    [Header("Play this sound on awake\nAlso check playOnAwake")]
    [SerializeField] SFXLibrary.SFXType awakeSoundEffect;
    [SerializeField] bool playOnAwake = false;

    [Header("Play this sound when calling PlaySound()")]
    [SerializeField] SFXLibrary.SFXType mainSoundEffect;

    [Header("If I have no specific audio source, I'll make my own, dw")]
    [SerializeField] AudioSource myAudioSource = null;


    public bool canMakeSound = false;

    //private static Transform audioTransform = null;
    //public Transform AudioTransform
    //{
    //    get
    //    {
    //        if (audioTransform == null)
    //        {
    //            audioTransform = transform.Find("AudioParent");
    //        }
    //        return audioTransform;
    //    }
    //}

    public virtual void Awake()
    {
        if (myAudioSource == null) {  myAudioSource = GetComponent<AudioSource>();}

        //if(playOnAwake && awakeSoundEffect != SFXLibrary.SFXType.Default)
        //{
        //    MakeSound(awakeSoundEffect);
        //}
    }

    private void OnEnable()
    {
        if (playOnAwake && awakeSoundEffect != SFXLibrary.SFXType.Default)
        {
            MakeSound(awakeSoundEffect);
        }
    }

    private void OnDisable()
    {

    }

    private void SetSoundOn()
    {
        canMakeSound = true;
    }

    private void SetSoundOff()
    {
        canMakeSound = false;
    }

    public void PlaySound()
    {
        MakeSound(mainSoundEffect);
    }

    public static void PlaySoundType(SFXLibrary.SFXType type, Vector3 position)
    {
        try
        {
            SFXSO sound = SFXLibrary.GetSound(type);

            AudioSource source = null;
            AudioClip clip = sound.GetClip();
            source.clip = clip;

            if (source == null)
            {
                source = (new GameObject()).AddComponent<AudioSource>();
                source.name = type.ToString();
                source.transform.position = position;
                source.outputAudioMixerGroup = sound.GetAudioGroup();

                if (clip != null)
                {
                    GameObject.Destroy(source.gameObject, source.clip.length + sound.Seconds);
                }
            }

            source.volume = sound.GetVolume();
            source.pitch = sound.GetPitch();

            source.Play();
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    public void MakeSound(SFXLibrary.SFXType soundEffect)
    {
        if (soundEffect == SFXLibrary.SFXType.Default) { return; }
        SFXSO sound = SFXLibrary.GetSound(soundEffect);

        //Debug.Log(sound);

        AudioSource source = myAudioSource;
        AudioClip clip = sound.GetClip();

        if (clip == null)
        {
            Debug.Log("No Clip: " + soundEffect.ToString());
            return;
        }

        if (source == null) //make a temp source
        {
            source = (new GameObject()).AddComponent<AudioSource>();
            source.name = soundEffect.ToString() + " " + clip.name;
            source.transform.position = transform.position;
            source.outputAudioMixerGroup = sound.GetAudioGroup();

            if(source != null)
            {
                GameObject.Destroy(source.gameObject, clip.length + sound.Seconds);
            }
;
        }

        if (source != null)
        {
            source.clip = clip;
            source.volume = sound.GetVolume();
            source.pitch = sound.GetPitch();
            PlayAudio(source, sound);
        }
        //Debug.Log(gameObject.name + " makes sound " + soundEffect.ToString());
        try
        {

        }
        catch (Exception e)
        {
            Debug.Log(e + " " + mainSoundEffect);
        }
    }

    //public void StopLoop()
    //{
    //    if (myAudioSource != null)
    //    {
    //        myAudioSource.Stop();
    //    }
    //}

    public void MakeSound(out float clipLength)
    {
        //Debug.Log(gameObject.name + " makes sound " + soundEffect.ToString());
        clipLength = 0;
        if (mainSoundEffect == SFXLibrary.SFXType.Default) { return; }

        SFXSO sound = SFXLibrary.GetSound(mainSoundEffect);
        //Debug.Log(sound + ", " + sound.GetNumClips());

        AudioSource source = myAudioSource;
        AudioClip clip = sound.GetClip();

        if (clip == null)
        {
            Debug.Log("No Clip: " + sound.ToString());
            return;
        }

        clipLength = sound.Seconds;

        if (source == null) //make a temp source
        {
            source = (new GameObject()).AddComponent<AudioSource>();
            source.name = mainSoundEffect.ToString() + " " + clip.name; ;
            source.transform.position = transform.position;
            source.outputAudioMixerGroup = sound.GetAudioGroup();

            GameObject.Destroy(source.gameObject, (clip.length + sound.Seconds));
        }

        if (source != null)
        {
            source.clip = clip;
            source.volume = sound.GetVolume();
            source.pitch = sound.GetPitch();
            PlayAudio(source, sound);
        }

    }


    Coroutine coroutine = null;

    private void PlayAudio(AudioSource source, SFXSO so)
    {
        source.Play();
        //if (source.clip != null)
        //{
        //    GameObject.Destroy(source.gameObject, source.clip.length + so.Seconds);
        //}

        return;

        //fade in
        //if (so.FadesIn || so.FadesOut)
        //{
        //    if(coroutine != null)
        //    {
        //        StopCoroutine(coroutine);
        //    }

        //    coroutine = StartCoroutine(FadeInFadeOutRoutine(so, source));
        //}
        //else
        //{
        //    source.Play();
        //    if (source.clip != null)
        //    {
        //        GameObject.Destroy(source.gameObject, source.clip.length + so.Seconds);
        //    }
        //    //if (so.ClipType == SFXSO.ClipPlayType.OneShot)
        //    //{

        //    //}
        //    //else //loop
        //    //{
        //    //    source.loop = true;
        //    //    source.Play();
        //    //}
        //}
    }

    //public IEnumerator FadeInFadeOutRoutine(SFXSO data, params AudioSource[] source)
    //{
    //    if(data.FadesIn)
    //    {
    //        yield return FadeInRoutine(data.Seconds, source);
    //    }
    //    if (source[0].clip != null)
    //    {
            
    //    }
    //    if(data.FadesOut)
    //    {
    //        yield return FadeOutRoutine(data.Seconds, source);
    //    }
    //    yield return null;
    //}


    public IEnumerator FadeOutRoutine(float fadeTime, params AudioSource[] source)
    {
        if (source[0] != null)
        {
            while (source[0].volume > 0f)
            {
                foreach (AudioSource a in source)
                {
                    a.volume -= Time.deltaTime * fadeTime;
                }
                yield return null;
            }
        }

        foreach (AudioSource a in source)
        {
            a.volume = 0f;
        }
        yield return null;
    }

    public IEnumerator FadeInRoutine(float fadeTime, params AudioSource[] source)
    {
        if (source[0] != null)
        {
            while (source[0].volume < 1f)
            {
                foreach (AudioSource a in source)
                {
                    a.volume += Time.deltaTime * fadeTime;
                }
                yield return null;
            }
        }

        foreach (AudioSource a in source)
        {
            a.volume = 1f;
        }
        yield return null;
    }
}
