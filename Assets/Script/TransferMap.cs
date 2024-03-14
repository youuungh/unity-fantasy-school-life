using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    public GameObject player;
    public Transform targetPoint;
    public Image fadeImage;
    public UIManager UIManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerU"))
            StartCoroutine(TransToUnivEffect());
        else if (collision.CompareTag("PlayerH"))
            StartCoroutine(TransToHomeEffect());
    }

    IEnumerator TransToUnivEffect()
    {
        UIManager.isAct = true;
        fadeImage.gameObject.SetActive(true);
        SoundManager.instance.PauseBgm();
        fadeImage.DOFade(1, (float)0.5);
        SoundManager.instance.PlayBgm("Univ");
        yield return new WaitForSeconds(1.5f);
        fadeImage.DOFade(0, 1);
        fadeImage.gameObject.SetActive(false);
        player.transform.position = targetPoint.position + new Vector3(0, 1.5f, 0);
        player.tag = "PlayerH";
        yield return new WaitForSeconds(0.4f);
        UIManager.isAct = false;
    }

    IEnumerator TransToHomeEffect()
    {
        UIManager.isAct = true;
        fadeImage.gameObject.SetActive(true);
        SoundManager.instance.PauseBgm();
        fadeImage.DOFade(1, (float)0.5);
        SoundManager.instance.PlayBgm("Home");
        yield return new WaitForSeconds(1.5f);
        fadeImage.DOFade(0, 1);
        fadeImage.gameObject.SetActive(false);
        player.transform.position = targetPoint.position + new Vector3(0, 1.5f, 0);
        player.tag = "PlayerU";
        yield return new WaitForSeconds(0.4f);
        UIManager.isAct = false;
    }
}
