using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    //public float for move speed can be changed in unity editor
    public float moveSpeed = 50.0f;
    public Rigidbody head;
    public LayerMask layerMask;
    public Animator bodyAnimator;
    public float[] hitForce;
    public float timeBetweenHits;
    public Rigidbody marineBody;

    private DeathParticles deathParticles;
    private bool isDead = false;
    private bool isHit;
    private float timeSinceHit;
    private int hitNumber = -1;
    private Vector3 currentLookTarget = Vector3.zero;
    private CharacterController characterController;


    public void die()
    {
        bodyAnimator.SetBool("IsMoving", false);
        marineBody.transform.parent = null;
        marineBody.isKinematic = false;
        marineBody.useGravity = true;
        marineBody.gameObject.GetComponent<CapsuleCollider>().enabled = true;
        marineBody.gameObject.GetComponent<Gun>().enabled = false;

        Destroy(head.gameObject.GetComponent<HingeJoint>());
        head.transform.parent = null;
        head.useGravity = true;
        soundManager.Instance.PlayOneShot(soundManager.Instance.marineDeath);
        deathParticles.Activate();
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        deathParticles = gameObject.GetComponent<DeathParticles>();
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


        if (isHit)
        {
            timeSinceHit += Time.deltaTime;
            if (timeSinceHit > timeBetweenHits)
            {
                isHit = false;
                timeSinceHit = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        //gets the direction character is moving
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //detects if marine is standing still
        if (moveDirection==Vector3.zero)
        {
            bodyAnimator.SetBool("IsMoving", false);
        }
        //makes head move in a direction when movment is happening
        else
        {
            head.AddForce(transform.right * 150, ForceMode.Acceleration);

            bodyAnimator.SetBool("IsMoving", true);
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

    private void OnTriggerEnter(Collider other)
    {
        Alien alien = other.gameObject.GetComponent<Alien>();
        if (alien!=null)
        {
            if (!isHit)
            {
                hitNumber += 1;
                CameraShake cameraShake = Camera.main.GetComponent<CameraShake>();


                if(hitNumber< hitForce.Length)
                {
                    cameraShake.intensity = hitForce[hitNumber];
                    cameraShake.Shake();
                }


                else
                {
                    die();
                }


                isHit = true;
                soundManager.Instance.PlayOneShot(soundManager.Instance.hurt);
            }
            alien.Die();
        }
    }
}
