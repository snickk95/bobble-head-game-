using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public GameObject bulletPrefab;
    public Transform launchPosition;
    public bool isUpgraded;
    public float upgradeTime = 10.0f;

    private float currentTime;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void fireBullet()
    {

        Rigidbody bullet = createBullet();
        bullet.velocity = transform.parent.forward * 100;

        

        if (isUpgraded)
        {
            Rigidbody bullet2 = createBullet();
            bullet2.velocity =
            (transform.right + transform.forward / 0.5f) * 100;
            Rigidbody bullet3 = createBullet();
            bullet3.velocity =
            ((transform.right * -1) + transform.forward / 0.5f) * 100;
        }

        if (isUpgraded)
        {
            audioSource.PlayOneShot(soundManager.Instance.upgradedGunFire);
        }
        else
        {
            audioSource.PlayOneShot(soundManager.Instance.gunFire);
        }
    }

    private Rigidbody createBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab) as GameObject;
        bullet.transform.position = launchPosition.position;
        return bullet.GetComponent<Rigidbody>();
    }


    public void UpgradeGun()
    {
        isUpgraded = true;
        currentTime = 0;
    }


    // Update is called once per frame
    void Update()
    {
        // check if mouse button pressed to call firing the bullet
        if (Input.GetMouseButtonDown(0))
        {
            if (!IsInvoking("fireBullet"))
            {
                InvokeRepeating("fireBullet", 0f, 0.1f);
            }
        }

        // stops the bullet from firing again when you let go of mouse button
        if (Input.GetMouseButtonUp(0))
        {
            CancelInvoke("fireBullet");
        }

    }
}
