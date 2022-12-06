using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBehavior : MonoBehaviour
{
public Transform firePoint;
    private float bulletSpeed = 2f;
    private float nextFire = 0f;
    public GameObject bulletPrefab;

[SerializeField] private float cooldown = 0.5f;
private float cooldownTimer;

void Update(){
    ShootAtPlayer();
}

void ShootAtPlayer()
{
    cooldownTimer -= Time.deltaTime;

    if(cooldownTimer > 0) return;

    cooldownTimer = cooldown;
     
    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D re = bullet.GetComponent<Rigidbody2D>();
            re.velocity = firePoint.up * bulletSpeed;
    Destroy(bullet, 10f);
}
}
    
