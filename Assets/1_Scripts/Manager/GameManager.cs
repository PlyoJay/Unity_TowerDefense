using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private WaypointsManager waypointsManager = null;
    [SerializeField] private SpawnManager spawnManager = null;

    public void SetInit()
    {
        Instance = this;

        waypointsManager.SetInit();
        spawnManager.SetInit();

        this.Activate_Func();
    }

    public void Activate_Func()
    {
        WaypointsManager.Instance.Activate();
        SpawnManager.Instance.Activate();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        SetInit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
