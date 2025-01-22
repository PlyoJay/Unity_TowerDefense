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
    public GameObject _highlightPrefab; // ���̶���Ʈ ������ (SpriteRenderer ����)

    private HashSet<Vector3Int> _occupiedCells = new HashSet<Vector3Int>();

    private GameObject _highlight;    // ���̶���Ʈ ������Ʈ
    private Vector3Int _previousCell; // ���� �� ��ǥ
    private bool _isMouseOverTile;    // ���콺�� Ÿ�� ���� �ִ��� Ȯ��

    private int _playerResources = 5000;

    internal void SetInit()
    {
        Instance = this;
    }

    void Start()
    {
        // ���̶���Ʈ ������Ʈ ����
        _highlight = Instantiate(_highlightPrefab);
        _highlight.SetActive(false); // �ʱ⿡�� ��Ȱ��ȭ
        _previousCell = Vector3Int.one * int.MaxValue; // �ʱⰪ ����
    }

    void Update()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;

        Vector3Int cellPosition = _tilemap.WorldToCell(mouseWorldPos);

        if (_tilemap.HasTile(cellPosition))
        {
            // ���ο� Ÿ�Ͽ� ���� ��츸 ó��
            if (cellPosition != _previousCell)
            {
                UpdateHighlight(cellPosition);
                _previousCell = cellPosition;
            }

            _isMouseOverTile = true;
        }
        else
        {
            // Ÿ�ϸ� ������ ������ ���̶���Ʈ ��Ȱ��ȭ
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
        return IsCellEmpty(cellPosition) && _playerResources >= _towerCost;
    }

    private bool IsCellEmpty(Vector3Int cellPosition)
    {
        return !_occupiedCells.Contains(cellPosition);
    }

    private void UpdateHighlight(Vector3Int cellPosition)
    {
        // ���� ��ǥ ���
        Vector3 cellWorldPosition = _tilemap.GetCellCenterWorld(cellPosition);

        if (CanPlaceTower(cellPosition))
        {
            _highlight.SetActive(true);
            _highlight.transform.position = cellWorldPosition;
            _highlight.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 0.5f); // �ʷϻ�
        }
        else
        {
            _highlight.SetActive(true);
            _highlight.transform.position = cellWorldPosition;
            _highlight.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.5f); // ������
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
