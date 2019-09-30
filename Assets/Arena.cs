using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena : MonoBehaviour
{

    public GameObject player;
    public Transform elevator;
    private Animator arenaAnimator;
    private SphereCollider sphereCollider;
    // Start is called before the first frame update
    void Start()
    {
        arenaAnimator = GetComponent<Animator>();
        sphereCollider = GetComponent<SphereCollider>();
    }

    void OnTriggerEnter(Collider other)
    {
        Camera.main.transform.parent.gameObject.
        GetComponent<cameraMovment>().enabled = false;
        player.transform.parent = elevator.transform;

        player.GetComponent<playerController>().enabled = false;

        soundManager.Instance.PlayOneShot(soundManager.Instance.elevatorArrived);

        arenaAnimator.SetBool("OnElevator", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
