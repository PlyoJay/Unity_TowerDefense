using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public List<Vector3> pathNodes;
    public List<Transform> waypointList;
    public float speed = 2f; // �̵� �ӵ�
    private int currentWaypointIndex = 0; // ���� ��ǥ Waypoint �ε���

    private void Start()
    {
        waypointList = WaypointsManager.Instance.waypointList;
    }

    void Update()
    {
        if (currentWaypointIndex < waypointList.Count)
        {
            // ���� Waypoint�� �̵�
            Transform target = waypointList[currentWaypointIndex];
            Vector3 direction = target.position - transform.position;
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

            // Waypoint�� �����ϸ� ���� Waypoint�� ��ȯ
            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                currentWaypointIndex++;
            }
        }
        else
        {
            // �� ���� ���� �� ó�� (��: ����, ���� �߰� ��)
            Destroy(gameObject);
        }
    }
}
