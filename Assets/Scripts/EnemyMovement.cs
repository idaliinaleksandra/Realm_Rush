using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    [SerializeField] float movementPeriod = 1f;
    [SerializeField] ParticleSystem goalParticle;

    void Start()
    {
        PathFinder pathfinder = FindObjectOfType<PathFinder>();
        var path = pathfinder.GetPath();
        StartCoroutine(FollowPath(path));
    }

     IEnumerator FollowPath(List<Waypoint> path)
    {
        print("Starting point");
        foreach(Waypoint waypoint in path)
        {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(movementPeriod);
        }
        SelfDestruct();
    }

    public void SelfDestruct()
    {
            var vfx = Instantiate(goalParticle, transform.position, Quaternion.identity);
            vfx.Play();
            Destroy(vfx.gameObject, vfx.main.duration);
            Destroy(gameObject);
    }
}
