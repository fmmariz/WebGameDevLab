using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatsController : MonoBehaviour, IObserver<PlayerEnums>
{
    [SerializeField] private Subject<PlayerEnums> _playerSubject;
    [SerializeField] private int _playerHealth = 3; 


    void Awake()
    {
        _playerSubject = GameObject.FindGameObjectWithTag("Player")
            .GetComponent<Subject<PlayerEnums>>();

    }

    void OnEnable() => _playerSubject.AddObserver(this);
    

    void OnDisable() => _playerSubject.RemoveObserver(this);
    

    public void OnNotify(PlayerEnums playerEnums)
    {
        switch (playerEnums)
        {
            case PlayerEnums.DAMAGED:
                Died();
                break;
            case PlayerEnums.JUMPING:
                break;
            case PlayerEnums.DEAD:
                break;
        }
        
    }

    private void Died()
    {
        Debug.Log($"Player died from fall.");
        _playerHealth--;
        if (_playerHealth <= 0)
        {
            Debug.Log($"Game Over");
            SceneManager.LoadScene("GameOver");
        }
    }
}
