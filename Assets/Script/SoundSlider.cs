using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SoundSlider : MonoBehaviour
{
    public static SoundSlider soundSlider;

    public Slider sliderBgm, sliderSe;
    public TextMeshProUGUI bgmText, seText;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            sliderBgm.value = 0.5f;
            sliderSe.value = 0.5f;
        }
        else if (SceneManager.GetActiveScene().name == "Main")
        {
            sliderBgm.value = PlayerPrefs.GetFloat("BGM");
            sliderSe.value = PlayerPrefs.GetFloat("SE"); 
        }
        else if (SceneManager.GetActiveScene().name == "Game1")
        {
            sliderBgm.value = PlayerPrefs.GetFloat("BGM");
            sliderSe.value = PlayerPrefs.GetFloat("SE");
        }

        // 볼륨값 변경
        UpdateBgmText(sliderBgm.value);
        sliderBgm.onValueChanged.AddListener(UpdateBgmText);
        UpdateSeText(sliderSe.value);
        sliderSe.onValueChanged.AddListener(UpdateSeText);
    }

    void Update()
    {
        PlayerPrefs.SetFloat("BGM", sliderBgm.value);
        PlayerPrefs.SetFloat("SE", sliderSe.value);
    }

    // 볼륨값 갱신
    public void UpdateBgmText(float val)
    {
        bgmText.text = Mathf.RoundToInt(val * 100).ToString();
    }
    public void UpdateSeText(float val)
    {
        seText.text = Mathf.RoundToInt(val * 100).ToString();
    }

    // 메뉴 볼륨값 조절
    public void ChangeBgmVolume(float value)
    {
        SoundManager.instance.bgmSource.volume = value;
    }
    public void ChangeSeVolume(float value)
    {
        for (int i = 0; i < SoundManager.instance.seSource.Length; i++)
        {
            SoundManager.instance.seSource[i].volume = value;
        }
    }
}
