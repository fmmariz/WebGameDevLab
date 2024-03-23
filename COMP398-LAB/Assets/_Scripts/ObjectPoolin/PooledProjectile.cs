using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledProjectile : MonoBehaviour
{
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _maxLifeTime = 3f;

    private void OnEnable()
    {
        _lifeTime = 0;
    }

    private void Update()
    {
        _lifeTime += Time.deltaTime;
        if(_lifeTime > _maxLifeTime)
        {
            ProjectilePoolManager.Instance.ReturnToPool(this);
        }
    }
}
