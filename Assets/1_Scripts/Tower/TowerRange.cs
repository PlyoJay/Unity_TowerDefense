using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TowerRange : MonoBehaviour
{
    public CircleCollider2D _attackRangeCollider;
    public Transform _rotationTransform;

    private LineRenderer _lineRenderer;
    private int _segments = 50;

    private HashSet<GameObject> monstersInRange = new HashSet<GameObject>();

    public void SetLineRenderer(CircleCollider2D collider)
    {
        _lineRenderer = GetComponent<LineRenderer>();

        _lineRenderer.startColor = Color.red;
        _lineRenderer.endColor = Color.red;
        _lineRenderer.startWidth = 0.03f;
        _lineRenderer.endWidth = 0.03f;
        _lineRenderer.positionCount = _segments + 1;
        _lineRenderer.useWorldSpace = false; // 로컬 좌표계 사용
        _lineRenderer.numCapVertices = 5;
        _lineRenderer.numCornerVertices = 5;

        DrawCircle(collider);

        _lineRenderer.enabled = false;
    }

    public void DrawCircle(CircleCollider2D collider)
    {
        float radius = collider.radius;
        Vector3 offset = collider.offset;

        float deltaTheta = (2f * Mathf.PI) / _segments;
        float theta = 0f;

        for (int i = 0; i <= _segments; i++)
        {
            float x = radius * Mathf.Cos(theta);
            float y = radius * Mathf.Sin(theta);

            Vector3 pos = new Vector3(x, y, 0) + offset;
            _lineRenderer.SetPosition(i, pos);

            theta += deltaTheta;
        }
    }

    private void Start()
    {
        Tower tower = GetComponent<Tower>();
        _attackRangeCollider.radius = tower.TowerData.AttackRange;
        SetLineRenderer(_attackRangeCollider);
    }

    public void OnMouseOver()
    {
        _lineRenderer.enabled = true;
    }

    public void OnMouseExit()
    {
        _lineRenderer.enabled = false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Monster monster = collision.GetComponent<Monster>();
        if (monster != null)
        {
            monstersInRange.Add(monster.gameObject);
            Debug.Log($"ID number {monster.MonsterId} Monster Entered Tower Range");
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        Monster monster = collision.GetComponent<Monster>();
        if (monster != null)
        {
            // 타워에서 몬스터로의 방향 계산
            Vector2 direction = monster.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Z축 회전 적용 (2D에서)
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 400f * Time.deltaTime);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        Monster monster = collision.GetComponent<Monster>();
        if (monster != null)
        {
            monstersInRange.Remove(monster.gameObject);
            Debug.Log($"ID number {monster.MonsterId} Monster Exit Tower Range");
        }
    }
}
