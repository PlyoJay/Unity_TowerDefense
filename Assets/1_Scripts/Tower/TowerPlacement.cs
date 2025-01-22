using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class TowerPlacement : MonoBehaviour
{
    public static TowerPlacement Instance;

    public Tilemap _tilemap;
    public GameObject _towerPrefab;
    public int _towerCost = 100;
    public GameObject _highlightPrefab; // 하이라이트 프리팹 (SpriteRenderer 포함)

    private HashSet<Vector3Int> _occupiedCells = new HashSet<Vector3Int>();

    private GameObject _highlight;    // 하이라이트 오브젝트
    private Vector3Int _previousCell; // 이전 셀 좌표
    private bool _isMouseOverTile;    // 마우스가 타일 위에 있는지 확인

    private int _playerResources = 5000;

    internal void SetInit()
    {
        Instance = this;
    }

    void Start()
    {
        // 하이라이트 오브젝트 생성
        _highlight = Instantiate(_highlightPrefab);
        _highlight.SetActive(false); // 초기에는 비활성화
        _previousCell = Vector3Int.one * int.MaxValue; // 초기값 설정
    }

    void Update()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;

        Vector3Int cellPosition = _tilemap.WorldToCell(mouseWorldPos);

        if (_tilemap.HasTile(cellPosition))
        {
            // 새로운 타일에 들어갔을 경우만 처리
            if (cellPosition != _previousCell)
            {
                UpdateHighlight(cellPosition);
                _previousCell = cellPosition;
            }

            _isMouseOverTile = true;
        }
        else
        {
            // 타일맵 밖으로 나가면 하이라이트 비활성화
            if (_isMouseOverTile)
            {
                _highlight.SetActive(false);
                _isMouseOverTile = false;
            }
        }

        if (_tilemap.HasTile(cellPosition))
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
        return IsCellEmpty(cellPosition) && _playerResources >= _towerCost;
    }

    private bool IsCellEmpty(Vector3Int cellPosition)
    {
        return !_occupiedCells.Contains(cellPosition);
    }

    private void UpdateHighlight(Vector3Int cellPosition)
    {
        // 월드 좌표 계산
        Vector3 cellWorldPosition = _tilemap.GetCellCenterWorld(cellPosition);

        if (CanPlaceTower(cellPosition))
        {
            _highlight.SetActive(true);
            _highlight.transform.position = cellWorldPosition;
            _highlight.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 0.5f); // 초록색
        }
        else
        {
            _highlight.SetActive(true);
            _highlight.transform.position = cellWorldPosition;
            _highlight.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.5f); // 빨간색
        }
    }

    private void PlaceTower(Vector3Int cellPosition)
    {
        Vector3 cellWorldPosition = _tilemap.GetCellCenterWorld(cellPosition);
        Instantiate(_towerPrefab, cellWorldPosition, Quaternion.identity, _tilemap.transform);
        _playerResources -= _towerCost;

        _occupiedCells.Add(cellPosition);
    }
}
