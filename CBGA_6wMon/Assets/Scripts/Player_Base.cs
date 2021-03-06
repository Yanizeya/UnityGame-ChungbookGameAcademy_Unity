using UnityEngine;
using System.Collections;




//플레이어 클래스 기저
//캐릭터 이동, 메카님(메션)제어 등
public class Player_Base : HitObject
{




    //플레이어 조작 종류
    protected enum PlayerInput
    {
        Move_Left       //이동 왼쪽
        , Move_Up       //이동 위쪽
        , Move_Right        //이동 오른쪽
        , Move_Down     //이동 아래쪽
        , Shoot         //사격
        , EnumMax       //전체 조작 개수
    }

    private static readonly float MOVE_ROTATION_Y_LEFT = -90f;      //이동방향 왼쪽
    private static readonly float MOVE_ROTATION_Y_UP = 0f;      //위쪽
    private static readonly float MOVE_ROTATION_Y_RIGHT = 90f;      //오른쪽
    private static readonly float MOVE_ROTATION_Y_DOWN = 180f;      //아래쪽

    public float MOVE_SPEED = 5.0f;     //移動の速度





    public GameObject playerObject = null;      //動かす対象のモデル
    public GameObject bulletObject = null;      //弾プレハブ


    public GameObject hitEffectPrefab = null;       //ヒットエフェクトのプレハブ





    private float m_rotationY = 0.0f;       //プレーヤーの回転角度

    protected bool[] m_playerInput = new bool[(int)PlayerInput.EnumMax];        //押されている操作

    protected bool m_playerDeadFlag = false;        //プレーヤーが死んだフラグ




    private void Update()
    {

        //플레이어가 사망한 상태
        if (m_playerDeadFlag)
        {
            //모든 처리를 무시한다
            return;
        }

        //플래그 초기화
        ClearInput();
        //입력 처리를 얻는다
        GetInput();

        //이동 처리
        CheckMove();
    }

    //입력 처리 초기화
    private void ClearInput()
    {
        //플래그 초기화
        int i;
        for (i = 0; i < (int)PlayerInput.EnumMax; i++)
        {
            m_playerInput[i] = false;
        }
    }

    //입력 처리 검사
    protected virtual void GetInput()
    {
    }


    //이동처리검사
    private void CheckMove()
    {

        //에니메이터를 얻는다
        Animator animator = playerObject.GetComponent<Animator>();

        //총알에 맞지 않았으면 이동 가능
        float moveSpeed = MOVE_SPEED;       //이동속도
        bool shootFlag = false;         //

        //移動と回転
        {
            //キー操作による回転と移動
            if (m_playerInput[(int)PlayerInput.Move_Left])
            {
                //左
                m_rotationY = MOVE_ROTATION_Y_LEFT;
            }
            else
            if (m_playerInput[(int)PlayerInput.Move_Up])
            {
                //上
                m_rotationY = MOVE_ROTATION_Y_UP;
            }
            else
            if (m_playerInput[(int)PlayerInput.Move_Right])
            {
                //右
                m_rotationY = MOVE_ROTATION_Y_RIGHT;
            }
            else
            if (m_playerInput[(int)PlayerInput.Move_Down])
            {
                //下
                m_rotationY = MOVE_ROTATION_Y_DOWN;
            }
            else
            {
                //何も押してなければ移動しない
                moveSpeed = 0f;
            }

            //向いている方向をオイラー角で入れる
            transform.rotation = Quaternion.Euler(0, m_rotationY, 0);       //Y軸回転でキャラの向きを横に動かせます

            //移動量を Transform に渡して移動させる
            transform.position += ((transform.rotation * (Vector3.forward * moveSpeed)) * Time.deltaTime);
        }

        //射撃
        {
            //射撃ボタン(クリック)押してる？
            if (m_playerInput[(int)PlayerInput.Shoot])
            {
                //撃った
                shootFlag = true;

                //弾を生成する位置
                Vector3 vecBulletPos = transform.position;
                //進行方向にちょっと前へ
                vecBulletPos += (transform.rotation * Vector3.forward);
                //Yは高さを適当に上げる
                vecBulletPos.y = 2.0f;

                //弾を生成
                Instantiate(bulletObject, vecBulletPos, transform.rotation);
            }
            else
            {
                //撃ってない
                shootFlag = false;
            }
        }


        //メカニム
        {
            //Animatorで設定した値を渡す
            animator.SetFloat("Speed", moveSpeed);      //移動量
            animator.SetBool("Shoot", shootFlag);       //射撃フラグ
        }
    }




    /*
     *	Collider が何かにヒットしたら呼ばれる関数
     *
     *	自分の GameObject に Collider(IsTriggerをつける) と Rigidbody をつけると呼ばれるようになります
     */
    private void OnTriggerEnter(Collider hitCollider)
    {

        //ヒットして良いか確認
        if (false == IsHitOK(hitCollider.gameObject))
        {
            //このオブジェクトにはあたってはいけない
            return;
        }

        //弾に当たった
        {
            //アニメーター(メカニム)を取得
            Animator animator = playerObject.GetComponent<Animator>();

            //メカニムに死んだことを通知
            animator.SetBool("Dead", true);     //死んだフラグ
        }

        //ヒットエフェクトある？
        if (null != hitEffectPrefab)
        {
            //自分と同じ位置でヒットエフェクトを出す
            Instantiate(hitEffectPrefab, transform.position, transform.rotation);
        }

        //このプレーヤーは死んだ状態にする
        m_playerDeadFlag = true;
    }




}
