using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private GameObject continueButton;

    [SerializeField] private FadeScreenUI fadeScreenUI;
    private void Start(){
        if(!SaveManager.Instance.HasSavedData()){
            continueButton.SetActive(false);
        }
    }
    public void ContinueGame(){
        StartCoroutine(LoadSceneWithFadeEffect(1f));
    }
    public void NewGame(){
        SaveManager.Instance.DeleteSavedData();
        StartCoroutine(LoadSceneWithFadeEffect(1f));
    }
    public void ExitGame(){
        Application.Quit();
    }
    private IEnumerator LoadSceneWithFadeEffect(float _delay){
        fadeScreenUI.FadeOut();
        yield return new WaitForSeconds(_delay);
        SceneManager.LoadScene(sceneName);
    }
}
