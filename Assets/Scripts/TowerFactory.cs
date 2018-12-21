﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour {

    [SerializeField] int towerLimit = 5;
    [SerializeField] Tower towerPrefab;
    [SerializeField] GameObject towerParent;

    Queue<Tower> towerQueue = new Queue<Tower>();


    public void AddTower(Waypoint baseWaypoint)
    {
        int numTowers = towerQueue.Count;

        if (numTowers < towerLimit)
        {
            var newTower = Instantiate(towerPrefab, baseWaypoint.transform.position, Quaternion.identity);
            newTower.transform.parent = towerParent.transform;
            baseWaypoint.isPlaceable = false;

            newTower.baseWaypoint = baseWaypoint;
            baseWaypoint.isPlaceable = false;

            towerQueue.Enqueue(newTower);
        }
        else
        {
            MoveExistingTower(baseWaypoint);
        }
    }

    private void MoveExistingTower(Waypoint newBaseWaypoint)
    {
        var oldTower = towerQueue.Dequeue();

        oldTower.baseWaypoint.isPlaceable = true;
        newBaseWaypoint.isPlaceable = false;

        oldTower.baseWaypoint = newBaseWaypoint;
        oldTower.transform.position = newBaseWaypoint.transform.position;

        towerQueue.Enqueue(oldTower);
    }
}