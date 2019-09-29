using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public GameObject bulletPrefab;
    public Transform launchPosition;


    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void fireBullet()
    {
        // creates a game object thats a  existing prefab 
        GameObject bullet = Instantiate(bulletPrefab) as GameObject;


        // sets the position of the object to where the gun is 
        bullet.transform.position = launchPosition.position;


        // atatch it to the rigidbody of the marine so it fires in the same direction the marine is facing
        bullet.GetComponent<Rigidbody>().velocity = transform.parent.forward * 100;

        audioSource.PlayOneShot(soundManager.Instance.gunFire);
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
