using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private float _tileSpacing;
    [SerializeField] private int _gridWidth, _gridHeight;
    [SerializeField] private GameObject _tilePrefab;

    Tile[,] _tiles;


    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        _tiles = new Tile[_gridWidth, _gridHeight];
        float xOffset = (_gridWidth - 1) / 2f;
        float yOffset = (_gridHeight - 1) / 2f;
        for (int x = 0; x < _gridWidth; x++)
        {
            for (int y = 0; y < _gridHeight; y++)
            {
                Vector3 spawnPos = new((x - xOffset) * _tileSpacing, (y - yOffset) * _tileSpacing);
                GameObject spawnedTile = Instantiate(_tilePrefab, spawnPos, Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
                Tile newTile = spawnedTile.GetComponent<Tile>();
                newTile.Initialize(x, y);
                _tiles[x, y] = newTile;

            }
        }

    }

    public Tile GetRandomAdjacentTile(Tile tile, int range)
    {
        int newX = tile.X;
        int newY = tile.Y;

        int direction = Random.Range(0, 4);

        switch (direction)
        {
            case 0: newX += range; break;
            case 1: newX -= range; break;
            case 2: newY += range; break;
            case 3: newY -= range; break;
        }

        newX = Mathf.Clamp(newX, 0, _gridWidth - 1);
        newY = Mathf.Clamp(newY, 0, _gridHeight - 1);

        return _tiles[newX, newY];
    }

    public Tile GetRandomTile()
    {
        int x = Random.Range(0, _gridWidth);
        int y = Random.Range(0, _gridHeight);
        return _tiles[x, y];
    }


}
