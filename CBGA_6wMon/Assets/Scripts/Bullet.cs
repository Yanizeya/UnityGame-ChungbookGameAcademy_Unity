using UnityEngine;
using System.Collections;


/*
 *	弾クラス
 *	Maruchu
 *
 *	何でもいいので接触したらエフェクトを出して消える
 */
public class Bullet : HitObject
{



    private static readonly float bulletMoveSpeed = 10.0f;                  //1秒間に弾が進む距離


    public GameObject hitEffectPrefab = null;                       //ヒットエフェクトのプレハブ




    /*
     *	毎フレーム呼び出される関数
     */
    private void Update()
    {

        {
            Vector3 vecAddPos = (Vector3.forward * bulletMoveSpeed);

            transform.position += ((transform.rotation * vecAddPos) * Time.deltaTime);
        }
    }



    //Collider가 어떤 물체에 닿으면 호출되는 함수
    //자신의 GameObject에 Collider(iIsTrigger를 ON으로 하여)와 Rigidbody를 적용하면 호출 가능한 상태가 된다
private void OnTriggerEnter(Collider hitCollider)
    {

        //히트(닿았을 때) 검사
        if (false == IsHitOK(hitCollider.gameObject))
        {
            //히트가 없으면 그냥 종료
            return;
        }

        //히트 효과 프리팹이 있으면
        if (null != hitEffectPrefab)
        {
            //현재 위치에 히트효과 생성
            Instantiate(hitEffectPrefab, transform.position, transform.rotation);
        }

        //해당 게임 오브젝트를 Hierarchy에서 제거
        Destroy(gameObject);
    }





}
