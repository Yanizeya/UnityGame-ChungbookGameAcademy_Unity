using UnityEngine;
using System.Collections;




/*
 *	Collider を使ってヒットするものの基底クラス
 *	Maruchu
 */
public class HitObject : MonoBehaviour
{




    //当たり判定のグループ
    public enum HitGroup
    {
        Player1     //プレーヤー1のグループ
        , Player2       //プレーヤー2のグループ
        , Other         //それ以外(壁など)
    }

    public HitGroup m_hitGroup = HitGroup.Player1;      //플레이어 그룹



    //Collider가 닿아도 괜찮은지 확인
    protected bool IsHitOK(GameObject hittedObject)
    {

        //상대가 같은 스크립트를 가지고 있는지 확인
        HitObject hit = hittedObject.GetComponent<HitObject>();

        //같은 스크립트를 가지고 있지 않으면 닿지 않아도 된다
        if (null == hit)
        {
            return false;
        }

        //같은 그룹에 속한 것끼리는 판정을 무시한다
        if (m_hitGroup == hit.m_hitGroup)
        {
            return false;
        }

        //다른 그룹에 속한 것끼리 닿았을 때
        return true;
    }






}
