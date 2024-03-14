using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceData : MonoBehaviour
{
    public GameManager gameManager;
    GameObject scanObject;
    public int id;  

    public void Selected()
    {
        SoundManager.instance.PlaySe("SelectP");
        gameManager.Action(gameObject);
    }
}
