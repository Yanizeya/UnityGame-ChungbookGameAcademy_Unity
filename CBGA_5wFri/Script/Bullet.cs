using UnityEngine;
using System.Collections;


/*
 *	弾クラス
 *	Maruchu
 *
 *	何でもいいので接触したらエフェクトを出して消える
 */
public class Bullet : MonoBehaviour
{



    private static readonly float bulletMoveSpeed = 10.0f;                  //1秒間に弾が進む距離


    public GameObject hitEffectPrefab = null;                       //ヒットエフェクトのプレハブ




    /*
	 *	毎フレーム呼び出される関数
	 */
    private void Update()
    {

        //이동
        {
            // 1초간 이동거리
            // 유니티의 백터 표현법 http://docs.unity3d.com/ScriptReference/Vector3.html 
            Vector3 vecAddPos = (Vector3.forward * bulletMoveSpeed);

            //프레임 시간에 비례한 실제 이동거리
            transform.position += ((transform.rotation * vecAddPos) * Time.deltaTime);
        }
    }



    private void OnTriggerEnter(Collider hitCollider)
    {

        // 피격 효과 프리팹이 있다면
        if (null != hitEffectPrefab)
        {
            // 충돌 위치에 피격 효과를 생성
            Instantiate(hitEffectPrefab, transform.position, transform.rotation);
        }

        // 총알 제거
        Destroy(gameObject);
    }




}
