using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcreteFactory : MonoBehaviour
{
    [SerializeField] private List<AbstractFactory> _transportFactories;
    private AbstractFactory _factory;
    private int _index = 0;

    private void Start()
    {
        _factory = _transportFactories[_index];
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(GenerateAgents());
        }
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            StopCoroutine(GenerateAgents());
        }
    }

    private IEnumerator GenerateAgents()
    {
        var time = new WaitForSeconds(_factory._spawnTimer);
        while(true)
        {
            _factory.CreateAgent();
            _factory = _transportFactories[Random.Range(0, _transportFactories.Count)];
            yield return time;
        }
        yield return null;
    }
}

