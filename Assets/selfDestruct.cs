using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selfDestruct : MonoBehaviour
{
    public float destructTime = 3.0f;
    public void Initiate()
    {
        Invoke("selfDestruct", destructTime);
    }
    private void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
