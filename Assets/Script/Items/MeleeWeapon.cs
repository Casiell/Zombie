using UnityEngine;

[RequireComponent(typeof(Collider))]
public class MeleeWeapon : Item
{
    [SerializeField]
    private float damage = 30;

    [SerializeField]
    private float durability = 10;

    public override void PickUp(GameObject hand, Arm arm)
    {
        base.PickUp(hand, arm);
        this.GetComponent<Collider>().isTrigger = true;
    }

    public override void Drop()
    {
        base.Drop();
        this.GetComponent<Collider>().isTrigger = false;
    }

    public override void UseItem(Vector3 lookingDirection)
    {
        arm.Swing();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (arm.state != ArmState.Swinging)
            return;
        Creature enemy = collider.gameObject.GetComponent<Creature>();
        if (enemy != null)
            Hit(enemy);
    }

    private void Hit(Creature enemy)
    {
        if (durability > 0)
        {
            enemy.ChangeHP(-damage);
            durability--;
        }
        if (durability <= 0)
        {
            DropThisItem();
            Destroy(this.gameObject);
        }
    }
}