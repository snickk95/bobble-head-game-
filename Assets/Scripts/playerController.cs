using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    //public float for move speed can be changed in unity editor
    public float moveSpeed = 50.0f;

    //variable to contain the character controller
    private CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        //to refrence the character controller component 
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // gets player input
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //moves the character
        characterController.SimpleMove(moveDirection * moveSpeed);

    }
}
