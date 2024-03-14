using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class Notice : MonoBehaviour
{
    public Image panel;
    public TextMeshProUGUI text;
    public int id;
    public bool isCount = false;

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PlayerH") && id == 0)
        {
            if (!isCount) StartCoroutine(Notification_c0());
        }
        else if (other.CompareTag("PlayerH") && id == 1)
        {
            if (!isCount) StartCoroutine(Notification_c1());
        }
        else if (other.CompareTag("PlayerH") && id == 2)
        {
            if (!isCount) StartCoroutine(Notification_c2());
        }
    }

    IEnumerator Notification_c0()
    {
        text.text = "경천의 비석";
        panel.DOFade(0.5f, 1f);
        text.DOFade(1, 1f);
        yield return new WaitForSeconds(1.0f);
        panel.DOFade(0, 1f);
        text.DOFade(0, 1f);
        isCount = true;
    }
    IEnumerator Notification_c1()
    {
        text.text = "우원의 여신상";
        panel.DOFade(0.5f, 1f);
        text.DOFade(1, 1f);
        yield return new WaitForSeconds(1.0f);
        panel.DOFade(0, 1f);
        text.DOFade(0, 1f);
        isCount = true;
    }
    IEnumerator Notification_c2()
    {
        text.text = "이공의 제단";
        panel.DOFade(0.5f, 1f);
        text.DOFade(1, 1f);
        yield return new WaitForSeconds(1.0f);
        panel.DOFade(0, 1f);
        text.DOFade(0, 1f);
        isCount = true;
    }
}
