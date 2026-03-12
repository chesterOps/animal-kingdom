
public class Goat : Animal
{
    protected override void Awake()
    {
        base.Awake();
        movementRange = 1;
    }

    public override void TakeTurn()
    {
        EatGrass();
        base.TakeTurn();
    }

    void EatGrass()
    {
        if (CurrentTile != null)
        {
            CurrentTile.RemoveGrass();
            LifeTimer += 2;
        }
    }


}
