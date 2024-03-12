using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackItem : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private StackItem _previousStack;
    [SerializeField] private Vector3 _lastPlayerPosition;
    [SerializeField] private Transform _player;

    public float springForce = 5f; // Força de "mola" entre os pratos
    public float playerForceMultiplier = 0.1f; // Multiplicador da força do jogador
    public float forceMultiplierPerPlate = 0.1f; // Multiplicador de força por prato


    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void PreviousPlate(StackItem stackItem, Transform player)
    {
        _previousStack = stackItem;
        _player = player;
        _lastPlayerPosition = _player.transform.position;
    }
    public void RestStack()
    {
        _previousStack = null;
        _player = null;
        _lastPlayerPosition = Vector3.zero;
    }
    void Update()
    {
        if (_previousStack == null) return;
        // Aplica força para simular a conexão entre os pratos
        if (_previousStack != null)
        {
            Vector3 direction = _previousStack.transform.position - transform.position;
            _rb.AddForce(direction.normalized * springForce);
        }

        // Calcula a força do jogador com base na velocidade média
        Vector3 playerMovement = (transform.position - _lastPlayerPosition) / Time.deltaTime;
        Vector3 playerForce = playerMovement * playerForceMultiplier;

        // Calcula o multiplicador de força com base na posição do prato na pilha
        float distanceToTop = Vector3.Distance(transform.position, transform.parent.position);
        float distanceToBottom = Vector3.Distance(transform.position, _previousStack.transform.position);
        float forceMultiplier = Mathf.Lerp(1f, forceMultiplierPerPlate, distanceToTop / distanceToBottom);

        // Aplica a força ao prato
        _rb.AddForce(-playerForce * forceMultiplier);

        // Atualiza a posição do jogador
        _lastPlayerPosition = _player.transform.position;
    }
}
