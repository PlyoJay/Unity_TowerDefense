using UnityEngine;

public class TowerData
{
    public string Type;
    public float Power;
    public float AttackSpeed;
    public float AttackRange;
    public float ArmorPenetration;
    public float SplashRadius;
    public int Price;
}

public class TowerBase : MonoBehaviour
{
    public TowerData TowerData { get; set; }
    public int TowerId { get; set; }

    public void InitTower(int towerId, string type, float power, float attackSpeed, float attackRange,
                            float armorPenetration, float splashRadius, int price)
    {
        TowerId = towerId;
        TowerData = new TowerData()
        {
            Type = type,
            Power = power,
            AttackSpeed = attackSpeed,
            AttackRange = attackRange,
            ArmorPenetration = armorPenetration,
            SplashRadius = splashRadius,
            Price = price
        };
    }

    public virtual void SetRange()
    {

    }
}
