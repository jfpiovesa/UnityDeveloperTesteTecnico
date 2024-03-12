using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class RagDoll : MonoBehaviour
{
    [Header("Componemts")]
    [SerializeField] private Transform _parentGetComponemts;
    [SerializeField] private CapsuleCollider[] _listCapsules;
    [SerializeField] private BoxCollider[] _listBox;
    [SerializeField] private SphereCollider[] _listSphere;
    [SerializeField] private Rigidbody[] _rb;
    private void Awake()
    {
        GetCOmponemtsRagdoll();
        RagdollOnOff(false);
    }
    public void GetCOmponemtsRagdoll()
    {
        _listCapsules = _parentGetComponemts.GetComponentsInChildren<CapsuleCollider>();
        _listBox = _parentGetComponemts.GetComponentsInChildren<BoxCollider>();
        _listSphere = _parentGetComponemts.GetComponentsInChildren<SphereCollider>();
        _rb = _parentGetComponemts.GetComponentsInChildren<Rigidbody>();
    }
    public void RagdollOnOff(bool value)
    {
        foreach(CapsuleCollider capsuleCollider  in  _listCapsules)
        {
            capsuleCollider.enabled = value;
        }
        foreach (BoxCollider boxCollider in _listBox)
        {
            boxCollider.enabled = value;
        }
        foreach (SphereCollider sphereCollider in _listSphere)
        {
            sphereCollider.enabled = value;
        }
        foreach (Rigidbody rb in _rb)
        {
            rb.isKinematic = !value;
        }
    }
}
