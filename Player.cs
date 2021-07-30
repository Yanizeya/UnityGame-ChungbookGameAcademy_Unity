using UnityEngine;
using System.Collections;




/*
 *	プレーヤークラス
 *	Maruchu
 *
 *	キャラクターの移動、メカニム(モーション)の制御など
 */
public class Player : MonoBehaviour
{



    public GameObject playerObject = null;      //유니티짱 본체
    public GameObject bulletObject = null;      //총알

    public Transform bulletStartPosition = null;        //발사 지점


    private static readonly float MOVE_Z_FRONT = 5.0f;  //전진 속도
    private static readonly float MOVE_Z_BACK = -2.0f;  //후진 속도

    private static readonly float ROTATION_Y_KEY = 360.0f;   //키보드 회전 속도
    private static readonly float ROTATION_Y_MOUSE = 720.0f;    //마우스 회전 속도

    private float m_rotationY = 0.0f;       //좌우 회전 각도

    private bool m_mouseLockFlag = true;        //




    /*
	 *	毎フレーム呼び出される関数
	 */
    private void Update()
    {

        //ステージクリアしていたら操作を無視
        if (Game.IsStageCleared())
        {
            return;
        }

        //マウスロック処理
        CheckMouseLock();

        //移動処理
        CheckMove();
    }


    /*
	 *	マウスロック処理のチェック
	 */
    private void CheckMouseLock()
    {

        //Escキーをおした時の動作
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //フラグをひっくり返す
            m_mouseLockFlag = !m_mouseLockFlag;
        }

        //マウスロックされてる？
        if (m_mouseLockFlag)
        {
            //ロックしていたらロック解除
            Screen.lockCursor = true;
            Cursor.visible = false;
        }
        else
        {
            //ロック解除されていたらロック
            Screen.lockCursor = false;
            Cursor.visible = true;
        }
    }
    //이동 처리 검사
    private void CheckMove()
    {

        // 회전
        {
            // 이 프레임에서 움직이는 회전량
            float addRotationY = 0.0f;

            //Q,E 키로 좌우 회전
            if (Input.GetKey(KeyCode.Q))
            {
                addRotationY = -ROTATION_Y_KEY; //키보드 회전 속도
            }
            else
            if (Input.GetKey(KeyCode.E))
            {
                addRotationY = ROTATION_Y_KEY;
            }

            //マウスの移動量による回転
            if (m_mouseLockFlag)
            {
                //移動量を取得して角度に渡す
                addRotationY += (Input.GetAxis("Mouse X") * ROTATION_Y_MOUSE);
            }

            //現在の角度に加算
            m_rotationY += (addRotationY * Time.deltaTime);     //移動量、回転量には Time.deltaTime をかけて実行環境(フレーム数の差)による違いが出ないようにします

            //オイラー角で入れる
            transform.rotation = Quaternion.Euler(0, m_rotationY, 0);       //Y軸回転でキャラの向きを横に動かせます
        }

        //移動
        Vector3 addPosition = Vector3.zero;     //移動量(z の値はメカニムにも渡す)
        {
            /*
				Vector3.zero は new Vector3( 0f, 0f, 0f) と同じです

				他にも色々あるので↓のページを参照してみてください
				http://docs.unity3d.com/ScriptReference/Vector3.html
			 */

            //キー操作から移動する量を取得
            Vector3 vecInput = new Vector3(0f, 0, Input.GetAxisRaw("Vertical"));        //Zに前後の入力を入れます(Wキー、Sキー、ゲームパッドの入力など)

            //Z に何か値が入っている？
            if (vecInput.z > 0)
            {
                //前進
                addPosition.z = MOVE_Z_FRONT;
            }
            else
            if (vecInput.z < 0)
            {
                //後退
                addPosition.z = MOVE_Z_BACK;
            }

            //移動量を Transform に渡して移動させる
            transform.position += ((transform.rotation * addPosition) * Time.deltaTime);
            /*
				Vector3 に transform.rotation をかけると、その方向へ曲げてくれます
				この時、Vector3 は Z+ の方向を正面として考えます
			 */
        }

        // 총알 발사
        bool shootFlag;
        {
            // 입력을 확인
            if (Input.GetButtonDown("Fire1"))
            {
                // 발사
                shootFlag = true;

                // 발사 위치가 있다면
                if (null != bulletStartPosition)
                {
                    // 발사 위치가 총알 생성 위치
                    Vector3 vecBulletPos = bulletStartPosition.position;
                    // 조금 앞쪽에서
                    vecBulletPos += (transform.rotation * Vector3.forward);
                    // 특정 높이에서
                    vecBulletPos.y = 1.0f;

                    // 총알을 생성
                    Instantiate(bulletObject, vecBulletPos, transform.rotation);
                }
            }
            else
            {
                //撃ってない
                shootFlag = false;
            }
        }


        //メカニム(モーション)
        {
            //アニメーターを取得
            Animator animator = playerObject.GetComponent<Animator>();

            //Animatorで設定した値を渡す
            animator.SetFloat("SpeedZ", addPosition.z); //Z(前後の移動量)
            animator.SetBool("Shoot", shootFlag);       //射撃フラグ
        }
    }




}
