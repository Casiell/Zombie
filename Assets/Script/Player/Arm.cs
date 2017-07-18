using UnityEngine;

public enum ArmState
{
    Forward,
    Down,
    Swinging
}

public class Arm : MonoBehaviour
{
    public ArmState state
    {
        get;
        private set;
    }

    [SerializeField]
    private FixedJoint hand = null;

    private Item item = null;

    [SerializeField]
    private Transform joint = null;

    private float swingTime = 1;
    private float swingSpeed;
    private bool swingingUp = true;
    private float maxSwingAngle = 45;
    private float minSwingAngle = 90 - 15;
    private Vector3 swingRotation = Vector3.zero;

    public bool isHoldingSomething
    {
        get
        {
            return item != null;
        }
    }

    private void Start()
    {
        if (item == null)
            RotateArm(ArmState.Down);
    }

    public void UseItem(Vector3 lookingDirection)
    {
        if (isHoldingSomething)
            item.UseItem(lookingDirection);
    }

    public void PickUpItem(Item pickedUpItem)
    {
        if (hand == null)
        {
            Debug.LogError("No hand, can't pick up item!");
            return;
        }

        item = pickedUpItem;
        RotateArm(ArmState.Forward);
        item.PickUp(hand.gameObject, this);
        hand.connectedBody = item.GetComponent<Rigidbody>();
    }

    public void DropItem()
    {
        if (isHoldingSomething)
        {
            RotateArm(ArmState.Down);
            item.Drop();
            item = null;
            hand.connectedBody = null;
        }
    }

    private void Update()
    {
        if (state == ArmState.Swinging)
        {
            if (swingingUp)
            {
                swingRotation.x -= Time.deltaTime * swingSpeed;
                joint.localRotation = Quaternion.Euler(swingRotation);
                if (swingRotation.x <= -maxSwingAngle)
                {
                    swingingUp = false;
                }
            }
            else
            {
                swingRotation.x += Time.deltaTime * swingSpeed;
                joint.localRotation = Quaternion.Euler(swingRotation);
                if (swingRotation.x >= minSwingAngle)
                {
                    RotateArm(ArmState.Forward);
                }
            }
        }
    }

    public void Swing()
    {
        if (state == ArmState.Swinging)
            return;
        if (state != ArmState.Forward)
        {
            RotateArm(ArmState.Forward);
        }
        state = ArmState.Swinging;
        swingingUp = true;
        swingRotation = Vector3.zero;
        swingSpeed = (minSwingAngle + minSwingAngle) / swingTime;
    }

    public void RotateArm(ArmState finalState)
    {
        if (joint == null)
        {
            Debug.LogError("No joints, can't rotate arm!");
            return;
        }
        state = finalState;
        switch (finalState)
        {
            case ArmState.Down:
                joint.localRotation = Quaternion.Euler(90, 0, 0);
                break;

            case ArmState.Forward:
                joint.localRotation = Quaternion.Euler(0, 0, 0);
                break;
        }
    }
}