using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSHooter : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private List<Transform> _projectileSpawn;
    [SerializeField] private float _projectileForce = 0f;
    [SerializeField] private COMP398LAB _inputs;
    [SerializeField] private Transform _currentProjectileSpawn;
    [SerializeField] private Button _shootProjectileBtn;
    [SerializeField] private Button _turnProjectileSpawnRight;
    [SerializeField] private Button _turnProjectileSpawnLeft;


     private int _index = 0;

    private void Awake()
    {
        _currentProjectileSpawn = _projectileSpawn[_index];
        _inputs = new COMP398LAB();
        //_inputs.Player.Fire.performed += _ => ShootProjectile();
        _inputs.Player.Camera.performed += context => ChangeProjectileSpawn(context.ReadValue<float>());
        _shootProjectileBtn.onClick.AddListener(() => ShootPooledProjectile());
        _turnProjectileSpawnRight.onClick.AddListener(() => ChangeProjectileSpawn(-1));
        _turnProjectileSpawnLeft.onClick.AddListener(() => ChangeProjectileSpawn(1));
    }


    private void ShootProjectile()
    {
        GameObject projectile = Instantiate(
            _projectilePrefab,
            _currentProjectileSpawn.transform.position,
            _currentProjectileSpawn.transform.rotation);
        projectile.GetComponent<Rigidbody>()
            .AddForce(projectile.transform.forward * _projectileForce, ForceMode.Impulse);
    }

    private void ShootPooledProjectile()
    {
        var projectile = ProjectilePoolManager.Instance.Get();
        projectile.transform.SetPositionAndRotation(_currentProjectileSpawn.position, _currentProjectileSpawn.rotation);
        projectile.gameObject.SetActive(true);
        projectile.GetComponent<Rigidbody>()
           .AddForce(projectile.transform.forward * _projectileForce, ForceMode.Impulse);
    }

    private void ChangeProjectileSpawn(float direction)
    {
        _index -= (int) direction;
        if (_index < 0) _index = _projectileSpawn.Count - 1;
        if (_index > _projectileSpawn.Count - 1) _index = 0;
        _currentProjectileSpawn = _projectileSpawn[_index];
    }

    private void FixedUpdate()
    {
        if (_inputs.Player.Fire.IsPressed())
        {
            ShootPooledProjectile();

        }
    }

    private void OnEnable()
    {
        _inputs.Enable();
    }

    private void OnDisable()
    {
        _inputs.Disable();
    }



}
