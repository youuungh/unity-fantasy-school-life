using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextEffect : MonoBehaviour
{
    public GameObject dialogCursor;
    public int charPerSec;
    public bool isAnim;
    TextMeshProUGUI dialogText;
    string targetDialog;
    int index;
    float interval;

    private void Awake()
    {
        dialogText = GetComponent<TextMeshProUGUI>();
    }

    public void SetDialog(string dialog)
    {
        if(isAnim)
        {
            dialogText.text = targetDialog;
            CancelInvoke();
            EndEffect();
        }
        else
        {
            targetDialog = dialog;
            StartEffect();
        }
    }

    void StartEffect()
    {
        index = 0;
        dialogText.text = "";
        dialogCursor.SetActive(false);

        isAnim = true;
        interval = 1.0f / charPerSec;
        Invoke("Effecting", interval);
    }

    void Effecting()
    {
        if(dialogText.text == targetDialog)
        {
            EndEffect();
            return;
        }
        dialogText.text += targetDialog[index];

        if (targetDialog[index] != ' ' || targetDialog[index] != '.' || targetDialog != null)
            SoundManager.instance.PlaySe("Cursor");

        index++;
        Invoke("Effecting", interval);
    }

    void EndEffect()
    {
        isAnim = false;
        dialogCursor.SetActive(true);
    }
}
