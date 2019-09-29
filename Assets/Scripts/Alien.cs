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

    public void Die()
    {
        OnDestroy.Invoke();

        OnDestroy.RemoveAllListeners();

        Destroy(gameObject);
    }


    void OnTriggerEnter(Collider other)
    {
        Die();
        soundManager.Instance.PlayOneShot(soundManager.Instance.alienDeath);
    }
}
