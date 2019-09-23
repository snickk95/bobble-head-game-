using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    //public float for move speed can be changed in unity editor
    public float moveSpeed = 50.0f;
    public Rigidbody head;
    public LayerMask layerMask;
    private Vector3 currentLookTarget = Vector3.zero;

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

    private void FixedUpdate()
    {
        //gets the direction character is moving
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //detects if marine is standing still
        if (moveDirection==Vector3.zero)
        {

        }
        //makes head move in a direction when movment is happening
        else
        {
            head.AddForce(transform.right * 150, ForceMode.Acceleration);
        }
        
        RaycastHit hit;
        //sending the ray from the main camera to the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //this just shows you the ray well playing the game
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.green);

        // physics. ray cast makes the ray then checks if it hit 1000 is how long the ray is the layer mask shows what the player is trying to hit 
        //the ignore tells the physics engine not to activate triggers
        if (Physics.Raycast(ray, out hit, 1000, layerMask, QueryTriggerInteraction.Ignore))
        {
            if (hit.point != currentLookTarget)
            {
                currentLookTarget = hit.point;
            }
           
        }
        Vector3 targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
        Quaternion rotation = Quaternion.LookRotation(targetPosition - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 10.0f);
    }
}
