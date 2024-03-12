using UnityEngine;

public class AtackUniversalPlayer : AtackUniversalBase
{
  
    public override void SetDagameObejct(S_DamageObject SetDagameObejct)
    {
       
    }
 
    private void Update()
    {
        if (this.gameObject.activeInHierarchy )
        {
            DetectCollision();
        }
    }
    public override void DetectCollision()
    {
        Collider[] hitColliders = new Collider[0];
        switch (colisionHitSelect)
        {
            case ColisionHitSelect.sphere:
                hitColliders = Physics.OverlapSphere(transform.position, radius, colisionlayer);
                break;
            case ColisionHitSelect.Capsule:
                if (startPoint == null || endPoint == null) return;
                hitColliders = Physics.OverlapCapsule(startPoint.position, endPoint.position, radius, colisionlayer);

                break;

            case ColisionHitSelect.box:
                if (startPoint == null || endPoint == null) return;
                hitColliders = Physics.OverlapBox(startPoint.position, boxRadius,Quaternion.identity,colisionlayer);

                break;
        }
        if (hitColliders.Length > 0)
        {
            foreach (Collider hit in hitColliders)
            {

                IDamagable<S_DamageObject> damagableObject = hit.GetComponent(typeof(IDamagable<S_DamageObject>)) as IDamagable<S_DamageObject>;
                if (damagableObject != null && damagableObject !=
                    (character.gameObject.GetComponent(typeof(IDamagable<S_DamageObject>)) as IDamagable<S_DamageObject>))
                {

                    RaycastHit hitInfo;
                    if (Physics.Raycast(transform.position, hit.transform.position - transform.position, out hitInfo, Mathf.Infinity, colisionlayer))
                    {

                        Vector3 pontoDeContato = hitInfo.point;
                        damageObject.pointColision = pontoDeContato;
                        damagableObject.Hit(damageObject);
                    }

                    this.gameObject.SetActive(false);
                }

            }
        }
    }
}
