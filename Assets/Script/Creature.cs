using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Creature : MonoBehaviour
{
    public delegate void HPChange(string name, float amount);

    public static event HPChange OnHPChanged;

    public delegate void Death(string name);

    public static event Death OnDeath;

    public bool isAlive = true;

    [SerializeField]
    protected float maxHP = 100;

    [SerializeField]
    protected float currentHP;

    [SerializeField]
    protected float speed = 10;

    [SerializeField]
    protected bool isEnemy = true;

    [SerializeField]
    protected string creatureName = "";

    [SerializeField]
    protected float attackRange = 3;

    protected virtual void Start()
    {
        currentHP = maxHP;
    }

    public virtual void ChangeHP(float amount)
    {
        currentHP += amount;
        currentHP = Mathf.Clamp(currentHP, -1, maxHP);
        if (isEnemy)
            HPBar.instance.SetHP(currentHP, maxHP, creatureName);

        if (OnHPChanged != null)
            OnHPChanged.Invoke(creatureName, amount);
        if (currentHP <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        isAlive = false;
        if (OnDeath != null)
            OnDeath.Invoke(creatureName);
    }

    protected virtual bool IsInAttackRange()
    {
        return Vector3.Distance(this.transform.position, PlayerController.instance.transform.position) <= attackRange;
    }
}