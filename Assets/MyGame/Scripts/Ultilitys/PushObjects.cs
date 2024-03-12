using UnityEngine;

public class PushObjects : MonoBehaviour
{
    public float pushForce = 10.0f;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rigidbody = hit.collider.gameObject.GetComponent<Rigidbody>();

        if (rigidbody != null)
        {
            Vector3 pushDirection = (transform.position - hit.transform.position).normalized;
            rigidbody.AddForce(pushDirection * pushForce, ForceMode.Impulse);
        }
    }
}
