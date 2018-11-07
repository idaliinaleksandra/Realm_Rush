using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode] [SelectionBase] [RequireComponent(typeof(Waypoint))]
public class CubeEditor : MonoBehaviour {

    Vector3 gridPos;
    Waypoint waypoint;

    void Awake()
    {
        waypoint = GetComponent<Waypoint>();
    }

    void Update()
    {
        SnapToGrid();

        UpdateLabel();
    }

    void SnapToGrid()
    {
        int gridSize = waypoint.GetGridSize();
        transform.position = new Vector3(waypoint.GetGridPos().x * gridSize, 0f, waypoint.GetGridPos().y * gridSize);
    }

    private void UpdateLabel()
    {
        int gridSize = waypoint.GetGridSize();
        Vector2 gridPos = waypoint.GetGridPos();
        TextMesh textMesh = GetComponentInChildren<TextMesh>();

        string labelText = gridPos.x + "," + gridPos.y;
        textMesh.text = labelText;
        gameObject.name = labelText;
    }
}
