using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : BaseHealth
{
    [Header("Max Health")]
    public float MaxHealth = 10;

    [SerializeField] private RagDoll _ragDoll;
    private void Awake()
    {
        Initialized();
    }
    private void Initialized()
    {
        if(_ragDoll == null)
        {
            _ragDoll = GetComponentInChildren<RagDoll>();
        }



        currentHealth = MaxHealth;

    }
    public override void Hit(S_DamageObject DO)
    {
        TakeDamege(DO.damage);
    }
    public override void TakeDamege(float value)
    {
        if (isDeath) return;
        float healthLocal = currentHealth - value;
        ModifyHealth(healthLocal);

        if (healthLocal <= 0)
        {
            if (!isDeath)
            {
                isDeath = true;
                _ragDoll.RagdollOnOff(true);
            }
        }
    }
    public override void ModifyHealth(float value)
    {
        currentHealth = Mathf.Clamp(value,0,MaxHealth);
    }
}
