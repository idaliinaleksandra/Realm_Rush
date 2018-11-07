using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFinder : MonoBehaviour {

    [SerializeField] Waypoint startWaypoint, endWaypoint;
    Queue<Waypoint> queue = new Queue<Waypoint>();

    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Vector2Int[] directions = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left};
    bool isRunning = true;
    Waypoint searchCenter; // the current searchCenter
    List<Waypoint> path = new List<Waypoint>();

    public List<Waypoint> GetPath()
    {
        if (path.Count == 0)
        {
            LoadBlocks();
            ColorStartAndEnd();
            BreadthFirstSearch();
            CreatePath();
        }
        return path;
    }

    private void CreatePath()
    {
        path.Add(endWaypoint);
        Waypoint previous = endWaypoint.exploredFrom;
        while (previous != startWaypoint) 
        {
            path.Add(previous);
            previous = previous.exploredFrom;
        }

        path.Add(startWaypoint);
        path.Reverse();
    }

    private void BreadthFirstSearch()
    {
        queue.Enqueue(startWaypoint);

        while (queue.Count > 0 && isRunning)
        {
            searchCenter = queue.Dequeue();
            HaultIfEndFound();
            ExploreNeigbours();
            searchCenter.isExplored = true;
        }
    }

    private void HaultIfEndFound()
    {
        if (searchCenter == endWaypoint)
        {
            isRunning = false;
        }
    }

    private void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<Waypoint>();
        foreach(Waypoint waypoint in waypoints)
        {
            var gridPos = waypoint.GetGridPos();

            bool isOverlapping = grid.ContainsKey(gridPos);

            if (isOverlapping)
            {
                Debug.LogWarning("Skipping overlapping block" + waypoint);
            }
            else
            {
                grid.Add(gridPos, waypoint);
            }
        }
    }

    private void ColorStartAndEnd()
    {
        startWaypoint.SetTopColor(Color.green);
        endWaypoint.SetTopColor(Color.blue);
    }

    private void ExploreNeigbours()
    {

        if (!isRunning) { return; }

        foreach (Vector2Int direction in directions)
        {

            Vector2Int neighbourCoordinates = searchCenter.GetGridPos() + direction;
            if (grid.ContainsKey(neighbourCoordinates))
            {
                QueueNewNeighbour(neighbourCoordinates);
            }
        }
    }

    private void QueueNewNeighbour(Vector2Int neighbourCoordinates)
    {
        Waypoint neighbour = grid[neighbourCoordinates];
        if (neighbour.isExplored || queue.Contains(neighbour))
        {
            // do nothing
        }
        else
        { 
            queue.Enqueue(neighbour);
            neighbour.exploredFrom = searchCenter;
        }
    }
}
