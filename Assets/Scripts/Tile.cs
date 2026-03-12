using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Tile : MonoBehaviour
{
    public int X;
    public int Y;

    private SpriteRenderer _spriteRenderer;
    private Sprite _grass;


    public List<Animal> animalsOnTile = new();

    public bool HasGrass { private set; get; } = true;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _grass = _spriteRenderer.sprite;
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
        _spriteRenderer.sprite = null;
        HasGrass = false;
    }
}
