using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    public Slider hpSlider;

    public void SetHud(Unit unit)
    {
        hpSlider.maxValue = unit.maxHp;
        hpSlider.value = unit.curHp;
    }

    public void SetHp(int hp)
    {
        hpSlider.value = hp;
    }
}
