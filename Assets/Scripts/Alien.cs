using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Alien : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent agent;
    public UnityEvent OnDestroy;
    public float navigationUpdate;
    public Rigidbody head;
    public bool isAlive = true;
    private float navigationTime=0;

    // Start is called before the first frame update
    void Start()
    {
        //refrence to nav mesh to access in code
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            if (target != null)
            {
                // old way
                // agent.destination = target.position;

                //new way
                navigationTime += Time.deltaTime;
                if (navigationTime > navigationUpdate)
                {
                    agent.destination = target.position;
                    navigationTime = 0;
                }
            }
        }
    }

    public void Die()
    {
        OnDestroy.Invoke();

        OnDestroy.RemoveAllListeners();

        head.GetComponent<selfDestruct>().Initiate();

        Destroy(gameObject);
    }

    public void die()
    {
        isAlive = false;
        head.GetComponent<Animator>().enabled = false;
        head.isKinematic = false;
        head.useGravity = true;
        head.GetComponent<SphereCollider>().enabled = true;
        head.gameObject.transform.parent = null;
        head.velocity = new Vector3(0, 26.0f, 3.0f);
        OnDestroy.Invoke();
        OnDestroy.RemoveAllListeners();
        soundManager.Instance.PlayOneShot(soundManager.Instance.alienDeath);
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        Die();
        soundManager.Instance.PlayOneShot(soundManager.Instance.alienDeath);
    }

}
