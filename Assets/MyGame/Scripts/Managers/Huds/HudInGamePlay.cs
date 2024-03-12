using System.Collections;
using TMPro;
using UnityEngine;


public class HudInGamePlay : MonoBehaviour
{
    [SerializeField] private TMP_Text _coins;
    public float valueOld = 0;
    [Range(0,1)] public  float velocidadeAnimacao = 1;
    private void Start()
    {
        PlayerStatickStatus.CoinsPlayer += UpdateTextCoisn;
        PlayerStatickStatus.SetCoins(GameManager.Instance.PlayerManager.PlayerStatus.coins);
    }
    void UpdateTextCoisn(int value)
    {
        if (_coins != null)
        {
            StartCoroutine(AniamtionCoin(value));
        }
    }
    IEnumerator AniamtionCoin(int value)
    {
        while (valueOld != value)
        {
            // Incrementa ou decrementa o número atual
            if (valueOld < value)
            {
                valueOld++;
            }
            else
            {
                valueOld--;
            }

            // Atualiza o texto
            _coins.text = valueOld.ToString();

            // Espera um tempo antes de atualizar novamente
            yield return new WaitForSeconds(velocidadeAnimacao);
        }
        StopAllCoroutines();
    }
}
