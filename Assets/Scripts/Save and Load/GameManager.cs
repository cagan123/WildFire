using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, ISaveManager
{
    public static GameManager instance{ get; private set; }
    [SerializeField] private CheckPoint[] checkPoints;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    public void Start()
    {
        checkPoints = FindObjectsOfType<CheckPoint>();
    }

    public void RestartScene(){
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void LoadData(GameData _data)
    {
        
    }

    public void SaveData(ref GameData _data)
    {
        foreach(CheckPoint checkPoint in checkPoints){
            _data.checkpoints.Add(checkPoint.checkPointID, checkPoint.activated);
        }
    }
}
