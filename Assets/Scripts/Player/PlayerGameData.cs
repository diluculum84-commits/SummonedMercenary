using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameData : MonoBehaviour
{
    // Start is called before the first frame update
    public static Vector3 playerPosition; //저장된 플레이어 좌표
    public static bool hasSavedPosition; //좌표를 저장했는가
    public static bool isInsideBuilding; //건물 안에 있는가
    public static string currentBuilding; //무슨 건물에 있는가
}
