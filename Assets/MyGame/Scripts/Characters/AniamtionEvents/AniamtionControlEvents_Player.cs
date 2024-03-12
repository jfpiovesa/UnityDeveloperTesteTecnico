using UnityEngine;

public class AniamtionControlEvents_Player : AniamtionControlEvents
{
    [SerializeField] private AtackUniversalBase _hand_right;

    public override void Initialized()
    {
        base.Initialized();

        _hand_right.gameObject.SetActive(false);
    }
    public void HandRightAttackOn()
    {
        _hand_right.gameObject.SetActive(true);
    }
    public void HandRightAttackOff()
    {
        _hand_right.gameObject.SetActive(false);
    }
    public void ActiveAttack()
    {
        _character.canAttack = true;
    }

    public override void Death()
    {
        base.Death();

        BaseHealth health = GetComponentInParent<BaseHealth>();
        if (health != null)
        {
            health.Death();
        }
    }


}
