using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private Vector3 fireDir;

    private float projectileSpeed;

    public void SetUp(Vector3 fireDir)
    {
        this.fireDir = fireDir;
    }


    void Start()
    {
        projectileSpeed = 50f;
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        transform.position += fireDir * projectileSpeed * Time.deltaTime;
    }
}
