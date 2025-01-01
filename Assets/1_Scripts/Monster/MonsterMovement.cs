using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
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
            Transform target = waypointList[currentWaypointIndex];
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

            // 방향 벡터 정규화
            Vector3 direction = (targetPosition - transform.position).normalized;

            // 속도와 Time.deltaTime 적용
            transform.position += direction * speed * Time.deltaTime;

            // Waypoint에 도달하면 다음 Waypoint로 전환
            if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
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
