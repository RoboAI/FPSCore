using UnityEngine;

public class SceneGlobals : MonoBehaviour
{
    public static SceneGlobals Instance { get; private set; }
    [SerializeField] public Player _player;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        //DontDestroyOnLoad(gameObject);//doesn't destroy when loading new scene (this one or another one)
    }

    private void Start()
    {
        if (_player == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("MainActor");
            if (player && player.TryGetComponent(out Player _playerComponent)) {
                    _player = _playerComponent;
            }
            else
            {
                Debug.LogError("Player not set in SceneGlobals");
            }
        }
    }
}
