using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private GameObject continueButton;

    private void Start(){
        if(!SaveManager.Instance.HasSavedData()){
            continueButton.SetActive(false);
        }
    }
    public void ContinueGame(){
        SceneManager.LoadScene(sceneName);
    }
    public void NewGame(){
        SaveManager.Instance.DeleteSavedData();
        SceneManager.LoadScene(sceneName);
    }
    public void ExitGame(){
        Application.Quit();
    }
}
