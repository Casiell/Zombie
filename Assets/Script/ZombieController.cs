using UnityEngine;

public class ZombieController : Creature
{
    private bool move = false;
    private CharacterController controller;
    private float aggroDistance = 100;
    private float playerCheckInterval = 1;
    private float rotationSpeed = 10;
    private float timer = 0;
    private Animator animator = null;

    private void Awake()
    {
        controller = this.GetComponent<CharacterController>();
        animator = this.GetComponent<Animator>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= playerCheckInterval)
        {
            timer = 0;
            if (CheckDistanceFromPlayer() && CheckLineOfSight())
            {
                Debug.Log("I see you!");
                move = true;
            }
            else
            {
                move = false;
            }
        }
        if (IsInAttackRange())
        {
            AttackAnimation(true);
        }
        else if (move)
        {
            AttackAnimation(false);
            RotateTowardsPlayer();
            MoveTowardsPlayer();
        }
    }

    private void AttackAnimation(bool attack)
    {
        animator.SetBool("isInMeleeRange", attack);
    }

    private void MoveTowardsPlayer()
    {
        controller.SimpleMove(this.transform.TransformDirection(Vector3.forward * speed));
    }

    private void RotateTowardsPlayer()
    {
        Vector3 relativePosition = PlayerController.instance.transform.position - this.transform.position;
        Quaternion finalRotation = Quaternion.LookRotation(relativePosition);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, finalRotation, Time.deltaTime * rotationSpeed);
    }

    private bool CheckDistanceFromPlayer()
    {
        return Vector3.Distance(this.transform.position, PlayerController.instance.transform.position) <= aggroDistance;
    }

    private bool CheckLineOfSight()
    {
        RaycastHit hit = new RaycastHit();
        if (Physics.Linecast(this.transform.position + new Vector3(0, controller.height, 0), PlayerController.instance.transform.position, out hit))
        {
            return hit.transform == PlayerController.instance.transform;
        }
        return true;
    }

    protected override void Die()
    {
        base.Die();
        this.enabled = false;
    }
}