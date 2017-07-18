using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Bullet : MonoBehaviour
{
    private float damage;
    private Vector3 startingPosition;
    private float range;

    private void Start()
    {
        startingPosition = this.transform.position;
    }

    public void Shoot(float force, float damage, Vector3 direction)
    {
        this.damage = damage;
        this.range = force;
        this.GetComponent<Rigidbody>().AddForce(direction * force, ForceMode.Impulse);
    }

    private void Update()
    {
        if (Vector3.Distance(startingPosition, this.transform.position) >= range)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Creature creature = collision.gameObject.GetComponent<Creature>();
        if (creature != null)
        {
            creature.ChangeHP(-damage);
        }
        Destroy(this.gameObject);
    }
}