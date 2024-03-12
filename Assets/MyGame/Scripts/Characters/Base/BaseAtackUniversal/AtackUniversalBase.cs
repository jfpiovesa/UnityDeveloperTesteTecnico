using UnityEngine;
public enum AttackIdPart
{
    hand,
    leg,
    bory,
    weapon
}
public enum ColisionHitSelect
{
    sphere,
    box,
    Capsule
}
public class AtackUniversalBase : MonoBehaviour
{

    public ColisionHitSelect colisionHitSelect = ColisionHitSelect.sphere;
    public Transform startPoint, endPoint;

    [Header("Box")]
    public Vector3 boxRadius = Vector3.one;
    [Header("Spehere")]
    public float radius = 1f;

    public AttackIdPart attackIdPart;

    public LayerMask colisionlayer;

    public S_DamageObject damageObject;

    public BaseCharacter character;

    public virtual void DetectCollision() { }
    public virtual void SetDagameObejct(S_DamageObject SetDagameObejct) { }
    public virtual void Hit(Collider hit) { }
    private void OnDrawGizmosSelected()
    {
        Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);


        switch (colisionHitSelect)
        {
            case ColisionHitSelect.sphere:
                Gizmos.color = transparentRed;
                Gizmos.DrawSphere(transform.position, radius);
                break;
            case ColisionHitSelect.Capsule:

                if (startPoint == null || endPoint == null) return;
                Gizmos.color = transparentGreen;

                Gizmos.color = transparentGreen;
                Gizmos.DrawWireSphere(startPoint.position, radius);
                Gizmos.DrawWireSphere(endPoint.position, radius);
                Gizmos.DrawLine(startPoint.position + Vector3.up * radius, endPoint.position + Vector3.up * radius);
                Gizmos.DrawLine(startPoint.position - Vector3.up * radius, endPoint.position - Vector3.up * radius);
                Gizmos.DrawWireSphere(startPoint.position, radius);
                Gizmos.DrawWireSphere(endPoint.position, radius);
                Gizmos.DrawLine(startPoint.position - Vector3.up * radius, startPoint.position + Vector3.up * radius);
                Gizmos.DrawLine(endPoint.position - Vector3.up * radius, endPoint.position + Vector3.up * radius);

                break;
            case ColisionHitSelect.box:
                Gizmos.color = transparentRed;
                Gizmos.DrawCube(transform.position, boxRadius);
                break;
        }

    }
}
