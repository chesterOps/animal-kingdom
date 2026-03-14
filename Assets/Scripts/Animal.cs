using System.Collections;
using UnityEngine;

public class Animal : MonoBehaviour
{
    private static WaitForSeconds _waitForSeconds3_0 = new(3.0f);
    private static WaitForSeconds _waitForSeconds5_0 = new(5.0f);
    public Tile CurrentTile;
    [SerializeField] protected int _currentLifeSpan;
    [SerializeField] protected int _movementRange;

    [SerializeField] private float _movementSpeed;

    private Vector3 targetPosition;
    protected int _lifeSpan;
    protected GridManager _gridManager;

    protected virtual void Awake()
    {
        _gridManager = FindAnyObjectByType<GridManager>();
        _lifeSpan = _currentLifeSpan;
    }

    protected void ResetLifeSpan()
    {
        _currentLifeSpan = _lifeSpan;
    }

    protected virtual void Start()
    {
        StartCoroutine(DecreaseLifeSpan());
        StartCoroutine(MoveCoroutine());
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


    IEnumerator DecreaseLifeSpan()
    {
        while (_currentLifeSpan > 0)
        {
            yield return _waitForSeconds3_0;
            _currentLifeSpan--;
            if (_currentLifeSpan == 0)
            {
                Die();
            }
        }
    }




    IEnumerator MoveCoroutine()
    {
        while (_currentLifeSpan > 0 && CurrentTile != null)
        {
            yield return _waitForSeconds3_0;
            Move();
            yield return _waitForSeconds3_0;
            AfterMove();
        }
    }

    protected virtual void AfterMove()
    {

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


    public virtual void Die()
    {
        if (CurrentTile != null)
        {
            CurrentTile.RemoveAnimal(this);
        }
        gameObject.SetActive(false);
    }

}
