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
}
