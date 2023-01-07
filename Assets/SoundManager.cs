using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    [SerializeField] public AudioClip[] musicDB;
    [SerializeField] public AudioClip[] effectDB;

    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _effectSource;

    private bool _isPlaying = false;
    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Main Menu" && !_isPlaying)
        {
            PlayMusic(0);
            SetPlaying(true);
        }
        if (SceneManager.GetActiveScene().name == "Lvl1" && !_isPlaying)
        {
            PlayMusic(1);
            SetPlaying(true);
        }
    }

    public void PlayMusic(int id)
    {
        _musicSource.clip = musicDB[id];
        _musicSource.Play();
        _musicSource.loop = true;
    }
    public void SetPlaying(bool isactiv)
    {
        _isPlaying = isactiv;
    }
}
