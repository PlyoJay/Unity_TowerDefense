using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public List<Vector3> pathNodes;
    public List<Transform> waypointList;
    public float speed = 2f; // 이동 속도
    private int currentWaypointIndex = 0; // 현재 목표 Waypoint 인덱스

    private void Start()
    {
        waypointList = WaypointsManager.Instance.waypointList;
    }

    void Update()
    {
        if (currentWaypointIndex < waypointList.Count)
        {
            // 현재 Waypoint로 이동
            Transform target = waypointList[currentWaypointIndex];
            Vector3 direction = target.position - transform.position;
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

            // Waypoint에 도달하면 다음 Waypoint로 전환
            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                currentWaypointIndex++;
            }
        }
        else
        {
            // 끝 지점 도달 후 처리 (예: 제거, 점수 추가 등)
            Destroy(gameObject);
        }
    }
}
