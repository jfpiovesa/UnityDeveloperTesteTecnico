using System;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public Transform slotSatack;
    public Transform playerTransform;
    public int maxStackSize = 10;
    public float stackOffset = 0.5f;
    public float forceMultiplier = 10;
    public float maxLength = 10;

    [SerializeField] private List<Rigidbody> _rigidbodies = new List<Rigidbody>();
    public List<Rigidbody> GetRigidbodiesCounts { get { return _rigidbodies; } }
    private void FixedUpdate()
    {
        Vector3 playerVelocity = playerTransform.GetComponent<CharacterController>().velocity;

        if (playerVelocity.magnitude <= 0.1f)
        {
            AlignStack();
        }
        else
        {
            ApplyStack(playerVelocity);
        }
    }
    public void ApplyStack(Vector3 playerVelocity)
    {

        Vector3 forceDirection = -playerVelocity.normalized;

        for (int i = 0; i < _rigidbodies.Count; i++)
        {
            float forceMagnitude = forceMultiplier * (i + 1) * Time.fixedDeltaTime;
            Vector3 force = forceDirection * forceMagnitude;

            _rigidbodies[i].AddForce(force);
            _rigidbodies[i].angularVelocity = Vector3.ClampMagnitude(_rigidbodies[i].angularVelocity, maxLength);
        }
    }
    public void AlignStack()
    {
        for (int i = 0; i < _rigidbodies.Count; i++)
        {

            Vector3 targetPosition = new Vector3(0, stackOffset * (i + 1), 0);

            _rigidbodies[i].transform.localPosition = Vector3.Lerp(_rigidbodies[i].transform.localPosition, targetPosition, Time.deltaTime * 5);

        }
    }
    public void AddBodyToStack(Rigidbody body)
    {
        maxStackSize = GameManager.Instance.PlayerManager.PlayerStatus.maxStackSize;
        if (_rigidbodies.Count >= maxStackSize)
        {
            return;
        }

        body.GetComponent<EnemyControl>().isStackBack = true;
        body.GetComponentInChildren<Animator>().enabled = true;
        body.GetComponentInChildren<RagDoll>().RagdollOnOff(false);

        if (_rigidbodies.Count <= 0)
        {
            body.GetComponent<StackItem>().PreviousPlate(slotSatack.GetComponent<StackItem>(), slotSatack);
        }
        else
        {
            body.GetComponent<StackItem>().PreviousPlate(_rigidbodies[_rigidbodies.Count - 1].GetComponent<StackItem>(), slotSatack);
        }
        _rigidbodies.Add(body);
        body.constraints |= RigidbodyConstraints.FreezePositionY;
        body.useGravity = false;
        body.isKinematic = false;


        body.transform.parent = slotSatack;
        body.transform.localPosition = new Vector3(0, stackOffset * _rigidbodies.Count, 0);


        double resto = Math.IEEERemainder(_rigidbodies.Count, 2);

        if (resto == 0)
        {
            body.transform.localRotation = Quaternion.Euler(-90f, 0, 90f);
        }
        else
        {
            body.transform.localRotation = Quaternion.Euler(-90f, 0, -90f);
        }
        // body.GetComponentInChildren<Animator>().enabled = false;
    }
    public void RemoveBodyFromStack()
    {
        if (_rigidbodies.Count <= 0)
        {
            return;
        }
        Rigidbody bodyToRemove = _rigidbodies[_rigidbodies.Count - 1];
        EnemyControl enemy = bodyToRemove.GetComponent<EnemyControl>();
        if (enemy != null)
        {
            AddCoins(enemy);
        }
        _rigidbodies.RemoveAt(_rigidbodies.Count - 1);
        bodyToRemove.gameObject.SetActive(false);

        Transform parent = FindAnyObjectByType<ManagerEnemy>().transform;
        if (parent != null)
        {
            bodyToRemove.transform.parent = parent;
        }
        else
        {
            bodyToRemove.transform.parent = null;
        }
        enemy.ResetLocomotionPostion();
    }

    void AddCoins(EnemyControl enemy)
    {
        GameManager.Instance.PlayerManager.AddCoins(enemy.valueCoinColletorStatck);
    }
}
