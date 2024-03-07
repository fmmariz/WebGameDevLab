using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Audio Manager is scene dependent. which means it will control the audio
/// for a specific scene
/// </summary>
public class AudioManager : MonoBehaviour, IObserver<PlayerEnums>
{
    [SerializeField] private Subject<PlayerEnums> _playerSubject;
    [SerializeField] private string _musicName;
    [SerializeField]
    private List<AudioAsset> audios =
    new List<AudioAsset>();

    void Awake()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            _playerSubject = player.GetComponent<Subject<PlayerEnums>>();
        }
    }

    void Start()
    {
        var musicAsset = audios.Find(a => a.name == _musicName);
        AudioController.Instance.PlayMusic(musicAsset);
    }

    public void OnNotify(PlayerEnums enums)
    {
        AudioAsset asset = null;
        switch (enums)
        {
            case PlayerEnums.DAMAGED:
                asset = audios.Find(s => s.AudioName == "Death");
                break;
            case PlayerEnums.JUMPING:
                asset = audios.Find(s => s.AudioName == "Jump");
                break;
        }
        AudioController.Instance.PlaySfx(asset);
    }

    private void OnEnable()
    {
        if (_playerSubject == null) return;
        _playerSubject.AddObserver(this);
    }

    private void OnDisable()
    {
        if (_playerSubject == null) return;
        _playerSubject.RemoveObserver(this);
    }
}
