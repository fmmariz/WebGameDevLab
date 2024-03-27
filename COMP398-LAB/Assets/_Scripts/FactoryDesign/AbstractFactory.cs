using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractFactory : MonoBehaviour
{
    public float _spawnTimer;
    public GameObject _agentPrefab;
    public Transform _spawnLocation;
    public Transform _agentDestination;

    public abstract void CreateAgent();

}
