using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Item : MonoBehaviour
{
    [SerializeField]
    protected Vector3 pickUpRotation;

    protected Vector3 startingScale;
    protected Arm arm;

    public abstract void UseItem(Vector3 lookingDirection);

    public virtual void PickUp(GameObject hand, Arm arm)
    {
        this.arm = arm;
        this.transform.position = hand.transform.position;
        this.transform.rotation = Quaternion.Euler(hand.transform.rotation.eulerAngles + pickUpRotation);
        this.gameObject.layer = hand.layer;
    }

    public virtual void Drop()
    {
        arm = null;
        this.gameObject.layer = 0;
    }

    public virtual void DropThisItem()
    {
        arm.DropItem();
    }
}