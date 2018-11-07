using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    [SerializeField] Transform objectToPan;
    [SerializeField] Transform target;
    [SerializeField] float attackRange = 10f;
    [SerializeField] ParticleSystem projectileParticle;

	// Update is called once per frame
	void Update () {
        if (target)
        {
            objectToPan.LookAt(target);
            FireAtEnemy();
        }
        else
        {
            Shoot(false);
        }
	}

    private void FireAtEnemy()
    {
        float distanceToEnemy = Vector3.Distance(target.transform.position, gameObject.transform.position);

        if(distanceToEnemy <= attackRange)
        {
            Shoot(true);
        } else
        {
            Shoot(false);
        }
    }

    private void Shoot(bool isActive)
    {
        var emissionModule = projectileParticle.emission;
        emissionModule.enabled = isActive;
    }
}
