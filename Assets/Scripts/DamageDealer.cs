using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour {

    [SerializeField] Collider collisionMesh;
    [SerializeField] int hitPoints = 2;
    [SerializeField] ParticleSystem hitParticlePrefab;
    [SerializeField] ParticleSystem deathParticlePrefab;
    [SerializeField] AudioClip enemyHitsSFX;
    [SerializeField] AudioClip enemyDeathSFX;

    [SerializeField] GameObject camera;

    AudioSource myAudioSource;

    void Start()
    {
        AddNonTriggerBoxCollider();
        myAudioSource = GetComponent<AudioSource>();
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
        hitParticlePrefab.Play();
        myAudioSource.PlayOneShot(enemyHitsSFX);
    }

    public void KillEnemy()
    {
        var vfx = Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
        vfx.Play();

        Destroy(vfx.gameObject, vfx.main.duration);
        AudioSource.PlayClipAtPoint(enemyDeathSFX, camera.transform.position);
        Destroy(gameObject);
    }
}
