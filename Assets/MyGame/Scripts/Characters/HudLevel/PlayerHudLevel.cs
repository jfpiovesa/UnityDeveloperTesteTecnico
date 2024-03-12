using TMPro;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerHudLevel : MonoBehaviour
{
    [SerializeField] private SO_LevellUp _levellUp;

    void Start()
    {
        Initialization();
    }
    private void OnDisable()
    {
        PlayerStatickStatus.LevelPlayer -= UpdatetextLevel;

    }
    void Initialization()
    {
        LookAtConstraint look = GetComponent<LookAtConstraint>();
        ConstraintSource constraintSource = new ConstraintSource()
        {
            sourceTransform = Camera.main.gameObject.transform,
            weight = 1f
        };
        look.AddSource(constraintSource);

        PlayerStatickStatus.LevelPlayer += UpdatetextLevel;
    }
    void UpdatetextLevel(int value)
    {
        TextMeshPro textMeshPro = GetComponentInChildren<TextMeshPro>();
        if (textMeshPro != null)
        {
            textMeshPro.text = _levellUp.levelsUp[value].level;
        }
    }
}
