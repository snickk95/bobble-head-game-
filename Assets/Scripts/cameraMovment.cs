using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovment : MonoBehaviour
{
    public GameObject followTarget;
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //fail safe for if the player is not detected
        if (followTarget != null)
        {
            transform.position = Vector3.Lerp(transform.position,
            followTarget.transform.position, Time.deltaTime * moveSpeed);

        }


    }
}
