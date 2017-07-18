using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : Creature
{
    public static PlayerController instance;
    private CharacterController player;
    private MouseLook look;

    [SerializeField]
    private GameObject head = null;

    public Arm leftArm;
    public Arm rightArm;
    private float pickUpRange = 10;

    public delegate void ItemPickUp(Arm arm, Item item);

    public static event ItemPickUp OnPickUpItem;

    private void Awake()
    {
        instance = this;
    }

    protected override void Start()
    {
        base.Start();
        player = this.GetComponent<CharacterController>();
        look = new MouseLook();
        look.Init(this.transform, Camera.main.transform);
    }

    private void FixedUpdate()
    {
        look.LookRotation(this.transform, Camera.main.transform);
        UseArm();
        Move();
    }

    private void UseArm()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (rightArm.isHoldingSomething)
                rightArm.UseItem(head.transform.forward);
            else
                PickUpItem(rightArm);
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (leftArm.isHoldingSomething)
                leftArm.UseItem(head.transform.forward);
            else
                PickUpItem(leftArm);
        }
        if (Input.GetMouseButtonDown(2))
        {
            rightArm.DropItem();
            leftArm.DropItem();
        }
    }

    private void PickUpItem(Arm arm)
    {
        var raycastHits = Physics.RaycastAll(head.transform.position, head.transform.forward, pickUpRange);
        //Debug.DrawRay(head.transform.position, head.transform.forward * pickUpRange, Color.red, 5);
        foreach (RaycastHit hit in raycastHits)
        {
            Item item = hit.collider.GetComponent<Item>();
            if (item != null)
            {
                arm.PickUpItem(item);
                if (OnPickUpItem != null)
                    OnPickUpItem.Invoke(arm, item);
                break;
            }
        }
    }

    private void Move()
    {
        player.SimpleMove(transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * speed));
    }
}