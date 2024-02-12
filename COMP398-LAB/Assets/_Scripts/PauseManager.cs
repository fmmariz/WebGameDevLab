using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _pausePanel;
    private CharacterController _playerController;
    private COMP398LAB _inputs; 

    private void Awake()
    {
        _playerController = _player.GetComponent<CharacterController>();
        _inputs = new COMP398LAB();
        _inputs.Player.Pause.performed += context => PauseGame();
        _inputs.Enable();
       
    }

    private void OnDisable()
    {
        _inputs.Disable();
    }

    public void UnpauseGame()
    {
        _playerController.enabled = true;
        _pausePanel.SetActive(false);
    }

    public void PauseGame()
    {
        _playerController.enabled = false;
        _pausePanel.SetActive(true);
    }
}
