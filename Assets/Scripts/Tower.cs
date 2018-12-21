using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    [SerializeField] Transform objectToPan;
    [SerializeField] float attackRange = 10f;
    [SerializeField] ParticleSystem projectileParticle;
    [SerializeField] AudioClip pewSFX;

    public Waypoint baseWaypoint;

    Transform target;

    // Update is called once per frame
    void Update () {

        SetEnemy();

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

    private void SetEnemy()
    {
        var sceneEnemies = FindObjectsOfType<DamageDealer>();
        if (sceneEnemies.Length == 0) { return; }

        Transform closestEnemy = sceneEnemies[0].transform;

        foreach(DamageDealer testEnemy in sceneEnemies)
        {
            closestEnemy = GetClosest(closestEnemy, testEnemy.transform);
        }

        target = closestEnemy;
    }

    private Transform GetClosest(Transform transformA, Transform transformB)
    {
        var distToA = Vector3.Distance(transform.position, transformA.position);
        var distToB = Vector3.Distance(transform.position, transformB.position);

        if (distToA < distToB)
        {
            return transformA;
        }

        return transformB;
    }

    private void FireAtEnemy()
    {
        float distanceToEnemy = Vector3.Distance(target.transform.position, gameObject.transform.position);

        if(distanceToEnemy <= attackRange)
        {
            Shoot(true);
            GetComponent<AudioSource>().PlayOneShot(pewSFX);
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
