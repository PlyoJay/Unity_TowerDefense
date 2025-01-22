using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class TowerPlacement : MonoBehaviour
{
    public static TowerPlacement Instance;

    public Tilemap tilemap;
    public GameObject towerPrefab;
    public int towerCost = 100;
    public GameObject highlightPrefab; // ���̶���Ʈ ������ (SpriteRenderer ����)

    private GameObject highlight;    // ���̶���Ʈ ������Ʈ
    private Vector3Int previousCell; // ���� �� ��ǥ
    private bool isMouseOverTile;    // ���콺�� Ÿ�� ���� �ִ��� Ȯ��

    private int playerResources = 5000;

    internal void SetInit()
    {
        Instance = this;
    }

    void Start()
    {
        // ���̶���Ʈ ������Ʈ ����
        highlight = Instantiate(highlightPrefab);
        highlight.SetActive(false); // �ʱ⿡�� ��Ȱ��ȭ
        previousCell = Vector3Int.one * int.MaxValue; // �ʱⰪ ����
    }

    void Update()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;

        Vector3Int cellPosition = tilemap.WorldToCell(mouseWorldPos);

        if (tilemap.HasTile(cellPosition))
        {
            // ���ο� Ÿ�Ͽ� ���� ��츸 ó��
            if (cellPosition != previousCell)
            {
                UpdateHighlight(cellPosition);
                previousCell = cellPosition;
            }

            isMouseOverTile = true;
        }
        else
        {
            // Ÿ�ϸ� ������ ������ ���̶���Ʈ ��Ȱ��ȭ
            if (isMouseOverTile)
            {
                highlight.SetActive(false);
                isMouseOverTile = false;
            }
        }

        if (tilemap.HasTile(cellPosition))
        {
            if (Input.GetMouseButtonDown(0))
            {
                // ���콺 Ŭ������ Ÿ�� ��ġ
                if (CanPlaceTower(cellPosition))
                {
                    PlaceTower(cellPosition);
                }
            }
        }
    }

    private bool CanPlaceTower(Vector3Int cellPosition)
    {
        return IsCellEmpty(cellPosition) && playerResources >= towerCost;
    }

    private bool IsCellEmpty(Vector3Int cellPosition)
    {
        return tilemap.GetInstantiatedObject(cellPosition) == null;
    }

    private void UpdateHighlight(Vector3Int cellPosition)
    {
        // ���� ��ǥ ���
        Vector3 cellWorldPosition = tilemap.GetCellCenterWorld(cellPosition);

        if (CanPlaceTower(cellPosition))
        {
            highlight.SetActive(true);
            highlight.transform.position = cellWorldPosition;
            highlight.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 0.5f); // �ʷϻ�
        }
        else
        {
            highlight.SetActive(true);
            highlight.transform.position = cellWorldPosition;
            highlight.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.5f); // ������
        }
    }

    private void PlaceTower(Vector3Int cellPosition)
    {
        Vector3 cellWorldPosition = tilemap.GetCellCenterWorld(cellPosition);
        Instantiate(towerPrefab, cellWorldPosition, Quaternion.identity, tilemap.transform);
        playerResources -= towerCost;
    }
}
