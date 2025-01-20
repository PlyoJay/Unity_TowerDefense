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
        // Ÿ���� �ν��� ���̾��ũ (��: LayerMask.GetMask("Tower"))
        int layerMask = 1 << LayerMask.NameToLayer("Tower");

        // ���콺 ��ġ���� �ݶ��̴��� �˻�
        Collider2D hit = Physics2D.OverlapPoint(mousePosition, layerMask);

        if (hit != null && hit.gameObject == this.gameObject)
        {
            // ���콺�� Ÿ�� ���� ����
            _lineRenderer.enabled = true;
        }
        else
        {
            // ���콺�� Ÿ�� ���� ����
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
        _lineRenderer.useWorldSpace = false; // ���� ��ǥ�� ���
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
