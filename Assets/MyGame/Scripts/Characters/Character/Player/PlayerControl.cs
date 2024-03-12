using PlayerActions;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : BaseCharacter
{
    [Header("script componemts"), Space(5)]
    [SerializeField] PlayerHealth _playerHealth;
    [SerializeField] PlayerLocomotion _playerLocomotion;
    [SerializeField] Inventory _inventory;

    [Header("PlayerInput"), Space(5)]
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private SystemActions inputActions;

    [Header("Hud Level"), Space(5)]
    [SerializeField] private GameObject _hudLevel;

    [Header("Actions"), Space(5)]
    [SerializeField] private InputActionReference playerMovimentAction;


    [Header("material color"), Space(5)]
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshColorMaterial;
    float timeColorLocal = -1;
    private void Awake()
    {
        Initialized();
    }

    void Update()
    {
        if (_playerHealth.isDeath) return;
        if (!_playerLocomotion.canMoveCharacter) return;


        _playerLocomotion.Locomotion(inputActions.Player.Move.ReadValue<Vector2>());

        if (inputActions.Player.Fire.WasPressedThisFrame() && canAttack)
        {
            _playerLocomotion.Atack();
            canAttack = false;
        }
    }


    private void Initialized()
    {

        _playerLocomotion = GetComponent<PlayerLocomotion>();
        _playerHealth = GetComponent<PlayerHealth>();
        _inventory = GetComponent<Inventory>();
        _playerInput = GetComponent<PlayerInput>();
        inputActions = new SystemActions();
        inputActions.Enable();
        _playerInput.enabled = true;
    }
    public void SetColorMaterialPlayr(Color color)
    {
        StartCoroutine(SetColor(color));
    }
    IEnumerator SetColor(Color color)
    {
        _skinnedMeshColorMaterial.material.SetColor("_Color2", color);
        timeColorLocal = -1;
        while (timeColorLocal <= 1.2)
        {
            timeColorLocal = Mathf.Lerp(timeColorLocal, 1.5f, 2f * Time.deltaTime);
            _skinnedMeshColorMaterial.material.SetFloat("_time", timeColorLocal);

            yield return null;
        }
        _skinnedMeshColorMaterial.material.SetColor("_Color1", color);
        _skinnedMeshColorMaterial.material.SetFloat("_time", -1f);
        print("tets");
        StopCoroutine(SetColor(color));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 7) return;

        EnemyControl enemy = other.GetComponentInParent<EnemyControl>();
        if (enemy != null && !enemy.isStackBack && enemy.GetComponentInParent<EnemyHealth>().isDeath)
        {
            _inventory.AddBodyToStack(enemy.GetComponent<Rigidbody>());
        }
    }

}
