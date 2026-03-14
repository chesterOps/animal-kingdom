using System.Collections.Generic;
using System.Linq;

public class Lion : Animal
{
    protected override void AfterMove()
    {
        base.AfterMove();
        EatGoat();
    }

    void EatGoat()
    {
        if (CurrentTile != null)
        {
            Goat goat = FindGoat();
            if (goat != null)
            {
                goat.Die();
                ResetLifeSpan();
            }
        }
    }

    Goat FindGoat()
    {
        List<Animal> animals = CurrentTile.animalsOnTile;
        Goat goat = animals.OfType<Goat>().FirstOrDefault();
        return goat;
    }
}
