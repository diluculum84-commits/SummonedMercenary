
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MercenaryDataManager : MonoBehaviour
{
    // 다른 스크립트에서 접근하기 위한 인스턴스
    public static MercenaryDataManager instance;

    [Header("보유 용병 데이터")]
    public List<MercenaryData> ownedMercenaries = new List<MercenaryData>();

    private void Awake()
    {
        // 아직 데이터 관리자가 없다면
        if (instance == null)
        {
            instance = this;

            // 씬이 변경되어도 오브젝트 유지
            DontDestroyOnLoad(gameObject);

            Debug.Log("MercenaryDataManager 생성 완료");
        }
        else if (instance != this)
        {
            // 이미 존재한다면 중복 오브젝트 제거
            Destroy(gameObject);
        }
    }
}
