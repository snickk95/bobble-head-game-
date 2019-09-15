using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //destroy the object when no longer on camera
    private void OnBecomeInvisible()
    {
        Destroy(gameObject);
    }

    //destroy when the object hits the alien
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
