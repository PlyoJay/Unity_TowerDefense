using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    public GameObject monsterPrefab; // 몬스터 프리팹
    public float spawnInterval = 2f; // 몬스터 생성 간격 (초)
    public int _maxMonsters = 10; // 최대 몬스터 수

    private List<Transform> _waypointList; // 몬스터가 따라갈 Waypoints
    private int _spawnedMonsters = 0; // 생성된 몬스터 수

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
            // 최대 몬스터 수에 도달하면 스폰 중지
            CancelInvoke(nameof(SpawnMonster));
            return;
        }

        // 몬스터 생성

        Monster monsterObj = Instantiate<GameObject>(monsterPrefab, _waypointList[0].position, Quaternion.identity).GetComponent<Monster>();
        monsterObj.Init(_spawnedMonsters);
        //Monster monster = Instantiate<Monster>(monsterObj, _waypointList[0].position, Quaternion.identity);

        _spawnedMonsters++;
    }
}
