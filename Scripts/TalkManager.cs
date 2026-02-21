using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    private Dictionary<int, string[]> talkData; // 대화 데이터 저장용 딕셔너리

    void Awake()
    {
        talkData = new Dictionary<int, string[]>(); // 딕셔너리 초기화
        GenerateData(); // 데이터 생성
    }

    void GenerateData()
    {
        talkData.Add(1000, new string[] { "안녕?", "이 곳에 처음 왔구나?" });
        talkData.Add(100, new string[] { "평범한 나무상자다." });
        talkData.Add(200, new string[] { "누군가 사용했던 흔적이 있는 책상이다." });
    }

    public string GetTalk(int id, int talkIndex)
    {
        if (talkData.ContainsKey(id) && talkIndex < talkData[id].Length)
        {
            return talkData[id][talkIndex]; // 대화 내용 반환
        }
        else
        {
            return null; // 대화 내용이 없거나 인덱스 초과 시 null 반환
        }
    }
}
