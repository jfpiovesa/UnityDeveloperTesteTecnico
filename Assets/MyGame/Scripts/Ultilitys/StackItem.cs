using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackItem : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private StackItem _previousStack;
    [SerializeField] private Vector3 _lastPlayerPosition;
    [SerializeField] private Transform _player;

    public float springForce = 5f; // For�a de "mola" entre os pratos
    public float playerForceMultiplier = 0.1f; // Multiplicador da for�a do jogador
    public float forceMultiplierPerPlate = 0.1f; // Multiplicador de for�a por prato


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
        // Aplica for�a para simular a conex�o entre os pratos
        if (_previousStack != null)
        {
            Vector3 direction = _previousStack.transform.position - transform.position;
            _rb.AddForce(direction.normalized * springForce);
        }

        // Calcula a for�a do jogador com base na velocidade m�dia
        Vector3 playerMovement = (transform.position - _lastPlayerPosition) / Time.deltaTime;
        Vector3 playerForce = playerMovement * playerForceMultiplier;

        // Calcula o multiplicador de for�a com base na posi��o do prato na pilha
        float distanceToTop = Vector3.Distance(transform.position, transform.parent.position);
        float distanceToBottom = Vector3.Distance(transform.position, _previousStack.transform.position);
        float forceMultiplier = Mathf.Lerp(1f, forceMultiplierPerPlate, distanceToTop / distanceToBottom);

        // Aplica a for�a ao prato
        _rb.AddForce(-playerForce * forceMultiplier);

        // Atualiza a posi��o do jogador
        _lastPlayerPosition = _player.transform.position;
    }
}
