using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatsController : MonoBehaviour, IObserver<PlayerEnums>
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private int _playerHealth = 3; 


    void Awake()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player")
            .GetComponent<PlayerController>();

    }

    void OnEnable() => _playerController.AddObserver(this);
    

    void OnDisable() => _playerController.RemoveObserver(this);
    

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
            SceneController.Instance.ChangeScene("GameOver");
        }
    }

    public void SaveGameIntoFile()
    {
        SaveGameManager.Instance().SaveGame(_playerController.transform);

    }

    public void LoadGameFromFile()
    {
        var playerData = SaveGameManager.Instance().LoadGame();
        var position = playerData.position;
        _playerController.transform.position = JsonUtility.FromJson<Vector3>(position);

    }
}
