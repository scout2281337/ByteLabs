using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width, _height;

    [SerializeField] private Tile _tilePrefab;

    [SerializeField] private float _tileSize;

    //[SerializeField] private Transform _cam;

    private Dictionary<Vector2, Tile> _tiles;

    public static GridManager instance;

    private void Awake()
    {
        instance = this;
        GameManager.OnGameStateChanged += ActivateGrid;
    }
    private void Start()
    {
        
    }

    public void GenerateGrid()
    {
        _tiles = new Dictionary<Vector2, Tile>();
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x + 0.5f, 0, y + 0.5f) * _tileSize + transform.position, Quaternion.Euler(90, 0, 0), this.transform);
                spawnedTile.name = $"Tile {x} {y}";
                Vector2 ind = new Vector2(x, y);
                spawnedTile.Init(_tileSize, ind);
                if(y<4) spawnedTile.State(TileState.emptyZone);
                else spawnedTile.State(TileState.enemyZone);
                _tiles[ind] = spawnedTile;
            }

        }
        Debug.Log("State::GenerateGrid");       
    }

    public Tile GetTileAtPosition(Vector2 pos)
    {
        if (_tiles.TryGetValue(pos, out var tile)) return tile;
        return null;
    }

    public void ActivateGrid(GameState state)
    {
        //Debug.Log("ActivateGrid");
        if (state == GameState.HeroesTurn || state == GameState.GenerateGrid)
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
    }
}
