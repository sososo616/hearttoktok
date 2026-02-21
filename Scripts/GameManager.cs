using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject talkPanel; // 대화 패널
    public Text talkText; // 대화 텍스트
    public bool isAction; // 현재 액션 상태

    void Start()
    {
        talkPanel.SetActive(false); // 게임 시작 시 대화 패널 숨기기
        isAction = false; // 초기 액션 상태 설정
    }

    public void Action(GameObject scanObj)
    {
        if (isAction) // 대화가 열려 있는 경우
        {
            isAction = false; // 대화 종료
            talkPanel.SetActive(false); // 대화 패널 숨기기
        }
        else // 대화 시작
        {
            isAction = true; // 대화 시작
            talkText.text = "이것의 이름은 " + scanObj.name + "이라고 한다."; // 오브젝트 이름 설정
            talkPanel.SetActive(true); // 대화 패널 보이기
        }
    }
}
