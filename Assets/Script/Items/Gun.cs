using System;
using UnityEngine;

public class Gun : Item
{
    [SerializeField]
    private float reloadTime = 1;

    [SerializeField]
    private int magazineSize = 6;

    [SerializeField]
    private Vector3 zeroRotation = Vector3.zero;

    [SerializeField]
    private Transform bulletSpawn = null;

    private float damage = 10;
    private float range = 200;
    private int currentAmmo;
    private bool isReloading;

    public bool anyAmmoLeft
    {
        get
        {
            return currentAmmo != 0;
        }
    }

    private void Start()
    {
        pickUpRotation = zeroRotation;
        currentAmmo = magazineSize;
    }

    private float timer = 0;

    private void Update()
    {
        if (isReloading)
        {
            timer += Time.deltaTime;
            if (timer >= reloadTime)
            {
                timer = 0;
                isReloading = false;
                currentAmmo = magazineSize;
            }
        }
    }

    [SerializeField]
    private GameObject bulletPrefab = null;

    public override void UseItem(Vector3 direction)
    {
        if (!isReloading)
            if (anyAmmoLeft)
            {
                Shoot(direction);
            }
            else
            {
                Reload();
            }
    }

    private void Shoot(Vector3 direction)
    {
        currentAmmo--;
        GameObject bullet = GameObject.Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.Euler(Vector3.forward));
        bullet.GetComponent<Bullet>().Shoot(range, damage, direction);
    }

    private void Reload()
    {
        isReloading = true;
    }
}