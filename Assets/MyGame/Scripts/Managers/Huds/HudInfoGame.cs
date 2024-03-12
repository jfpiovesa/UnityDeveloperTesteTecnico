using TMPro;
using UnityEngine;

public class HudInfoGame : MonoBehaviour
{

    [SerializeField] private TMP_Text _fspTetx;
    public void UpdateTextFps(string textValue)
    {
        _fspTetx.text = textValue;
    }
}
