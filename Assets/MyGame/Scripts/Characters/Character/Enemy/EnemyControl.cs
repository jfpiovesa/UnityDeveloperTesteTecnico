using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class EnemyControl : BaseCharacter
{

    [Header("Componemts")]
    [SerializeField] private EnemyHealth health;


    [Header("Vars"), Space(5)]
    public bool isStackBack = true;
    public int valueCoinColletorStatck = 5;

    [SerializeField] private Vector3 _initialposition;
    private quaternion _initialrotation;
    private Animator _animator;
    private Rigidbody _rb;
    private CapsuleCollider _capsuleCollider;
    private StackItem _stackItem;
    private void Awake()
    {
        Initialized();
    }
    private void Initialized()
    {
        _stackItem = GetComponent<StackItem>();
        health = GetComponent<EnemyHealth>();
        _animator = GetComponentInChildren<Animator>();
        _rb = GetComponent<Rigidbody>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _initialposition = transform.localPosition;
        _initialrotation = transform.rotation;
    }

    public void ResetLocomotionPostion()
    {
        _stackItem.RestStack();
        _rb.constraints &= ~RigidbodyConstraints.FreezePositionY;
        transform.localPosition = _initialposition;
        transform.rotation = _initialrotation;
        _capsuleCollider.enabled = true;
        health.isDeath = false;
        isStackBack = true;
        _rb.useGravity = true;
        this.gameObject.SetActive(true);
        //  StartCoroutine(LocomotionPlay());
    }
    IEnumerator LocomotionPlay()
    {
        while (_initialposition != transform.position)
        {
            _animator.SetFloat("Speed", _rb.velocity.magnitude);
            yield return null;
        }
        StopAllCoroutines();
    }
}
