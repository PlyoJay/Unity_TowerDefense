using UnityEngine;

public class Tower : TowerBase
{
    public CircleCollider2D AttackRangeCollider;

    public void Init()
    {
        InitTower(0, "Tower1", 20, 2.0f, 1.5f, 40f, 1.0f, 50);
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
            Debug.Log($"ID number {monster.MonsterId} Monster Entered Tower Range");
        }
    }

    public override void Start()
    {
        base.Start();

        base.SetLineRenderer(AttackRangeCollider);
    }
}
