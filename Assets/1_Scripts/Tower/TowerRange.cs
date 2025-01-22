using System.Collections.Generic;
using UnityEngine;

public class TowerRange : MonoBehaviour
{
    [SerializeField] private Tower _tower;

    private HashSet<GameObject> _monstersInRange = new HashSet<GameObject>();

    private float _lastAttackTime = 0f;

    private void Attack()
    {
        List<GameObject> monsterListToRemove = new List<GameObject>();

        foreach (GameObject monster in _monstersInRange)
        {
            Monster monsterScript = monster.GetComponent<Monster>();

            if (monsterScript.IsAlive == false)
            {
                monsterListToRemove.Add(monster);
                continue;
            }

            monsterScript.TakeDamage(_tower.TowerData.AttackPower, _tower.TowerData.ArmorPenetration);
        }

        foreach (var monster in monsterListToRemove)
        {
            _monstersInRange.Remove(monster);
            Destroy(monster);
        }
    }

    private void Start()
    {
    
    }

    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Monster monster = collision.GetComponent<Monster>();
        if (monster != null)
        {
            _monstersInRange.Add(monster.gameObject);
            Debug.Log($"ID number {monster.MonsterId} Monster Entered Tower Range");
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        Monster monster = collision.GetComponent<Monster>();
        if (monster != null)
        {
            // 타워에서 몬스터로의 방향 계산
            Vector2 direction = monster.transform.position - _tower.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Z축 회전 적용 (2D에서)
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            _tower.transform.rotation = Quaternion.Slerp(_tower.transform.rotation, rotation, 300f * Time.deltaTime);

            if (Time.time >= _lastAttackTime + _tower.TowerData.AttackSpeed)
            {
                Attack();
                _lastAttackTime = Time.time; // 마지막 공격 시간 업데이트
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        Monster monster = collision.GetComponent<Monster>();
        if (monster != null)
        {
            _monstersInRange.Remove(monster.gameObject);
            Debug.Log($"ID number {monster.MonsterId} Monster Exit Tower Range");
        }
    }
}
