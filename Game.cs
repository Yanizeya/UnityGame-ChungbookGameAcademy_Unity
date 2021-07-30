using UnityEngine;
using System.Collections;




/*
 *	ゲームの情報を管理するクラス
 *	Maruchu
 */
public class Game : MonoBehaviour
{




    private static bool m_stageClearFlag = false;   //これが true ならステージクリアとする

    /*
	 *	ステージクリアしたら呼ばれる
	 */
    public static void SetStageClear()
    { //public 이면 어디서든 갖다 쓸 수 있는 static 함수다

        m_stageClearFlag = true;
    }

    public static bool IsStageCleared()
    {
        return m_stageClearFlag;
    }




}
