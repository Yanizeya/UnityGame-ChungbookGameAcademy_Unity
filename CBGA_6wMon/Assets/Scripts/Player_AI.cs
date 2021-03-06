using UnityEngine;
using System.Collections;




public class Player_AI : Player_Base
{



    //チェック方向
    private enum CheckDir
    {
        Left        
        , Up            
        , Right     
        , Down      
        , EnumMax   
    }
    //チェック情報
    private enum CheckData
    {
        X           
        , Y         
        , EnumMax   
    }

    //검사 방향
    private static readonly int[][] CHECK_DIR_LIST = new int[(int)CheckDir.EnumMax][] {
	//										 X		 Y
	 new int[ (int)CheckData.EnumMax] {     -1,      0      }
    ,new int[ (int)CheckData.EnumMax] {      0,      1      }
    ,new int[ (int)CheckData.EnumMax] {      1,      0      }
    ,new int[ (int)CheckData.EnumMax] {      0,     -1      }
};

//    예시 1
//    int[][] jaggedArray1 = new int[3][]
//jaggedArray[0] = new int[] { 11, 12, 13 };
//jaggedArray[1] = new int[] { 21, 22, 23, 24 };
//jaggedArray[2] = new int[] { 31, 32 };

// 예시 2
//int[][] jaggedArray2 = new int[][]
//{
//    new int[] { 11, 12, 13 },
//    new int[] { 21, 22, 23, 24 },
//    new int[] { 31, 32 }
//};

private static readonly int AI_PRIO_MIN = 99;                       //ai 우선순위 중 가장 낮은 값



    private static readonly float AI_INTERVAL_MIN = 0.5f;                       //AI 사고 간격 가장 짧은 값
    private static readonly float AI_INTERVAL_MAX = 0.8f;                       //AI 사고 간격 가장 긴 값

    private static readonly float AI_IGNORE_DISTANCE = 2.0f;                    // 이 이상 플레이어에게 다가가지 않는다

    private static readonly float SHOOT_INTERVAL = 1.0f;                        //사격 간격



    private float m_aiInterval = 0f;                        //AI의 사고를 갱신할 때까지의 시간
    private float m_shootInterval = 0f;                     //사격하는 사고를 갱신할 때까지의 시간


    private PlayerInput m_pressInput = PlayerInput.Move_Left;   //AI가 하는 입력의 종류




    protected override void GetInput()
    {
        // 사용자가 조종하는 플레이어 오브젝트를 얻는다
        GameObject mainObject = Player_Key.m_mainPlayer;
        if (null == mainObject)
        {
            // 플레이어가 없으면 사고를 중단
            return;
        }



        //AI의 사고를 갱신할 때까지의 시간
        m_aiInterval -= Time.deltaTime;

        //사격하는 사고를 갱신할 때까지의 시간
        m_shootInterval -= Time.deltaTime;



        //플레이어와 자신의 거리를 계산한다
        Vector3 aiSubPosition = (transform.position - mainObject.transform.position);
        //Debug.Log("거리 :" + aiSubPosition);
        aiSubPosition.y = 0f;


        //거리가 생기면 움직인다
        if (aiSubPosition.magnitude > AI_IGNORE_DISTANCE)
        {

            //일정시간마다 ai를 갱신한다
            if (m_aiInterval < 0f)
            {

                //다음 사고까지 기다릴 시간, 무작위로 결정
                m_aiInterval = Random.Range(AI_INTERVAL_MIN, AI_INTERVAL_MAX);      


                //현재 AI 위치에서 상하좌우의 우선순위를 얻는다
                int[] prioTable = GetMovePrioTable();

                //가장 우선순위가 높은 장소의 숫자를 가져온다
                int highest = AI_PRIO_MIN;
                int i;
                for (i = 0; i < (int)CheckDir.EnumMax; i++)
                {
                    //값이 작을수록 우선순위가 높다
                    if (highest > prioTable[i])
                    {
                        //우선순위 갱신
                        highest = prioTable[i];
                    }
                }

                //어느 방향의 우선순위가 높은지 결정한다
                PlayerInput pressInput = PlayerInput.Move_Left;
                if (highest == prioTable[(int)CheckDir.Left])
                {
                    //왼쪽으로 이동
                    pressInput = PlayerInput.Move_Left;
                }
                else
                if (highest == prioTable[(int)CheckDir.Right])
                {
                    //右に移動
                    pressInput = PlayerInput.Move_Right;
                }
                else
                if (highest == prioTable[(int)CheckDir.Up])
                {
                    //上に移動
                    pressInput = PlayerInput.Move_Up;
                }
                else
                if (highest == prioTable[(int)CheckDir.Down])
                {
                    //下に移動
                    pressInput = PlayerInput.Move_Down;
                }
                m_pressInput = pressInput;
            }

            //入力
            m_playerInput[(int)m_pressInput] = true;
        }


        //사격의 사고를 하는가
        if (m_shootInterval < 0f)
        {
            //Debug.Log(aiSubPosition);
            //X, 또는 Z의 거리가 가까울 경우 직선상에 있다고 판단하여 사격하는
            if ((Mathf.Abs(aiSubPosition.x) < 1f) || (Mathf.Abs(aiSubPosition.z) < 1f))
            {
                //Debug.Log("distance : " + aiSubPosition);
                //射撃操作
                if((Mathf.Abs(aiSubPosition.x) < 1f))
                {

                }
                else if ((Mathf.Abs(aiSubPosition.z) < 1f))
                {

                }
                m_playerInput[(int)PlayerInput.Shoot] = true;

                //次の射撃はこの時間が経過するまで待つ(連射の抑制)
                m_shootInterval = SHOOT_INTERVAL;
            }
        }
    }








    /*
     *	位置からグリッドへ変換 グリッドX
     */
    private int GetGridX(float posX)
    {
        //Mathf.Clamp() 1번 인자의 값이 2번보다 크고  3번보다 작게 반환한다
        return Mathf.Clamp((int)((posX) / Field.BLOCK_SCALE), 0, (Field.FIELD_GRID_X - 1));
    }
    /*
     *	位置からグリッドへ変換 グリッドY
     */
    private int GetGridY(float posZ)
    {
        //UnityではXZ平面が地平線
        return Mathf.Clamp((int)((posZ) / Field.BLOCK_SCALE), 0, (Field.FIELD_GRID_Y - 1));
    }



    /*
     *	AIが移動するときの優先度の算出
     */
    private int[] GetMovePrioTable()
    {

        int i, j;

        //자기 자신(AI)의 위치
        Vector3 aiPosition = transform.position;
        //그리드로 변환
        int aiX = GetGridX(aiPosition.x);
        int aiY = GetGridY(aiPosition.z);

        //사용자가 움직이는 플레이어의 객체 가져오기
        GameObject mainObject = Player_Key.m_mainPlayer;
        //공격 목표 위치 취득
        Vector3 playerPosition = mainObject.transform.position;
        //그리드로 변환
        int playerX = GetGridX(playerPosition.x);
        int playerY = GetGridY(playerPosition.z);
        int playerGrid = playerX + (playerY * Field.FIELD_GRID_X);


        //그리드의 각 위치별 우선도를 저장하는 배열
        int[] calcGrid = new int[(Field.FIELD_GRID_X * Field.FIELD_GRID_Y)];
        //초기화
        for (i = 0; i < (Field.FIELD_GRID_X * Field.FIELD_GRID_Y); i++)
        {
            //우선도를 최저로 하다
            calcGrid[i] = AI_PRIO_MIN;
        }



        //플레이어가 현재 있는 자리에 먼저 1을 넣는다
        calcGrid[playerGrid] = 1;


        //체크하는 우선도는 일단 1부터
        int checkPrio = 1;
        //체크용 변수
        int checkX;
        int checkY;
        int tempX;
        int tempY;
        int tempGrid;
        //뭔가 체크하면 true
        bool update;
        do
        {
            //초기화
            update = false;

            //체크개시
            for (i = 0; i < (Field.FIELD_GRID_X * Field.FIELD_GRID_Y); i++)
            {
                //체크하는 우선도가 아니라면 무시

                if (checkPrio != calcGrid[i])
                {
                    continue;
                }

                // 이 우선도의 좌푤ㄹ x형으로 변환

                checkX = (i % Field.FIELD_GRID_X);
                checkY = (i / Field.FIELD_GRID_X);

                //Debug.Log(checkX + " " + checkY);
                //거기서부터 상하좌우 위치를 체크
                for (j = 0; j < (int)CheckDir.EnumMax; j++)
                { 
                    //알아보는 장소 옆
                    tempX = (checkX + CHECK_DIR_LIST[j][(int)CheckData.X]);
                    tempY = (checkY + CHECK_DIR_LIST[j][(int)CheckData.Y]);
                    //Debug.Log(checkX +" "+ checkY);
                    //그리드 밖이라면
                    if ((tempX < 0) || (tempX >= Field.FIELD_GRID_X) || (tempY < 0) || (tempY >= Field.FIELD_GRID_Y))
                    {
                        //무시한다
                        continue;
                    }
                    //여기를 알아보다
                    tempGrid = (tempX + (tempY * Field.FIELD_GRID_X));

                    //옆이 벽인지 체크
                    if (Field.ObjectKind.Block == (Field.ObjectKind)Field.GRID_OBJECT_DATA[tempGrid])
                    {
                        //벽면무시
                        continue;
                    }

                    //이 장소의 우선도 숫자가 현재 체크하고 있는 우선도보다 크면 새로 고침
                    if (calcGrid[tempGrid] > (checkPrio + 1))
                    {
                        //값을 갱신 
                        calcGrid[tempGrid] = (checkPrio + 1);   //이 숫자가 다음에 체크할 때 우선도
                                                                //플래그를 세우다
                        update = true;
                    }
                }
            }

            //체크하는 우선순위를 +1 함
            checkPrio++;

            //뭔가 업데이트가 있으면 다시 돌려보는
        } while (update);



        //AIの周辺の優先度テーブル
        int[] prioTable = new int[(int)CheckDir.EnumMax];

        //優先度テーブルが作成できたらAIの周辺の優先度を取得
        for (i = 0; i < (int)CheckDir.EnumMax; i++)
        {

            //調べる場所の隣
            tempX = (aiX + CHECK_DIR_LIST[i][(int)CheckData.X]);
            tempY = (aiY + CHECK_DIR_LIST[i][(int)CheckData.Y]);
            //グリッドの外？
            if ((tempX < 0) || (tempX >= Field.FIELD_GRID_X) || (tempY < 0) || (tempY >= Field.FIELD_GRID_Y))
            {
                //場外なので優先度を最低にする
                prioTable[i] = AI_PRIO_MIN;
                continue;
            }

            //この場所の優先度を代入
            tempGrid = (tempX + (tempY * Field.FIELD_GRID_X));
            prioTable[i] = calcGrid[tempGrid];
        }


        //優先度のテーブルをデバッグ出力
        {
            //デバッグ用文字列
            string temp = "";

            //優先度テーブルが作成できたらAIの周辺の優先度を取得
            temp += "PRIO TABLE\n";
            for (tempY = 0; tempY < Field.FIELD_GRID_Y; tempY++)
            {
                for (tempX = 0; tempX < Field.FIELD_GRID_X; tempX++)
                {

                    //Y軸は上下逆に出力されてしまうので逆さまにする
                    temp += "\t\t" + calcGrid[tempX + ((Field.FIELD_GRID_Y - 1 - tempY) * Field.FIELD_GRID_X)] + "";

                    //自分の位置
                    if ((aiX == tempX) && (aiY == (Field.FIELD_GRID_Y - 1 - tempY)))
                    {
                        temp += "*";
                    }
                }
                temp += "\n";
            }
            temp += "\n";

            //移動方向別の優先度情報
            temp += "RESULT\n";
            for (i = 0; i < (int)CheckDir.EnumMax; i++)
            {
                //この場所の優先度を代入
                temp += "\t" + prioTable[i] + "\t" + (CheckDir)i + "\n";
            }

            //出力
            //Debug.Log("" + temp);
        }


        //4方向の優先度情報を返す
        return prioTable;
    }





}
