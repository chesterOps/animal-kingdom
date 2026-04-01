using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;
    private List<GameObject> _goats = new();
    private List<GameObject> _lions = new();
    private GridManager _gridManager;


    [SerializeField] private int _goatSpawnCount;
    [SerializeField] private int _lionSpawnCount;
    [SerializeField] private GameObject _goatPrefab;

    [SerializeField] private GameObject _lionPrefab;
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
            bool isMale = i % 2 == 0;
            Tile randomTile = _gridManager.GetRandomTile();
            GameObject goatObject = CreateAnimal(_goatPrefab, _goats);
            Goat goat = goatObject.GetComponent<Goat>();
            goat.Initialize(isMale ? 0 : 1);
            goat.SetPosition(randomTile);
        }

        for (int i = 0; i < _lionSpawnCount; i++)
        {
            Tile randomTile = _gridManager.GetRandomTile();
            GameObject lionObject = CreateAnimal(_lionPrefab, _lions);
            Lion goat = lionObject.GetComponent<Lion>();
            goat.SetPosition(randomTile);
        }
    }


    GameObject CreateAnimal(GameObject prefab, List<GameObject> container)
    {
        GameObject animalObject = Instantiate(prefab);
        animalObject.transform.parent = gameObject.transform;
        container.Add(animalObject);
        return animalObject;
    }

    public GameObject GetAnimal(GameObject prefab, List<GameObject> container)
    {
        for (int i = 0; i < container.Count; i++)
        {
            if (!container[i].activeInHierarchy)
            {
                return container[i];
            }
        }

        GameObject newAnimal = CreateAnimal(prefab, container);
        return newAnimal;
    }

    public GameObject GetGoat()
    {
        return GetAnimal(_goatPrefab, _goats);
    }


}
