using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseHealth : MonoBehaviour, IDamagable<S_DamageObject>
{
    public float currentHealth { get; set; }
    public bool isDeath { get; set; }

    public virtual void Hit(S_DamageObject DO) { }
    public virtual void AddHealth(float value) { }
    public virtual void TakeDamege(float value) { }
    public virtual void ModifyHealth(float value){ }
    public virtual void Death() { }
}

