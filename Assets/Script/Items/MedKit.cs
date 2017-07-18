using UnityEngine;

public class MedKit : Item
{
    [SerializeField]
    private int numberOfCharges = 5;

    [SerializeField]
    private float healing = 20;

    public override void UseItem(Vector3 lookingDirection)
    {
        if (numberOfCharges > 0)
        {
            PlayerController.instance.ChangeHP(healing);
            numberOfCharges--;
        }
        if (numberOfCharges <= 0)
        {
            RunOutOfCharges();
        }
    }

    private void RunOutOfCharges()
    {
        DropThisItem();
        Destroy(this.gameObject);
    }
}