using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
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
            Transform target = waypointList[currentWaypointIndex];
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

            // ���� ���� ����ȭ
            Vector3 direction = (targetPosition - transform.position).normalized;

            // �ӵ��� Time.deltaTime ����
            transform.position += direction * speed * Time.deltaTime;

            // Waypoint�� �����ϸ� ���� Waypoint�� ��ȯ
            if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
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
