using UnityEngine;

public class TrainingDummy : Creature
{
    protected override void Start()
    {
        maxHP = 1000;
        base.Start();
    }

    protected override void Die()
    {
        currentHP = maxHP;
    }
}