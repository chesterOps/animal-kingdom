
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Goat : Animal
{

    public enum Gender
    {
        Male,
        Female, Count
    }

    public Gender Sex;

    public void Initialize(int gender)
    {
        Sex = (Gender)gender;
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
        Goat goat = goats.Find(g => g.Sex == Gender.Male);
        if (goat) return true;
        return false;
    }



    void Reproduce()
    {
        if (CurrentTile != null)
        {
            bool isFemale = Sex == Gender.Female;
            bool hasMaleGoat = FindMaleGoat(CurrentTile);
            if (hasMaleGoat && isFemale)
            {
                for (int i = 0; i < 2; i++)
                {
                    GameObject goatObject = ObjectPool.Instance.GetGoat();
                    Goat childGoat = goatObject.GetComponent<Goat>();
                    childGoat.SetPosition(CurrentTile);
                }
            }
        }
    }
}
