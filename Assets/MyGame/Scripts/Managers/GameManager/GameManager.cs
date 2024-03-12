using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    [Header("Managers")]
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private HudInfoGame _hudInfoGame;

    public PlayerManager PlayerManager { get { return _playerManager; } set { _playerManager = value; } }


    [Header("Fps")]
    [SerializeField] private bool _isFps = false;
    [SerializeField] private float _fps;


    public float Fps { get { return _fps; } }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);


        Initialized();
    }
    private void Update()
    {
        if (_isFps)
        {
            _fps = 1.0f / Time.deltaTime;
            _hudInfoGame.UpdateTextFps( "FPS: " +_fps.ToString("00"));
        }
        else
        {
            _hudInfoGame.UpdateTextFps(string.Empty);
        }
    }
    private void Initialized()
    {
        Application.targetFrameRate = 60;
        PlayerManager.Initialized();
    }
}
