using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GuidebookScript : MonoBehaviour
{
    //private UIDocument guidedocument;
    public Button rightbutton;
    public Button leftbutton;
    public Sprite[] guideimage = new Sprite[2];
    public int index;
    public Image currentPage;
    public Canvas CanvasPage;
    //public AudioClip backAudio;
    //private AudioSource audioSrc;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        index = 0;
        //currentPage = CanvasPage.GetComponentInChildren<Image>();
        currentPage = GameObject.Find("page").GetComponent<Image>();
        //audioSrc = GetComponent<AudioSource>();
        //audioSrc.clip = backAudio;
        //audioSrc.loop = true;
        //audioSrc.Play();
    }

    public void RightArrow()
    {
        if (index < 1)
        {
            index++;
        }
        currentPage.sprite = guideimage[index];
    }

    public void LeftArrow()
    {
        if (index > 0)
        {
            index--;
        }
        currentPage.sprite = guideimage[index];
    }
}
