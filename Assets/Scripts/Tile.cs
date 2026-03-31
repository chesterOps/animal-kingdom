using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Tile : MonoBehaviour
{
    public int X;
    public int Y;

    private SpriteRenderer _spriteRenderer;

    [SerializeField] private Sprite _grass;
    [SerializeField] private Sprite _dirt;


    public List<Animal> animalsOnTile = new();

    public bool HasGrass { private set; get; } = true;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _grass;
    }

    public void Initialize(int x, int y)
    {
        X = x;
        Y = y;
    }

    public void AddAnimal(Animal animal)
    {
        if (!animalsOnTile.Contains(animal))
        {
            animalsOnTile.Add(animal);
        }
    }

    public void RemoveAnimal(Animal animal)
    {
        if (animalsOnTile.Contains(animal))
        {
            animalsOnTile.Remove(animal);
        }
    }

    public void ReplenishGrass()
    {
        HasGrass = true;
        _spriteRenderer.sprite = _grass;
    }

    public void RemoveGrass()
    {
        _spriteRenderer.sprite = _dirt;
        HasGrass = false;
    }
}
