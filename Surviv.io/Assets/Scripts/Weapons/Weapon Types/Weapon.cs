using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Weapon : MonoBehaviour
{
    private Unit _unit;
    
    [SerializeField] private float _fireRate;

    [SerializeField] public int currentAmmoMag;
    [SerializeField] public int maxAmmoMag;
    [SerializeField] private int _ammoNumber;

    [SerializeField] private int _bulletCount = 1;
    [SerializeField] private float _spreadAngle = 25f;
    [SerializeField] private float _projectileSpeed;

    [SerializeField] private float _reloadSpeed;
    [SerializeField] private bool _canShoot = true;
    [SerializeField] private bool _isReloading = false;

    [SerializeField] protected GameObject _projectilePrefab;
    [SerializeField] protected Transform _firePoint;

    private void Awake()
    {
        _unit = GetComponentInParent<Unit>();

        if (_unit.unitType == UnitType.Enemy)
            EnemyReload();

    }

    public IEnumerator CO_Reload()
    {
        yield return new WaitForSeconds(_reloadSpeed);

        if (_unit.unitType == UnitType.Player)
        {
            SubtractTotalAmmoWithCurrentClipSize();
            //Update UI Ammo
        }
        else
        {
            EnemyReload();
        }

        _isReloading = false;

    }

    public IEnumerator CO_FireRate()
    {
        yield return new WaitForSeconds(_fireRate);

        _canShoot = true;
    }

    protected virtual void SubtractTotalAmmoWithCurrentClipSize()
    {
        Debug.Log("Subtracting Total Ammo with Clip Size");
    }

    private void HandleFire(bool isPlayer)
    {
        if (_isReloading)
            return;

        if (_canShoot && currentAmmoMag > 0)
        {
            for (int i = 0; i < _bulletCount; i++)
                SpawnProjectile();

            currentAmmoMag--;
            _canShoot = false;

            if (isPlayer)
                //Update UI

            StartCoroutine(CO_FireRate());
        }
        else
        {
            _isReloading = true;
            StartCoroutine(CO_Reload());
        }
    }

    public virtual void Fire()
    {
        if (!this.gameObject.activeSelf)
            return;

        bool isPlayer = _unit.unitType == UnitType.Player;
        HandleFire(isPlayer);
    }

    private void SpawnProjectile()
    {
        GameObject projectile = Instantiate(_projectilePrefab, _firePoint.position, _firePoint.rotation);

        float randomAngle = Random.Range(-_spreadAngle / 2, _spreadAngle / 2);

        Vector2 direction = Quaternion.Euler(0, 0, randomAngle) * _firePoint.right;

        Rigidbody2D rigidBody = projectile.GetComponent<Rigidbody2D>();
        rigidBody.velocity = direction * _projectileSpeed;

        Destroy(projectile, 2f);
    }

    void EnemyReload()
    {
        currentAmmoMag = maxAmmoMag;
    }

    private void OnEnable()
    {
        _canShoot = true;
        _isReloading = false;
    }
}
