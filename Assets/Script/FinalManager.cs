using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class FinalManager : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject dialoguePanel;
    public Animator transition;
    string getGrade;

    void Start()
    {
        getGrade = PlayerPrefs.GetString("Grade");
        SoundManager.instance.PlayBgm("Final");
        dialoguePanel.SetActive(false);
        Invoke("Branch", 0.8f);
    }

    void Branch()
    {
        dialoguePanel.SetActive(true);

        if (getGrade.Equals("A") || getGrade.Equals("B") || getGrade.Equals("C"))
        {
            text.DOText("����� ������ �����Ͽ����ϴ�. ~ HAPPY END ~", 2.5f);
            Invoke("Clear", 5f);
        }
        else if (getGrade.Equals("D") || getGrade.Equals("F"))
        {
            text.DOText("����� ������ �����Ͽ� ������ �θ��� �޾ҽ��ϴ�. ~ BAD END ~", 2.5f);
            Invoke("Clear", 5f);
        }
    }

    public void Clear()
    {
        StartCoroutine(SceneLevel(0));
    }

    IEnumerator SceneLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        SoundManager.instance.StopBgm();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadSceneAsync(levelIndex);
    }
}
