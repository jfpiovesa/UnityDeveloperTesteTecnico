using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private S_PlayerStatus _playerStatus;
    [SerializeField] private SO_LevellUp _levellUp;
    public S_PlayerStatus PlayerStatus { get { return _playerStatus; } }

    public void Initialized()
    {
        _playerStatus.maxStackSize = _levellUp.levelsUp[_playerStatus.levelPlayer].maxStackSize;
    }

    public void AddLeve(int value)
    {
        int localValue = _playerStatus.levelPlayer + value;
        _playerStatus.levelPlayer = Mathf.Clamp( localValue,0,9);
        _playerStatus.maxStackSize = _levellUp.levelsUp[_playerStatus.levelPlayer].maxStackSize;
        PlayerStatickStatus.SetLevel(_playerStatus.levelPlayer);

    }
    public void AddCoins(int value)
    {
        int localValue = _playerStatus.coins + value;
        _playerStatus.coins = Mathf.Clamp(localValue, 0, 999999);
        PlayerStatickStatus.SetCoins(_playerStatus.coins);
    }
    public void RemoveCoins(int value)
    {
        int localValue = _playerStatus.coins - value;
        _playerStatus.coins = Mathf.Clamp(localValue, 0,999999);
        PlayerStatickStatus.SetCoins(_playerStatus.coins);
    }

}
