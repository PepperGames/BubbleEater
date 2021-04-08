using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Needle : MonoBehaviour
{
    private float timeToExplosion = 2f;

    void Update()
    {
        timeToExplosion -= Time.deltaTime;

        if (timeToExplosion <= 0)
        {
            Destroy(gameObject);
        }
    }
}
