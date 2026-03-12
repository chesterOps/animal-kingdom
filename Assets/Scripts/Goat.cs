using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Goat : Animal
{
    public string Gender;
    public bool IsAdult;
    [SerializeField] private GameObject _goatprefab;
    protected override void Awake()
    {
        base.Awake();
        movementRange = 1;
    }

    public void Initialize(bool isAdult = true)
    {
        string[] genders = { "male", "female" };
        int index = Random.Range(0, genders.Length);
        Gender = genders[index];
        IsAdult = isAdult;
    }

    public override void TakeTurn()
    {
        EatGrass();
        Reproduce();
        base.TakeTurn();
    }

    void EatGrass()
    {
        if (CurrentTile != null && CurrentTile.HasGrass)
        {
            CurrentTile.RemoveGrass();
            LifeTimer += 2;
        }
    }

    bool FindOppositeGender(Tile tile)
    {
        List<Animal> animals = tile.animalsOnTile;
        List<Goat> goats = animals.OfType<Goat>().ToList();
        Goat goat = goats.Find(g => (g.Gender != Gender) && g.IsAdult);
        if (goat) return true;
        return false;
    }

    void Reproduce()
    {
        if (CurrentTile != null && IsAdult)
        {
            bool hasOppositeGender = FindOppositeGender(CurrentTile);
            if (hasOppositeGender)
            {
                GameObject goatObject = Instantiate(_goatprefab, CurrentTile.transform.position, Quaternion.identity);
                Goat childGoat = goatObject.GetComponent<Goat>();
                childGoat.Initialize(false);
            }
        }
    }

}
