using System.Collections;
using UnityEngine;

public class Animal : MonoBehaviour
{
    private static WaitForSeconds _waitForSeconds5_0 = new(5.0f);
    public Tile CurrentTile;
    [SerializeField] protected int _lifeTimer;
    [SerializeField] protected int _movementRange;

    [SerializeField] private float _movementSpeed;

    private Vector3 targetPosition;

    protected GridManager _gridManager;

    protected virtual void Awake()
    {
        _gridManager = FindAnyObjectByType<GridManager>();

    }

    protected virtual void Start()
    {
        StartCoroutine(TakeTurnCoroutine());
    }

    public virtual void SetPosition(Tile tile)
    {
        tile.AddAnimal(this);
        CurrentTile = tile;
        transform.position = tile.transform.position;
        targetPosition = tile.transform.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            _movementSpeed * Time.deltaTime
        );

    }


    public virtual void TakeTurn()
    {
        _lifeTimer--;
        if (_lifeTimer <= 0)
        {
            Die();
            return;
        }
        Move();
    }

    IEnumerator TakeTurnCoroutine()
    {
        while (_lifeTimer > 0 && CurrentTile != null)
        {
            yield return _waitForSeconds5_0;
            TakeTurn();
        }
    }

    protected virtual void Move()
    {
        Tile newTile = _gridManager.GetRandomAdjacentTile(CurrentTile, _movementRange);
        if (newTile != null)
        {
            MoveToTile(newTile);
        }
    }

    protected void MoveToTile(Tile newTile)
    {
        if (CurrentTile != null)
        {
            CurrentTile.RemoveAnimal(this);
        }

        CurrentTile = newTile;
        newTile.AddAnimal(this);
        targetPosition = newTile.transform.position;
    }


    protected virtual void Die()
    {
        if (CurrentTile != null)
        {
            CurrentTile.RemoveAnimal(this);
        }
        gameObject.SetActive(false);
    }

}
