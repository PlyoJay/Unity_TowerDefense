using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    public GameObject monsterPrefab; // ���� ������
    public float spawnInterval = 2f; // ���� ���� ���� (��)
    public int _maxMonsters = 10; // �ִ� ���� ��

    private List<Transform> _waypointList; // ���Ͱ� ���� Waypoints
    private int _spawnedMonsters = 0; // ������ ���� ��

    public void SetInit()
    {
        Instance = this;
    }

    public void Activate()
    {
        _waypointList = WaypointsManager.Instance.waypointList;

        InvokeRepeating(nameof(SpawnMonster), 0f, spawnInterval);
    }

    void SpawnMonster()
    {
        if (_spawnedMonsters >= _maxMonsters)
        {
            // �ִ� ���� ���� �����ϸ� ���� ����
            CancelInvoke(nameof(SpawnMonster));
            return;
        }

        // ���� ����

        Monster monsterObj = Instantiate<GameObject>(monsterPrefab, _waypointList[0].position, Quaternion.identity).GetComponent<Monster>();
        monsterObj.Init(_spawnedMonsters);
        //Monster monster = Instantiate<Monster>(monsterObj, _waypointList[0].position, Quaternion.identity);

        _spawnedMonsters++;
    }
}
