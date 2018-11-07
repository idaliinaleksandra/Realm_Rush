using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour {

    [SerializeField] Collider collisionMesh;
    [SerializeField] int hitPoints = 2;

    void Start()
    {
        AddNonTriggerBoxCollider();
    }

    void AddNonTriggerBoxCollider()
    {
        Collider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = false;
    }

    public void OnParticleCollision(GameObject other)
    {
        ProcessHits();
        if (hitPoints <= 0)
        {
            KillEnemy();
        }
    }

    private void ProcessHits()
    {
        hitPoints = hitPoints - 1;
    }

    public void KillEnemy()
    {
        Destroy(gameObject);
    }
}
