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
    public GameObject highlightPrefab; // 하이라이트 프리팹 (SpriteRenderer 포함)

    private GameObject highlight;    // 하이라이트 오브젝트
    private Vector3Int previousCell; // 이전 셀 좌표
    private bool isMouseOverTile;    // 마우스가 타일 위에 있는지 확인

    private int playerResources = 5000;

    internal void SetInit()
    {
        Instance = this;
    }

    void Start()
    {
        // 하이라이트 오브젝트 생성
        highlight = Instantiate(highlightPrefab);
        highlight.SetActive(false); // 초기에는 비활성화
        previousCell = Vector3Int.one * int.MaxValue; // 초기값 설정
    }

    void Update()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;

        Vector3Int cellPosition = tilemap.WorldToCell(mouseWorldPos);

        if (tilemap.HasTile(cellPosition))
        {
            // 새로운 타일에 들어갔을 경우만 처리
            if (cellPosition != previousCell)
            {
                UpdateHighlight(cellPosition);
                previousCell = cellPosition;
            }

            isMouseOverTile = true;
        }
        else
        {
            // 타일맵 밖으로 나가면 하이라이트 비활성화
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
                // 마우스 클릭으로 타워 배치
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
        // 월드 좌표 계산
        Vector3 cellWorldPosition = tilemap.GetCellCenterWorld(cellPosition);

        if (CanPlaceTower(cellPosition))
        {
            highlight.SetActive(true);
            highlight.transform.position = cellWorldPosition;
            highlight.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 0.5f); // 초록색
        }
        else
        {
            highlight.SetActive(true);
            highlight.transform.position = cellWorldPosition;
            highlight.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.5f); // 빨간색
        }
    }

    private void PlaceTower(Vector3Int cellPosition)
    {
        Vector3 cellWorldPosition = tilemap.GetCellCenterWorld(cellPosition);
        Instantiate(towerPrefab, cellWorldPosition, Quaternion.identity, tilemap.transform);
        playerResources -= towerCost;
    }
}
