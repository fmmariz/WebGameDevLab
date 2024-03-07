using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneGameOver : MonoBehaviour
{
    [SerializeField] private Button _menuBtn;
    [SerializeField] private string _menuSceneName;

    private void Start()
    {
        _menuBtn.onClick.AddListener(() =>
        {
            SceneController.Instance.ChangeScene(_menuSceneName);
        });
    }
}
