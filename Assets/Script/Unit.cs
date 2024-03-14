using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int maxHp;
    public int curHp;

    public bool TakeDamage(int dmg)
    {
        curHp -= dmg;

        if (curHp <= 0)
            return true;
        else
            return false;
    }
}

