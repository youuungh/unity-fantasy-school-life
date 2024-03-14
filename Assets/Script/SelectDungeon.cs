using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class SelectDungeon : MonoBehaviour
{
    public UIManager UIManager;
    public GameObject dungeon, entrance, energyNotice, alarmNotice;
    public TextMeshProUGUI countText, energyNoticeText, alarmText;
    public Animator transition;
    float setTime = 3.0f;
    bool timeActive = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.F))
        {
            UIManager.isAct = true;
            
            if (dungeon.transform.localScale == Vector3.zero)
            {
                SoundManager.instance.PlaySe("Open");
                dungeon.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
            }
        }
    }

    void Update()
    {
        if (timeActive)
        {
            setTime -= Time.deltaTime;
            countText.text = Mathf.Round(setTime).ToString() + " 초후 입장";
            if (setTime <= 0)
            {
                timeActive = false;
                SoundManager.instance.PauseBgm();
                StartCoroutine(SceneLevel(SceneManager.GetActiveScene().buildIndex + 1));
            }
        }
    }
    public void ClosePanel()
    {
        SoundManager.instance.PlaySe("Click");
        if (dungeon.transform.localScale == Vector3.one)
        {
            dungeon.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack);
            UIManager.isAct = false;
        }
    }
    public void EntranceTime(int val)
    {
        if (Sleep.weekIndex == 8)
        {
            if (dungeon.transform.localScale == Vector3.one)
            {
                dungeon.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack);
                StartCoroutine(AlarmNotice());
            }
        }
        else
        {
            SoundManager.instance.PlaySe("Click");
            PlayerPrefs.SetInt("PreAmends", UIManager.combat);

            if (UIManager.curEnergy >= 0.3f)
            {
                float curEnergy = (float)(0.01f * val);
                UIManager.curEnergy -= curEnergy;
                timeActive = true;
                if (dungeon.transform.localScale == Vector3.one)
                {
                    dungeon.transform.DOScale(Vector3.zero, 0.05f).SetEase(Ease.InBack);
                    entrance.SetActive(true);
                }
            }

            if (UIManager.curEnergy < 0.3f)
            {
                if (dungeon.transform.localScale == Vector3.one)
                {
                    dungeon.transform.DOScale(Vector3.zero, 0.05f).SetEase(Ease.InBack);
                    StartCoroutine(EnergyNotice());
                }
            }
        }
    }
    IEnumerator EnergyNotice()
    {
        UIManager.isAct = true;
        energyNotice.SetActive(true);
        SoundManager.instance.PlaySe("Close");
        energyNoticeText.text = "행동력이 부족합니다";
        yield return new WaitForSeconds(2.0f);
        energyNotice.SetActive(false);
        UIManager.isAct = false;
    }

    IEnumerator SceneLevel(int levelIndex)
    {
        PlayerPrefs.SetFloat("SaveEnergy", UIManager.curEnergy);
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadSceneAsync(levelIndex);
        entrance.SetActive(false);
    }

    IEnumerator AlarmNotice()
    {
        UIManager.isAct = true;
        alarmNotice.SetActive(true);
        SoundManager.instance.PlaySe("Close");
        alarmText.text = "중간고사 기간입니다\n'이공의제단'으로 이동하세요";
        yield return new WaitForSeconds(2.0f);
        alarmNotice.SetActive(false);
        UIManager.isAct = false;
    }
}
