using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;
    private List<GameObject> _goats = new();
    private GridManager _gridManager;


    [SerializeField] private int _goatSpawnCount;
    [SerializeField] private GameObject _goatPrefab;

    void Awake()
    {
        _gridManager = FindAnyObjectByType<GridManager>();
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        for (int i = 0; i < _goatSpawnCount; i++)
        {
            Tile randomTile = _gridManager.GetRandomTile();
            GameObject goatObject = CreateGoat();
            Goat goat = goatObject.GetComponent<Goat>();
            goat.Initialize();
            goat.SetPosition(randomTile);
        }
    }


    GameObject CreateGoat()
    {
        GameObject goatObject = Instantiate(_goatPrefab);
        goatObject.transform.parent = gameObject.transform;
        _goats.Add(goatObject);
        return goatObject;
    }

    public GameObject GetGoat()
    {
        for (int i = 0; i < _goats.Count; i++)
        {
            if (!_goats[i].activeInHierarchy)
            {
                return _goats[i];
            }
        }

        GameObject newGoat = CreateGoat();
        return newGoat;
    }
}
