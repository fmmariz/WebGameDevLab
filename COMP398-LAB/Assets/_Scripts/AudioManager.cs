using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AudioData
{
    public String audioName;
    public AudioClip audioClip;
}

public class AudioManager : MonoBehaviour, IObserver<PlayerEnums>
{
    [SerializeField] private Subject<PlayerEnums> _playerSubject;
    [SerializeField] private List<AudioData> audios =
        new List<AudioData>();
    [SerializeField] private AudioSource _sfxPlayer;

    void Awake()
    {
        _playerSubject = GameObject.FindGameObjectWithTag("Player").GetComponent<Subject<PlayerEnums>>();

    }
    public void OnNotify(PlayerEnums enums)
    {
        switch (enums)
        {
            case PlayerEnums.DAMAGED:
                _sfxPlayer.clip =
                    audios.Find(s => s.audioName == "Damaged").audioClip;
                _sfxPlayer.Play();

                break;
            case PlayerEnums.JUMPING:
                _sfxPlayer.clip = 
                    audios.Find(s => s.audioName == "Jump").audioClip;
                _sfxPlayer.Play();
                break;
        }
    }

    private void OnEnable()
    {
        _playerSubject.AddObserver(this);
    }

    private void OnDisable()
    {
        _playerSubject.RemoveObserver(this);
    }
}
