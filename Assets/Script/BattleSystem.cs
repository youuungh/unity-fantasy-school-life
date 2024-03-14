using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, WIN }

public class BattleSystem : MonoBehaviour
{
    public GameObject bossPrefab, startButton, dialoguePanel, finishPanel, exitButton;
    Unit bossUnit;
    public TextMeshProUGUI dialogueText, limitText, resultText;
    public Animator transition;
    public BattleHud bossHud;
    public BattleState state;
    float setTime = 60.0f;
    bool timeActive = false;
    public string grade;


    void Start()
    {
        SoundManager.instance.PlayBgm("Boss");
        startButton.SetActive(true);
        dialoguePanel.SetActive(false);
        if (finishPanel.transform.localScale == Vector3.one)
        {
            finishPanel.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack);
        }
    }

    void Update()
    {
        if (timeActive)
        {
            setTime -= Time.deltaTime;
            limitText.text = Mathf.Round(setTime).ToString() + " 초";
            if (setTime <= 0)
            {
                int scoreResult = bossUnit.curHp;

                if (scoreResult < 5000)
                    grade = "A";
                else if (scoreResult < 8000)
                    grade = "B";
                else if (scoreResult < 11000)
                    grade = "C";
                else if (scoreResult < 14000)
                    grade = "D";
                else
                    grade = "F";

                timeActive = false;
                dialoguePanel.SetActive(false);
                EndBattle();
            }
        }
    }

    public void StartButton()
    {
        startButton.SetActive(false);
        dialoguePanel.SetActive(true);
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject bossGO = Instantiate(bossPrefab);
        bossUnit = bossGO.GetComponent<Unit>();
        dialogueText.text = "적이 등장했습니다";

        bossHud.SetHud(bossUnit);
        yield return new WaitForSeconds(1.0f);
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        dialogueText.text = "제한시간 동안 최선을 다해서 적을 공격하세요 >";
    }

    public void OnAttackButton()
    {
        StartCoroutine(PlayerAttack());
    }

    IEnumerator PlayerAttack()
    {
        SoundManager.instance.PlaySe("Attack");
        timeActive = true;
        bossUnit.TakeDamage(UIManager.combat);
        bossHud.SetHp(bossUnit.curHp);
        dialogueText.text = "적에게 데미지를 입혔다!";
        yield return new WaitForSeconds(2.5f);
        PlayerTurn();
    }

    public void EndBattle()
    {
        if (finishPanel.transform.localScale == Vector3.zero)
        {
            resultText.text = grade;
            SoundManager.instance.PlaySe("Open");
            finishPanel.transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutBack);
        }        
    }

    public void ExitScene()
    {
        SoundManager.instance.PlaySe("Click");
        PlayerPrefs.SetString("Grade", resultText.text.ToString());

        exitButton.GetComponent<Button>().interactable = false;
        StartCoroutine(SceneLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator SceneLevel(int levelIndex)
    {
        SoundManager.instance.StopBgm();
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadSceneAsync(levelIndex);
    }

    public void UIHover()
    {
        SoundManager.instance.PlaySe("UIHover");
    }
}
