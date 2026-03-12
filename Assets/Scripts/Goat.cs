using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Goat : Animal
{
    public string Gender;
    private bool IsChild = false;

    public void Initialize()
    {
        string[] genders = { "male", "female" };
        int index = Random.Range(0, genders.Length);
        Gender = genders[index];
    }


    public void SetAsChild()
    {
        IsChild = true;
        transform.localScale *= 0.5f;
    }


    protected override void AfterMove()
    {
        base.AfterMove();
        EatGrass();
        Reproduce();
    }

    void EatGrass()
    {
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
        Goat goat = goats.Find(g => (g.Gender == "male") && !g.IsChild);
        if (goat) return true;
        return false;
    }

    void Reproduce()
    {
        if (CurrentTile != null && !IsChild)
        {
            bool isFemale = Gender == "female";
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
