using System.Collections;
using TMPro;
using UnityEngine;

public class LevelUpPlayerStack : MonoBehaviour
{
    [SerializeField] private TextMeshPro m_TextMeshPro;
    [SerializeField] private SO_LevellUp _levellUp;
    [SerializeField] private GameObject _vfxLevellUp;
    [SerializeField] private float _timeDalay = 0.5f;

    bool _isLevelUpAction = false;

    private void Start()
    {
        _vfxLevellUp.GetComponent<ParticleSystem>().Stop();
        m_TextMeshPro.text = TextColor(_levellUp.levelsUp[CheckLevel(1)].requeredMoney.ToString()) + "\n UP";
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerControl playerControl = other.GetComponent<PlayerControl>();
        if (!playerControl) return;
        StartCoroutine(CheckLevel(playerControl));
    }
    private void OnTriggerExit(Collider other)
    {
        PlayerControl playerControl = other.GetComponent<PlayerControl>();
        if (!playerControl) return;
        ExitLevelUp();
    }

    IEnumerator CheckLevel(PlayerControl playerControl)
    {
        _isLevelUpAction = false;

        while (!_isLevelUpAction)
        {
            if (GameManager.Instance.PlayerManager.PlayerStatus.levelPlayer == 9)
            {
                m_TextMeshPro.text = " ---- ";
                _isLevelUpAction = true;
            }
            else
            {
                if (GameManager.Instance.PlayerManager.PlayerStatus.coins >= _levellUp.levelsUp[CheckLevel(1)].requeredMoney)
                {

                    GameManager.Instance.PlayerManager.RemoveCoins(_levellUp.levelsUp[CheckLevel(1)].requeredMoney);
                    GameManager.Instance.PlayerManager.AddLeve(1);
                    VfxLevelUpPlay();
                    playerControl.SetColorMaterialPlayr(GetColorLevel(CheckLevel(0)));
                    m_TextMeshPro.text = TextColor(_levellUp.levelsUp[CheckLevel(1)].requeredMoney.ToString()) + "\n UP";
                }
                else
                {
                    _isLevelUpAction = true;
                }
            }
            yield return new WaitForSecondsRealtime(_timeDalay);
        }
        StopAllCoroutines();
    }

    void VfxLevelUpPlay()
    {
        ParticleSystem vfxLocal = _vfxLevellUp.GetComponent<ParticleSystem>();
        if (vfxLocal != null)
        { 
            vfxLocal.Play();
        }
    }
    void ExitLevelUp()
    {
        _isLevelUpAction = true;
        StopAllCoroutines();
    }
    #region ColorText
    string TextColor(string text)
    {
        string textLocal = text;
        Color colorLevel = GetColorLevel(CheckLevel(1));
        string hexColor = RGBtoHex(colorLevel);
        return "<color=" + hexColor + ">" + textLocal + "</color>";

    }
    public static string RGBtoHex(Color color)
    {
        string red = Mathf.FloorToInt(color.r * 255).ToString("X2");
        string green = Mathf.FloorToInt(color.g * 255).ToString("X2");
        string blue = Mathf.FloorToInt(color.b * 255).ToString("X2");
        return "#" + red + green + blue;
    }
    #endregion
    int CheckLevel(int valueAdd)
    {
        int valueLocal = Mathf.Clamp(GameManager.Instance.PlayerManager.PlayerStatus.levelPlayer + valueAdd, 0, 9);
        return valueLocal;
    }
    Color GetColorLevel(int level)
    {

        if (level <= _levellUp.levelsUp.Length)
        {
            return _levellUp.levelsUp[level].Color;
        }
        else
        {
            return Color.white;
        }
    }

}
