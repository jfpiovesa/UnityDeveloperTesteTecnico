using UnityEngine;

public class AniamtionControlEvents : MonoBehaviour
{

    protected  BaseCharacter _character;

    private void Awake()
    {
        Initialized();
    }
    public virtual void Initialized()
    {
        _character = GetComponentInParent<BaseCharacter>();

    }
    public virtual void Death() { }
}
