using System;
using System.Collections;
using UnityEngine;

public class Animal : MonoBehaviour
{
    private static WaitForSeconds _waitForSeconds5_0 = new(5.0f);
    public Tile CurrentTile;
    public int LifeTimer = 10;
    public int movementRange = 1;
    public float movementSpeed = 0.5f;

    private Vector3 targetPosition;

    protected GridManager _gridManager;

    protected virtual void Awake()
    {
        _gridManager = FindAnyObjectByType<GridManager>();
    }

    protected virtual void Start()
    {
        Tile tile = _gridManager.GetRandomTile();
        tile.AddAnimal(this);
        CurrentTile = tile;
        transform.position = tile.transform.position;
        targetPosition = tile.transform.position;
        StartCoroutine(TakeTurnCoroutine());
    }

    void Update()
    {

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            movementSpeed * Time.deltaTime
        );

    }


    public virtual void TakeTurn()
    {
        LifeTimer--;
        if (LifeTimer <= 0)
        {
            Die();
            return;
        }
        Move();
    }

    IEnumerator TakeTurnCoroutine()
    {
        while (LifeTimer > 0)
        {
            yield return _waitForSeconds5_0;
            TakeTurn();
        }
    }

    protected virtual void Move()
    {
        Tile newTile = _gridManager.GetRandomAdjacentTile(CurrentTile, movementRange);
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
        Destroy(gameObject);
    }

}
