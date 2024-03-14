using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    static public UIManager instance;

    public Animator transition;
    public GameObject optionPanel, quitPanel;
    public Slider sliderEnergy;
    public TextMeshProUGUI energyText, playerName, moneyValue, combatValue;
    public GameObject player;
    public Transform playerPos, playerPos2;
    public bool isAct = false;
    static public float curEnergy = 1.0f;
    static public int combat = 500;
    static public int chargeCount = 1;
    int playerIndex;
    int money;

    private void Awake()
    {
        // 스텟
        combatValue.GetComponent<TextMeshProUGUI>().text
            = (PlayerPrefs.GetInt("PreAmends") + PlayerPrefs.GetInt("curAmends")).ToString();

        // 싱글톤
        DontDestroyOnLoad(this.gameObject);
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        // BGM
        if (SceneManager.GetActiveScene().name == "Main")
            SoundManager.instance.PlayBgm("Home");
        else if (SceneManager.GetActiveScene().name == "Univ")
            SoundManager.instance.PlayBgm("Univ");

        if (Spawner.isChk == true)
        {
            player.transform.position = playerPos.position + new Vector3(2.0f, 0 , 0);
        }

        // 스테이터스
        playerIndex = PlayerPrefs.GetInt("PlayerName");
        switch (playerIndex)
        {
            case 10:
                playerName.text = "인기많은대학생";
                money = 1000;
                moneyValue.text = money.ToString();
                break;
            case 20:
                playerName.text = "아웃사이더";
                money = 3000;
                moneyValue.text = money.ToString();
                break;
            case 30:
                playerName.text = "자유로운도비";
                money = 2000;
                moneyValue.text = money.ToString();
                break;
            case 40:
                playerName.text = "고독한대학생";
                money = 2500;
                moneyValue.text = money.ToString();
                break;
            case 50:
                playerName.text = "정체불명의한량";
                money = 1500;
                moneyValue.text = money.ToString();
                break;
        }
    }

    public void ActiveOptionPanel(string panelName)
    {
        SoundManager.instance.PlaySe("Click");

        if (panelName == "Option")
        {
            if (optionPanel.transform.localPosition.x == 815)
            {
                optionPanel.transform.DOLocalMoveX(465, 0.4f).SetEase(Ease.OutBack);
                isAct = true;
            }
            else if (optionPanel.transform.localPosition.x == 465)
            {
                optionPanel.transform.DOLocalMoveX(815, 0.4f).SetEase(Ease.InBack);
                isAct = false;
            }
        }
    }

    void Update()
    {
        UpdateEnergy(curEnergy);
    }

    public void UpdateEnergy(float val)
    {
        sliderEnergy.value = Mathf.Lerp(sliderEnergy.value, val, 10 * Time.deltaTime);
        energyText.text = Mathf.RoundToInt(sliderEnergy.value * 100).ToString() + " / 100";
    }

    public void ActiveQuitPanel()
    {        
        SoundManager.instance.PlaySe("Click");
        quitPanel.SetActive(true);
        if (optionPanel.transform.localPosition.x == 465)
        {
            optionPanel.transform.DOLocalMoveX(815, 0.1f).SetEase(Ease.InBack);
            isAct = true;
        }
    }

    IEnumerator SceneLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadSceneAsync(levelIndex);
    }

    public void UIHover()
    {
        SoundManager.instance.PlaySe("UIHover");
    }

    public void GoTitle()
    {
        quitPanel.SetActive(false);
        StartCoroutine(SceneLevel(0));
        SoundManager.instance.PlaySe("Click");
    }

    public void ExitPanel()
    {
        SoundManager.instance.PlaySe("Close");
        quitPanel.SetActive(false);
        isAct = false;
    }

    public void GameQuit() {
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }
}
