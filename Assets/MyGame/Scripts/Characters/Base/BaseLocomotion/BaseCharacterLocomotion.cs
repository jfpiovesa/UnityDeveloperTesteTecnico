using UnityEngine;

public abstract class BaseCharacterLocomotion : MonoBehaviour
{
    public float SpeedWalk { get; set; } = 5;
    public bool canMoveCharacter { get; set; } = true;
    public virtual void Locomotion( Vector2 vector2) { }
    public virtual void Atack() { }
 
}
