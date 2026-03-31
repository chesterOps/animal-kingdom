using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Goat : Animal
{

    private bool IsChild = false;

    public enum Gender
    {
        Male,
        Female, Count
    }

    public Gender Sex;

    public void Initialize()
    {
        int index = Random.Range(0, (int)Gender.Count);
        Sex = (Gender)index;
    }


    public void SetAsChild()
    {
        IsChild = true;
        transform.localScale *= 0.8f;
    }


    protected override void AfterMove()
    {
        base.AfterMove();
        Eat();
        Reproduce();

    }

    protected override void Eat()
    {
        base.Eat();
        if (CurrentTile != null && CurrentTile.HasGrass)
        {
            CurrentTile.RemoveGrass();
            ResetLifeSpan();
        }
    }


    bool FindMaleGoat(Tile tile)
    {
        List<Animal> animals = tile.animalsOnTile;
        List<Goat> goats = animals.OfType<Goat>().ToList();
        Goat goat = goats.Find(g => (g.Sex == Gender.Male) && !g.IsChild);
        if (goat) return true;
        return false;
    }

    void Reproduce()
    {
        if (CurrentTile != null && !IsChild)
        {
            bool isFemale = Sex == Gender.Female;
            bool hasMaleGoat = FindMaleGoat(CurrentTile);
            if (hasMaleGoat && isFemale)
            {
                GameObject goatObject = ObjectPool.Instance.GetGoat();
                Goat childGoat = goatObject.GetComponent<Goat>();
                childGoat.SetAsChild();
                childGoat.SetPosition(CurrentTile);
            }
        }
    }

}
