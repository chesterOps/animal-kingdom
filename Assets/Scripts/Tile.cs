using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int X;
    public int Y;


    public List<Animal> animalsOnTile = new();

    public bool HasGrass { private set; get; } = true;

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
    }

    public void RemoveGrass()
    {
        HasGrass = false;
    }
}
