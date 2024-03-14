using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    Dictionary<int, string[]> dialogData;

    void Awake()
    {
        dialogData = new Dictionary<int, string[]>();
        AddData();
    }
    void AddData()
    {
        dialogData.Add(0, new string[] { "당신은 어떤 학생인가, 아니면 어떤 학생이 되고 싶은가 ··· ?" });

        // 선택지
        dialogData.Add(10, new string[] { "당신은 ··· '인기많은대학생' 입니다." });
        dialogData.Add(20, new string[] { "당신은 ··· '아웃사이더' 입니다." });
        dialogData.Add(30, new string[] { "당신은 ··· '자유로운도비' 입니다." });
        dialogData.Add(40, new string[] { "당신은 ··· '고독한대학생' 입니다." });
        dialogData.Add(50, new string[] { "당신은 ··· '정체불명의한량' 입니다." });

        // 프롤로그
        dialogData.Add(1, new string[] { "",
            "2032년 10월 17일 ―.",
            "세상이 변한 날이다.",
            "지구 곳곳에 운석이 떨어졌고 ···, 폭심지에 있던 사람들은 알 수 없는 힘으로 인해 '마물'이 되었다.",
            "'마물'들은 다른 살아있는 사람들을 공격했고, 공격받아 죽은 사람 또한 '마물'로 변하였다.",
            "'알 수 없는 힘'으로 인해 지구의 지형은 변했으며, 곳곳에 비석이나 제단이 생겼다.",
            "마물 앞에선 현대 무기 조차 무기력 했다.",
            "무자비하게 사람들이 죽어나가던 중, 성인인 사람들에게 '이능력'이 생겼다.\n사람들은 상태창을 확인할 수 있었는데,",
            "자신의 전투력 수치를 눈으로 확인할 수 있으며, 비석을 손으로 만지면 비경으로 이동할 수 있었다.",
            "비경에서 시련을 겪으면 전투력이 올랐고, 일정 기준의 전투력을 충족하면 마물을 해치울 수 있었다.", 
            "이런 사실이 널리 퍼진 후, 각 나라에선 비석이 있는 곳에 대학교를 세웠다.",
            "·········.",
            "그리고 나는 지금 강남대학교에 재학중이다.",
            "시련을 통하여 일정 기준을 충족해야 졸업할 수 있고,",
            "졸업에 실패하면 군대에 끌려가야 한다.",
            "······.",
            "···.",
            "현재 당신은 마지막 학기를 앞두고 있다\n전투력을 올려 졸업에 성공하자.",
            "기본 이동은 'W, A, S, D' 키 와 '화살표' 키로 이루어지며 확인 키는 'F' 키 이다."
        });

        dialogData.Add(2, new string[] { "··· 흐아암, 수업있는 날인가···.\n슬슬 일어나지 않으면 지각해 버린다." });
    }
    public string GetDialog(int id, int dialogIndex)
    {
        if (dialogIndex == dialogData[id].Length)
            return null;
        else
            return dialogData[id][dialogIndex];
    }
}
