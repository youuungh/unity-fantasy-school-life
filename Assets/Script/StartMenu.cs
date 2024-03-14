using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StartMenu : MonoBehaviour
{
    public Animator transition;
    public GameObject startBtn, loadBtn, optionBtn, optionPanel, quitBtn;
    public GameObject menu;

    void Start()
    {
        SoundManager.instance.PlayBgm("Start");
        PlayerPrefs.DeleteAll();
        Invoke("ShowMenu", 1.5f);
    }
    void ShowMenu()
    {
        if (menu.transform.localScale == Vector3.zero)
            menu.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
    }

    public void GameStart()
    {
        SoundManager.instance.PlaySe("Start");
        SoundManager.instance.StopBgm();

        StartCoroutine(SceneLevel(SceneManager.GetActiveScene().buildIndex + 1));
        
        // 버튼 비활성화
        startBtn.GetComponent<EventTrigger>().enabled = false;
        loadBtn.GetComponent<EventTrigger>().enabled = false;
        optionBtn.GetComponent<EventTrigger>().enabled = false;
        quitBtn.GetComponent<EventTrigger>().enabled = false;
        startBtn.GetComponent<Button>().interactable = false;
        loadBtn.GetComponent<Button>().interactable = false;
        optionBtn.GetComponent<Button>().interactable = false;
        quitBtn.GetComponent<Button>().interactable = false;
    }
    IEnumerator SceneLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadSceneAsync(levelIndex);
    }

    public void GameLoad()
    {
        SoundManager.instance.PlaySe("Select");
    }

    public void GameOption()
    {
        SoundManager.instance.PlaySe("Select");

        if (optionPanel.transform.localScale == Vector3.zero)
            optionPanel.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);

        // 버튼 비활성화
        startBtn.GetComponent<EventTrigger>().enabled = false;
        loadBtn.GetComponent<EventTrigger>().enabled = false;
        optionBtn.GetComponent<EventTrigger>().enabled = false;
        quitBtn.GetComponent<EventTrigger>().enabled = false;
        startBtn.GetComponent<Button>().interactable = false;
        loadBtn.GetComponent<Button>().interactable = false;
        optionBtn.GetComponent<Button>().interactable = false;
        quitBtn.GetComponent<Button>().interactable = false;
    }

    public void OptionQuit()
    {
        SoundManager.instance.PlaySe("Close");

        if (optionPanel.transform.localScale == Vector3.one)
            optionPanel.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack);

        // 버튼 활성화
        startBtn.GetComponent<EventTrigger>().enabled = true;
        loadBtn.GetComponent<EventTrigger>().enabled = true;
        optionBtn.GetComponent<EventTrigger>().enabled = true;
        quitBtn.GetComponent<EventTrigger>().enabled = true;
        startBtn.GetComponent<Button>().interactable = true;
        loadBtn.GetComponent<Button>().interactable = true;
        optionBtn.GetComponent<Button>().interactable = true;
        quitBtn.GetComponent<Button>().interactable = true;
    }

    public void GameQuit()
    {
        Application.Quit();
    }

    public void HoverSe()
    {
        SoundManager.instance.PlaySe("Hover");
    }
  
}
