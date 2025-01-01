using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

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
    private LineRenderer lineRenderer;
    private int segments = 50;

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

    public void SetLineRenderer(CircleCollider2D collider)
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.startWidth = 0.03f;
        lineRenderer.endWidth = 0.03f;
        lineRenderer.positionCount = segments + 1;
        lineRenderer.useWorldSpace = false; // 로컬 좌표계 사용
        lineRenderer.numCapVertices = 5;
        lineRenderer.numCornerVertices = 5;

        DrawCircle(collider);

        lineRenderer.enabled = false;
    }

    public void DrawCircle(CircleCollider2D collider)
    {
        float radius = collider.radius;
        Vector3 offset = collider.offset;

        float deltaTheta = (2f * Mathf.PI) / segments;
        float theta = 0f;

        for (int i = 0; i <= segments; i++)
        {
            float x = radius * Mathf.Cos(theta);
            float y = radius * Mathf.Sin(theta);

            Vector3 pos = new Vector3(x, y, 0) + offset;
            lineRenderer.SetPosition(i, pos);

            theta += deltaTheta;
        }
    }

    public void EnableCircle()
    {
        lineRenderer.enabled = true;
    }

    public void DisableCircle()
    {
        lineRenderer.enabled = false;
    }

    public virtual void Start()
    {
        
    }
}
