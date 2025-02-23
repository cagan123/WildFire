using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, ISaveManager
{
    public static GameManager instance;

    private Transform player;

    [SerializeField] private CheckPoint[] checkpoints;
    [SerializeField] private string closestCheckpointID;

    [Header("Lost currency")]
    [SerializeField] private GameObject lostCurrencyPrefab;
    public int lostCurrencyAmount;
    [SerializeField] private float lostCurrencyX;
    [SerializeField] private float lostCurrencyY;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        checkpoints = FindObjectsOfType<CheckPoint>();

        player = PlayerManager.instance.player.transform;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
            RestartScene();
    }
    public void RestartScene()
    {   
        if (SaveManager.instance != null)
        {
            SaveManager.instance.SaveGame();
        }
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        StartCoroutine(ReloadData());
    }

    private IEnumerator ReloadData()
    {
        yield return new WaitForSeconds(0.1f);
        if (SaveManager.instance != null)
        {
            SaveManager.instance.LoadGame();
        }
    }

    public void LoadData(GameData _data) => StartCoroutine(LoadWithDelay(_data));

    private void LoadCheckpoints(GameData _data)
    {
        foreach (KeyValuePair<string, bool> pair in _data.checkpoints)
        {
            foreach (CheckPoint checkpoint in checkpoints)
            {
                if (checkpoint.checkPointID == pair.Key && pair.Value == true)
                    checkpoint.ActivateCheckpoint();
            }
        }
    }

    private void LoadLostCurrency(GameData _data)
    {
        lostCurrencyAmount = _data.lostCurrencyAmount;
        lostCurrencyX = _data.lostCurrencyX;
        lostCurrencyY = _data.lostCurrencyY;

        if (lostCurrencyAmount > 0)
        {
            GameObject newLostCurrency = Instantiate(lostCurrencyPrefab, new Vector3(lostCurrencyX, lostCurrencyY), Quaternion.identity);
            //newLostCurrency.GetComponent<LostCurrencyController>().currency = lostCurrencyAmount;
        }

        lostCurrencyAmount = 0;
    }

    private IEnumerator LoadWithDelay(GameData _data)
    {
        yield return new WaitForSeconds(.1f);

        LoadCheckpoints(_data);
        LoadClosestCheckpoint(_data);
        LoadLostCurrency(_data);
    }

    public void SaveData(ref GameData _data)
    {
    _data.lostCurrencyAmount = lostCurrencyAmount;
    _data.lostCurrencyX = player.position.x;
    _data.lostCurrencyY = player.position.y;


        if(FindClosestCheckpoint() != null)
            _data.closestCheckpointID = FindClosestCheckpoint().checkPointID;

        _data.checkpoints.Clear();

        foreach (CheckPoint checkpoint in checkpoints)
        {
            _data.checkpoints.Add(checkpoint.checkPointID, checkpoint.activated);
        }
    }
    private void LoadClosestCheckpoint(GameData _data)
    {
        if (_data.closestCheckpointID == null)
            return;


        closestCheckpointID = _data.closestCheckpointID;

        foreach (CheckPoint checkpoint in checkpoints)
        {
            if (closestCheckpointID == checkpoint.checkPointID)
                player.position = checkpoint.transform.position;
        }
    }

    private CheckPoint FindClosestCheckpoint()
    {
        float closestDistance = Mathf.Infinity;
        CheckPoint closestCheckpoint = null;

        foreach (var checkpoint in checkpoints)
        {
            float distanceToCheckpoint = Vector2.Distance(player.position, checkpoint.transform.position);

            if (distanceToCheckpoint < closestDistance && checkpoint.activated == true)
            {
                closestDistance = distanceToCheckpoint;
                closestCheckpoint = checkpoint;
            }
        }

        return closestCheckpoint;
    }
}
