using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] bool testSound = false;
    [SerializeField] Slider volumeSlider_master;
    [SerializeField][Range(0f,1f)] float testMasterVol = 1f;
    [SerializeField] Slider volumeSlider_sfx;
    [SerializeField][Range(0f, 1f)] float testSFXVol = 1f;
    [SerializeField] Slider volumeSlider_music;
    [SerializeField][Range(0f, 1f)] float testMusicVol = 1f;
    [SerializeField] Slider volumeSlider_dx;
    [SerializeField][Range(0f, 1f)] float testDXVol = 1f;

    [SerializeField] AudioMixer mixer;

    private void OnEnable()
    {
        if(volumeSlider_master!= null)
            volumeSlider_master.onValueChanged.AddListener(delegate { SetMasterVolume(volumeSlider_master.value); });

        if (volumeSlider_sfx != null)
            volumeSlider_sfx.onValueChanged.AddListener(delegate { SetSFXVolume(volumeSlider_sfx.value); });

        if (volumeSlider_music != null)
            volumeSlider_music.onValueChanged.AddListener(delegate { SetMusicVolume(volumeSlider_music.value); });

        if (volumeSlider_dx != null)
            volumeSlider_dx.onValueChanged.AddListener(delegate { SetDXVolume(volumeSlider_dx.value); });
    }

    private void OnDisable()
    {
        if (volumeSlider_master != null)
            volumeSlider_master.onValueChanged.RemoveListener(delegate { SetMasterVolume(volumeSlider_master.value); });

        if (volumeSlider_sfx != null)
            volumeSlider_sfx.onValueChanged.RemoveListener(delegate { SetSFXVolume(volumeSlider_sfx.value); });

        if (volumeSlider_music != null)
            volumeSlider_music.onValueChanged.RemoveListener(delegate { SetMusicVolume(volumeSlider_music.value); });

        if (volumeSlider_dx != null)
            volumeSlider_dx.onValueChanged.RemoveListener(delegate { SetDXVolume(volumeSlider_dx.value); });
    }

    private void Update()
    {
        if(testSound)
        {
            SetMasterVolume(testMasterVol);
            SetSFXVolume(testSFXVol);
            SetMusicVolume(testMusicVol);
            SetDXVolume(testDXVol);
        }
    }

    public void SetMasterVolume(float f)
    {
        if(mixer == null) { return; }
        if (f == 0f) { f = -.01f; }
        mixer.SetFloat("MasterVol", (Mathf.Log10(f) * 20f) - .2f);
        if (volumeSlider_master != null)
        {
            volumeSlider_master.value = f;
        }
    }

    public void SetSFXVolume(float f)
    {
        if (mixer == null) { return; }
        if (f == 0f) { f = -.01f; }
        mixer.SetFloat("SFXVol", (Mathf.Log10(f) * 20f) - .2f);
        if (volumeSlider_sfx != null)
        {
            volumeSlider_sfx.value = f;
        }
    }

    public void SetMusicVolume(float f)
    {
        if (mixer == null) { return; }
        if (f == 0f) { f = -.01f; }
        mixer.SetFloat("MusicVol", (Mathf.Log10(f) * 20f) - .2f);
        if (volumeSlider_music != null)
        {
            volumeSlider_music.value = f;
        }
    }

    public void SetDXVolume(float f)
    {
        if (mixer == null) { return; }
        if (f == 0f) { f = -.01f; }
        mixer.SetFloat("DXVol", (Mathf.Log10(f) * 20f) - .2f);
        if (volumeSlider_dx != null)
        {
            volumeSlider_dx.value = f;
        }
    }
}
