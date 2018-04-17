using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour {

    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;
    public int currentAmmo;
    public int maxAmmo = 10;

    float nadeTimer;
    public float timeBetweenThrows = 1f;
    public int maxGrenadeCount;
    public int currentGrenadeCount;
    public GameObject grenade;

    float timer;
    Ray shootRay;
    RaycastHit shootHit;
    //int shootableMask;
    //ParticleSystem gunParticles;
    LineRenderer gunLine;
    Light gunLight;
    float effectsDisplayTime = 0.2f;


    float ammoTimer;
    public float reloadTime = 1;

    public Text ammoText;

    public CameraShake cameraShake;
    public float shakeDuration = 0f;
    public float shakeMagnitude = 0f;

    private void Awake()
    {
        //shootableMask = LayerMask.GetMask("ShootableMask");
        gunLine = GetComponent<LineRenderer>();
        gunLight = GetComponent<Light>();
        currentAmmo = maxAmmo;
        currentGrenadeCount = maxGrenadeCount;
    }

    private void Update()
    {
        ammoText.text = currentAmmo.ToString("00");

        timer += Time.deltaTime;
        nadeTimer += Time.deltaTime;

        if (Input.GetButton("Fire1") && timer >= timeBetweenBullets && currentAmmo > 0)
        {
            Shoot();
            StartCoroutine(cameraShake.Shake(shakeDuration, shakeMagnitude));
        }

        if (Input.GetButton("Fire2") && nadeTimer >= timeBetweenThrows && currentGrenadeCount > 0)
        {
            ThrowGrenade();
        }

        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }

        if(currentAmmo == 0)
        {
            ammoTimer += Time.deltaTime;
            if(ammoTimer >= reloadTime)
            {
                currentAmmo = maxAmmo;
                ammoTimer = 0f;
            }
        }
    }

    public void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }

    void Shoot()
    {
        timer = 0f;
        gunLight.enabled = true;
        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if(Physics.Raycast(shootRay, out shootHit, range))
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
            if(enemyHealth != null)
            {
                enemyHealth.TakeDamage(damagePerShot, shootHit.point);
            }
            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }

        currentAmmo--;
    }

    void ThrowGrenade()
    {
        nadeTimer = 0f;
        Instantiate(grenade, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
        currentGrenadeCount--;
    }
}
