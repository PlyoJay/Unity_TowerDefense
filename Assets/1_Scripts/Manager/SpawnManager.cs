using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    public GameObject monsterPrefab; // ���� ������
    public float spawnInterval = 2f; // ���� ���� ���� (��)
    public int maxMonsters = 10; // �ִ� ���� ��

    private List<Transform> waypointList; // ���Ͱ� ���� Waypoints
    private int spawnedMonsters = 0; // ������ ���� ��

    public void SetInit()
    {
        Instance = this;
    }

    public void Activate()
    {
        waypointList = WaypointsManager.Instance.waypointList;

        InvokeRepeating(nameof(SpawnMonster), 0f, spawnInterval);
    }

    void SpawnMonster()
    {
        if (spawnedMonsters >= maxMonsters)
        {
            // �ִ� ���� ���� �����ϸ� ���� ����
            CancelInvoke(nameof(SpawnMonster));
            return;
        }

        // ���� ����
        GameObject monster = Instantiate(monsterPrefab, waypointList[0].position, Quaternion.identity);

        spawnedMonsters++;
    }
}
