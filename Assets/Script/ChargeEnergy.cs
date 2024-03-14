using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ChargeEnergy : MonoBehaviour
{
    public UIManager UIManager;
    public GameObject charge, chargeNotice, energyNotice, alarmNotice;
    public TextMeshProUGUI chargeText, chargeNoticeText, energyNoticeText, alarmText;
    public GameObject hudEnergyText;
    public Transform hudPos;
    int energyValue;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.F))
        {
            UIManager.isAct = true;
            if (charge.transform.localScale == Vector3.zero)
            {
                SoundManager.instance.PlaySe("Open");
                charge.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
                chargeText.text = "우원의 은총을 받으시겠습니까?\n(현재 남은 횟수 : " + UIManager.chargeCount + ")";
            }
        }
    }

    public void ClickYes()
    {
        if (UIManager.chargeCount > 0 && UIManager.curEnergy < 1.0f)
        {
            SoundManager.instance.PlaySe("Charge");
            energyValue = Random.Range(1, 4);
            float curEnergy = (float)(0.1 * energyValue);
            charge.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack);
            UIManager.curEnergy += curEnergy;
            UIManager.chargeCount--;
            UIManager.isAct = false;
            TakeEnergy(energyValue);
        }
        else if (UIManager.curEnergy >= 1.0f)
            StartCoroutine(EnergyNotice());
        else if (UIManager.chargeCount <= 0)
            StartCoroutine(ChargeNotice());

        if (Sleep.weekIndex == 8)
        {
            StartCoroutine(AlarmNotice());
        }
    }
    public void ClickNo()
    {
        if (charge.transform.localScale == Vector3.one)
        {
            SoundManager.instance.PlaySe("Close");
            charge.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack);
            UIManager.isAct = false;
        }
    }

    public void TakeEnergy(int energyParam)
    {
        GameObject hudText = Instantiate(hudEnergyText);
        hudText.transform.position = hudPos.position;
        hudText.GetComponent<FloatingText>().energyParam = energyParam;
    }

    IEnumerator ChargeNotice()
    {
        if (charge.transform.localScale == Vector3.one)
        {
            UIManager.isAct = true;
            charge.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack);
            chargeNotice.SetActive(true);
            SoundManager.instance.PlaySe("Close");
            chargeNoticeText.text = "남은 은총 횟수가 없습니다";
            yield return new WaitForSeconds(2.0f);
            chargeNotice.SetActive(false);
            UIManager.isAct = false;
        }
    }

    IEnumerator EnergyNotice()
    {
        if (charge.transform.localScale == Vector3.one)
        {
            UIManager.isAct = true;
            charge.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack);
            energyNotice.SetActive(true);
            SoundManager.instance.PlaySe("Close");
            energyNoticeText.text = "행동력이 충분합니다";
            yield return new WaitForSeconds(2.0f);
            energyNotice.SetActive(false);
            UIManager.isAct = false;
        } 
    }

    IEnumerator AlarmNotice()
    {
        if (charge.transform.localScale == Vector3.one)
        {
            UIManager.isAct = true;
            charge.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack);
            alarmNotice.SetActive(true);
            SoundManager.instance.PlaySe("Close");
            alarmText.text = "중간고사 기간입니다\n'이공의제단'으로 이동하세요";
            yield return new WaitForSeconds(2.0f);
            alarmNotice.SetActive(false);
            UIManager.isAct = false;
        }
    }
}
