using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class BossTest : MonoBehaviour
{
    public UIManager UIManager;
    public GameObject entry, entrance;
    public TextMeshProUGUI entryText, countText;
    public Animator transition;
    float setTime = 3.0f;
    bool timeActive = false;
    bool isflag = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.F))
        {
            if (isflag == false && Sleep.weekIndex == 8)
            {
                UIManager.isAct = true;

                if (entry.transform.localScale == Vector3.zero)
                {
                    SoundManager.instance.PlaySe("Open");
                    entry.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
                    entryText.text = "시험을 치르시겠습니까?";
                }
            }
        }
    }

    private void Start()
    {
        entrance.SetActive(false);
    }

    private void Update()
    {
        if (timeActive)
        {
            setTime -= Time.deltaTime;
            countText.text = Mathf.Round(setTime).ToString() + " 초후 입장";
            if (setTime <= 0)
            {
                timeActive = false;
                SoundManager.instance.PauseBgm();
                StartCoroutine(SceneLevel(SceneManager.GetActiveScene().buildIndex + 3));
            }
        }
    }

    public void ClickYes()
    {
        if (entry.transform.localScale == Vector3.one)
        {
            SoundManager.instance.PlaySe("Close");
            entry.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack);
            timeActive = true;
            entrance.SetActive(true);
            isflag = true;
        }
    }

    public void ClickNo()
    {
        if (entry.transform.localScale == Vector3.one)
        {
            SoundManager.instance.PlaySe("Close");
            entry.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack);
            UIManager.isAct = false;
        }
    }

    IEnumerator SceneLevel(int levelIndex)
    {
        PlayerPrefs.SetFloat("SaveEnergy", UIManager.curEnergy);
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadSceneAsync(levelIndex);
        entrance.SetActive(false);
    }
}
