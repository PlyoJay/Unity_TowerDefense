using UnityEngine;

public class TowerRangeHightlight : MonoBehaviour
{
    [SerializeField] private CircleCollider2D _attackRangeCollider;
    [SerializeField] private Tower _tower;

    private LineRenderer _lineRenderer;

    private int _segments = 50;

    private void Start()
    {
        Tower tower = GetComponentInParent<Tower>();
        SetLineRenderer(_attackRangeCollider);
    }

    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // 타워를 인식할 레이어마스크 (예: LayerMask.GetMask("Tower"))
        int layerMask = 1 << LayerMask.NameToLayer("Tower");

        // 마우스 위치에서 콜라이더를 검사
        Collider2D hit = Physics2D.OverlapPoint(mousePosition, layerMask);

        if (hit != null && hit.gameObject == this.gameObject)
        {
            // 마우스가 타워 위에 있음
            _lineRenderer.enabled = true;
        }
        else
        {
            // 마우스가 타워 위에 없음
            _lineRenderer.enabled = false;
        }
    }

    private void SetLineRenderer(CircleCollider2D collider)
    {
        _lineRenderer = GetComponent<LineRenderer>();

        _lineRenderer.startColor = Color.red;
        _lineRenderer.endColor = Color.red;
        _lineRenderer.startWidth = 0.04f;
        _lineRenderer.endWidth = 0.04f;
        _lineRenderer.positionCount = _segments + 1;
        _lineRenderer.useWorldSpace = false; // 로컬 좌표계 사용
        _lineRenderer.numCapVertices = 5;
        _lineRenderer.numCornerVertices = 5;

        DrawCircle(collider);

        _lineRenderer.enabled = false;
    }

    private void DrawCircle(CircleCollider2D collider)
    {
        float radius = collider.radius;
        Vector3 offset = collider.offset;

        float deltaTheta = (2f * Mathf.PI) / _segments;
        float theta = 0f;

        for (int i = 0; i <= _segments; i++)
        {
            float x = radius * Mathf.Cos(theta);
            float y = radius * Mathf.Sin(theta);

            Vector3 pos = new Vector3(x, y, -1) + offset;
            _lineRenderer.SetPosition(i, pos);

            theta += deltaTheta;
        }
    }
}
