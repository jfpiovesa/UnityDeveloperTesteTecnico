public class AniamtionControlEvents_Enemy: AniamtionControlEvents
{
  
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
