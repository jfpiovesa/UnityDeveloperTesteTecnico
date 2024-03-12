using Cinemachine;
using UnityEngine;

public class EnemyHealth : BaseHealth
{
    Animator _animator;
    RagDoll _ragDoll;
    EnemyControl _enemyControl;
    [SerializeField] private GameObject _vfxHit;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _ragDoll = GetComponentInChildren<RagDoll>();
        _enemyControl = GetComponentInChildren<EnemyControl>();
       
    }
    public override void Hit(S_DamageObject DO)
    {
        if (isDeath) return;
        S_DamageObject damageObject = (S_DamageObject)DO;
        if (damageObject != null)
        {
            Inpact(damageObject);
            isDeath = true;
            _animator.Play("Death");

        }
    }

    void Inpact(S_DamageObject DO)
    {
        S_DamageObject DOLocal = DO;
        if (DOLocal == null) return;

        Vector3 direction = (DOLocal.pointColision - transform.position).normalized;
        Vector3 vector3 = new Vector3(direction.x, 0, direction.z);
        VfxHit(DOLocal);
        GetComponent<Rigidbody>().AddForce(-vector3 * 100f * DO.power, ForceMode.Impulse);
        Shake();
    }
    void VfxHit(S_DamageObject DO)
    {
       Instantiate(_vfxHit, DO.pointColision, Quaternion.identity);
    }
    void Shake()
    {
        CinemachineImpulseSource impulseSource = GetComponent<CinemachineImpulseSource>();
        if (impulseSource != null)
        {
            impulseSource.GenerateImpulse();
        }

    }
    public override void Death()
    {
        Invoke("Ragdoll", 0.3f);
    }
    void Ragdoll()
    {
        _animator.enabled = false;
        _ragDoll.RagdollOnOff(true);
        Invoke("CasnStack", 1f);
    }
    void CasnStack()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<CapsuleCollider>().enabled = false;
        _enemyControl.isStackBack = false;
    }
}
