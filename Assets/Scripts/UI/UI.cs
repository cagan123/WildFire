using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    [Header("End Screen")]
    [SerializeField] private FadeScreenUI fadeScreenUI;
    [SerializeField] private GameObject youDied;
    [SerializeField] private GameObject restartButton;
    [Space]
    [SerializeField] private GameObject charcaterUI;
    [SerializeField] private GameObject optionsUI;
    [SerializeField] private GameObject inGameUI;

    public ItemTooltipUI itemTooltip;
    public StatTooltipUI statTooltip;
    public SpellTooltipUI spellTooltip;
    public SpellKeyUI spellKeyUI;
    void Start()
    {
        SwitchTo(inGameUI);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            SwitchWithKeyTo(charcaterUI);

        if(Input.GetKeyDown(KeyCode.O))
            SwitchWithKeyTo(optionsUI);
    }
    public void SwitchTo(GameObject _menu)
    {
        
        for (int i = 0; i < transform.childCount; i++)
        {
            bool isFadeScreen = transform.GetChild(i).GetComponent<FadeScreenUI>() != null;
            if(!isFadeScreen)
                transform.GetChild(i).gameObject.SetActive(false);
        }

        if (_menu != null)
            _menu.SetActive(true);
    }
    public void SwitchWithKeyTo(GameObject _menu)
    {
        if (_menu != null && _menu.activeSelf)
        {
            _menu.SetActive(false);
            CheckForInGameUI();
            return;
        }

        SwitchTo(_menu);
    }
    private void CheckForInGameUI()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf && transform.GetChild(i).GetComponent<FadeScreenUI>() == null)
                return;
        }

        SwitchTo(inGameUI);
    }
    public void SwitchOnEndScreen(){
        fadeScreenUI.FadeOut();
        StartCoroutine(EndScreenRoutine());
    }

    public IEnumerator EndScreenRoutine(){
        yield return new WaitForSeconds(.5f);
        youDied.SetActive(true);
        yield return new WaitForSeconds(1f);
        restartButton.SetActive(true);
    }
    public void RestrartGameButton() => GameManager.instance.RestartScene();
}
