using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public DialogManager dialogManager;
    public GameObject dialogPanel, choicePanel;
    public TextEffect textEffect;
    public Animator transition;
    public Image image;
    public GameObject objParam;
    public int dialogIndex;
    private bool isAction;
    string dialogData;
    bool isA, isB, isC;

    void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Prolog")
        {
            StartProlog(0, 0);
            isA = true;
            isB = true;
            isC = true;
        }
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Prolog")
            SoundManager.instance.PlayBgm("Prolog");
    }

    #region Dialog
    void StartProlog(int id, int dialogIndex)
    {
        textEffect.SetDialog(dialogManager.GetDialog(id, dialogIndex));
    }

    public void Action(GameObject scanObj)
    {
        if (textEffect.isAnim)
            return;
        else
        {
            objParam = scanObj;
            ChoiceData choiceData = objParam.GetComponent<ChoiceData>();
            PlayerPrefs.SetInt("PlayerName", choiceData.id);
            StartDialog(choiceData.id);
            choicePanel.SetActive(false);
            isA = false;
        }
    }

    void StartDialog(int id)
    {
        if (textEffect.isAnim)
        {
            CancelInvoke();
            textEffect.SetDialog("");
            return;
        }
        else
            dialogData = dialogManager.GetDialog(id, dialogIndex);

        if (dialogData == null)
        {
            isAction = false;
            dialogIndex = 0;
            dialogPanel.SetActive(isAction);
            return;
        }
        else
        {
            isAction = true;
            dialogPanel.SetActive(isAction);
            textEffect.SetDialog(dialogManager.GetDialog(id, dialogIndex));
        }
        dialogIndex++;
    }
    void NextDialog()
    {
        if (textEffect.isAnim)
            return;
        else
        {
            dialogPanel.SetActive(false);
            Invoke("FirstDialog", 1.5f);
            isA = true;
            isB = false;
        }
    }
    void FirstDialog()
    {
        StartDialog(1);
        isC = false;
    }
    void SecondDialog()
    {
        StartDialog(2);
        image.GetComponent<Image>().DOColor(Color.white, 1.0f);

        SoundManager.instance.PlayBgm("Morning");
        isB = true;
    }
    IEnumerator SceneLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadSceneAsync(levelIndex);
    }
    #endregion

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Prolog")
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.F))
            {
                if (isA == false && isB == true && dialogData != null)
                    NextDialog();
                else if (isA == true && isB == false && dialogData != null)
                    FirstDialog();
                else if (isB == false && isC == false)
                    SecondDialog();
                else if (isB == true && isC == false)
                {
                    if (textEffect.isAnim)
                        return;
                    else
                    {
                        dialogPanel.SetActive(false);
                        StartCoroutine(SceneLevel(SceneManager.GetActiveScene().buildIndex + 1));
                        SoundManager.instance.StopBgm();
                    }
                }
                else if (textEffect.isAnim)
                {
                    textEffect.SetDialog("");
                    return;
                }
                else
                    return;
            }
        }
        
    }

    public void UIHover()
    {
        SoundManager.instance.PlaySe("UIHover");
    }
}
