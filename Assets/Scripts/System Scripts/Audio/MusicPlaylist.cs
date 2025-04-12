using UnityEngine;

public class MusicPlaylist : MonoBehaviour
{
    [Header("State")]
    [SerializeField] AudioClip currentSong = null;

    [Header("Configuration")]
    [SerializeField] AudioClip[] musicPlaylist = null;

    [Header("References")]
    [SerializeField] AudioSource musicPlayer = null;

    void Update()
    {
        if(!musicPlayer.isPlaying)
        {
            musicPlayer.Stop();
            PlayMusic();
        }
    }

    public void PlayMusic()
    {
        //pick a random song to play
        currentSong = musicPlaylist[Random.Range(0, musicPlaylist.Length)];
        musicPlayer.clip = currentSong;
        musicPlayer.Play();
    }

}
