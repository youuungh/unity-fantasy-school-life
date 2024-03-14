using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class Sleep : MonoBehaviour
{
    public UIManager UIManager;
    public GameObject sleep, alarmNotice;
    public TextMeshProUGUI week, sleepText, alarmText;
    public Image sleepImage;
    public static int weekIndex = 8;
    bool isFlag = false;
    public Notice n0, n1, n2;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(Input.GetKey(KeyCode.F))
        {
            UIManager.isAct = true;

            if (UIManager.curEnergy > 0)
            {
                if (sleep.transform.localScale == Vector3.zero)
                {
                    SoundManager.instance.PlaySe("Open");
                    sleep.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
                    sleepText.text = "행동력이 남아있습니다\n잠을 자시겠습니까?";
                }
            }
            else if (UIManager.curEnergy == 0.0f)
            {
                if (sleep.transform.localScale == Vector3.zero)
                {
                    SoundManager.instance.PlaySe("Open");
                    sleep.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
                    sleepText.text = "잠을 자시겠습니까?";
                }
            }
        }
    }

    void Update()
    {
        week.text = weekIndex + "주차";
    }

    public void ClickYes()
    {
        if (sleep.transform.localScale == Vector3.one)
        {
            if (Sleep.weekIndex == 8)
            {
                StartCoroutine(AlarmNotice());
            }
            else
            {
                SoundManager.instance.PlaySe("Click");
                sleep.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack);
                isFlag = true;

                if (isFlag)
                {
                    weekIndex++;
                    n0.isCount = false;
                    n1.isCount = false;
                    n2.isCount = false;

                    UIManager.chargeCount = 1;
                    UIManager.curEnergy = 1.0f;
                    StartCoroutine(Sleeping());
                }
            }
        }
    }

    public void ClickNo() {
        if (sleep.transform.localScale == Vector3.one)
        {
            SoundManager.instance.PlaySe("Close");
            sleep.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack);
            UIManager.isAct = false;
        }
    }

    IEnumerator Sleeping()
    {
        sleepImage.gameObject.SetActive(true);
        SoundManager.instance.PauseBgm();
        SoundManager.instance.PlaySe("Sleep");
        sleepImage.DOFade(1, (float)0.5);
        yield return new WaitForSeconds(2.5f);
        sleepImage.DOFade(0, 1);
        sleepImage.gameObject.SetActive(false);
        SoundManager.instance.PlayBgm("Home");
        isFlag = false;
        UIManager.isAct = false;
    }

    IEnumerator AlarmNotice()
    {
        if (alarmNotice.transform.localScale == Vector3.one)
        {
            UIManager.isAct = true;
            sleep.transform.DOScale(Vector3.zero, 0.1f).SetEase(Ease.InBack);
            alarmNotice.SetActive(true);
            SoundManager.instance.PlaySe("Close");
            alarmText.text = "중간고사 기간입니다\n'이공의제단'으로 이동하세요";
            yield return new WaitForSeconds(2.0f);
            alarmNotice.SetActive(false);
            UIManager.isAct = false;
        }
    }
}
