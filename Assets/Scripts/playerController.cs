using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    //public float for move speed can be changed in unity editor
    public float moveSpeed = 50.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //create a vector variable that is the player character
        Vector3 pos = transform.position;


        // makes position x or z go up depending on command. 
        pos.x += moveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
        pos.z += moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;

        //set space marine to new position
        transform.position = pos;
    }
}
