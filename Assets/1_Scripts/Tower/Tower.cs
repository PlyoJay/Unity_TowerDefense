using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Tower : TowerBase
{
    public CircleCollider2D AttackRangeCollider;

    public Transform rotationTransform;
    private HashSet<GameObject> monstersInRange = new HashSet<GameObject>();

    public void Init()
    {
        InitTower(0, "Tower1", 20, 2.0f, 2.0f, 40f, 1.0f, 50);
    }

    public override void SetRange()
    {
        base.SetRange();

        AttackRangeCollider.radius = TowerData.AttackRange;
    }

    public void OnMouseOver()
    {
        base.EnableCircle();
    }

    public void OnMouseExit()
    {
        base.DisableCircle();
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
        Vector3 targetDirection = monster.transform.position - new Vector3(transform.position.x, transform.position.y, 0); ;
        Vector3 direct = targetDirection.normalized;

        if (direct != Vector3.zero)
        {
            rotationTransform.rotation = Quaternion.RotateTowards(rotationTransform.rotation,
                Quaternion.LookRotation(direct, Vector3.up),
                400f * Time.deltaTime);
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

    public override void Start()
    {
        base.Start();

        Init();
        SetRange();

        base.SetLineRenderer(AttackRangeCollider);
    }
}
