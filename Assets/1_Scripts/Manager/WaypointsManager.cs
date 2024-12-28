using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WaypointsManager : MonoBehaviour
{
    public static WaypointsManager Instance;

    public List<Transform> waypointList;

    public void SetInit()
    {
        Instance = this;
    }

    public void Activate()
    {
        GetWaypointList();
    }

    private void GetWaypointList()
    {
        if (waypointList == null) waypointList = new List<Transform>();
        else waypointList.Clear();

        foreach (Transform child in transform)
        {
            waypointList.Add(child.gameObject.transform);
        }
    }
}
