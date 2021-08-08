using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Janken : MonoBehaviour
{
    bool flagJanken = false; // 묵찌빠 시작 플래그
    int modeJanken = 0;

    public AudioClip voiceStart;
    public AudioClip voicePon;
    public AudioClip voiceGoo;
    public AudioClip voiceChoki;
    public AudioClip voicePar;
    public AudioClip voiceWin;
    public AudioClip voiceLoose;
    public AudioClip voiceDraw;

    const int JANKEN = -1;
    const int GOO = 0;
    const int CHOKI = 1;
    const int PAR = 2;
    const int DRAW = 3;
    const int WIN = 4;
    const int LOOSE = 5;

    private Animator animator;
    private AudioSource univoice;

    int myHand;
    int unityHand;
    int flagResult;
    int[,] tableResult = new int[3, 3];
    float waitDelay;

    public GUIStyle guiBtnGame;
    public GUIStyle guiBtnGoo;
    public GUIStyle guiBtnChoki;
    public GUIStyle guiBtnPar;

    private Rect rtbtnGame = new Rect();
    private Rect rtbtnGoo = new Rect();
    private Rect rtBtnChoki = new Rect();
    private Rect rtBtnPar = new Rect();

    private void OnGUI()
    {
        // GUI 크기 : 16:9의 1280 x 720 해상도 기준
        const float guiScreen = 1280;
        const float guiPadding = 10; // gui와 테두리와의 간격
        const float guiButton = 200;
        const float guiTop = 720 - guiButton - guiPadding;

        //현재 화면과의 비율
        float gui_scale = Screen.width / guiScreen;
        float scaledPadding = guiPadding * gui_scale;
        float scaledButton = guiButton * gui_scale;
        float scaledTop = guiTop * gui_scale;

        // 버튼들 위치 조정
        rtbtnGame.x = scaledPadding;
        rtbtnGame.y = scaledTop;
        rtbtnGame.width = scaledButton;
        rtbtnGame.height = scaledButton;

        float left = (guiScreen - guiPadding * 2 - guiButton * 3) / 2 * gui_scale;

        rtbtnGoo.x = left;
        rtbtnGoo.y = scaledTop;
        rtbtnGoo.width = scaledButton;
        rtbtnGoo.height = scaledButton;

        left += scaledButton + scaledPadding;

        rtBtnChoki.x = left;
        rtBtnChoki.y = scaledTop;
        rtBtnChoki.width = scaledButton;
        rtBtnChoki.height = scaledButton;

        left += scaledButton + scaledPadding;

        rtBtnPar.x = left;
        rtBtnPar.y = scaledTop;
        rtBtnPar.width = scaledButton;
        rtBtnPar.height = scaledButton;

        //묵찌빠가 아니면
        if (!flagJanken)
        {
            // UI에 게임 버튼 추가
            flagJanken = GUI.Button(rtbtnGame, "묵찌빠", guiBtnGame);
        }
        if(modeJanken == 1)
        {
            // UI에 게임 버튼 추가
            if(GUI.Button(rtbtnGoo, "묵", guiBtnGoo))
            {
                myHand = GOO;
                modeJanken++;
            }
            if (GUI.Button(rtBtnChoki, "찌", guiBtnChoki))
            {
                myHand = CHOKI;
                modeJanken++;
            }
            if (GUI.Button(rtBtnPar, "빠", guiBtnPar))
            {
                myHand = PAR;
                modeJanken++;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        univoice = GetComponent<AudioSource>();

        //결과 테이블 미리 결정[유니티짱, 플레이어]
        tableResult[GOO, GOO] = DRAW;
        tableResult[GOO, CHOKI] = DRAW;
        tableResult[GOO, PAR] = DRAW;
        tableResult[CHOKI, GOO] = DRAW;
        tableResult[CHOKI, CHOKI] = DRAW;
        tableResult[GOO, PAR] = DRAW;
        tableResult[PAR, GOO] = DRAW;
        tableResult[PAR, CHOKI] = DRAW;
        tableResult[PAR, PAR] = DRAW;
        
    }

    void UnityChanAction(int act)
    {
        switch (act)
        {
            case JANKEN:
                animator.SetBool("Janken", true);
                univoice.clip = voiceStart;
                break;
            case GOO:
                animator.SetBool("Goo", true);
                univoice.clip = voiceGoo;
                break;
            case CHOKI:
                animator.SetBool("Choki", true);
                univoice.clip = voiceChoki;
                break;
            case PAR:
                animator.SetBool("Par", true);
                univoice.clip = voicePar;
                break;
            case DRAW:
                animator.SetBool("Goo", true);
                univoice.clip = voiceDraw;
                break;
            case WIN:
                animator.SetBool("Win", true);
                univoice.clip = voiceWin;
                break;
            case LOOSE:
                animator.SetBool("Win", true);
                univoice.clip = voiceLoose;
                break;
        }
        univoice.Play();
    }
    // Update is called once per frame
    void Update()
    {
        if(flagJanken)
        {
            //게임모드에 따라
            switch(modeJanken)
            {
                case 0://묵찌빠 시작
                    UnityChanAction(JANKEN);
                    modeJanken++;
                    break;
                case 1://플레이어 입력 대기
                    animator.SetBool("Janken", false);
                    animator.SetBool("Aiko", false);
                    animator.SetBool("Goo", false);
                    animator.SetBool("choki", false);
                    animator.SetBool("Par", false);
                    animator.SetBool("Win", false);
                    animator.SetBool("Loose", false);
                    break;
                case 2: // 판정
                    flagResult = JANKEN;
                    //유니티짱의 손을 무작위로 선택
                    unityHand = Random.Range(GOO, PAR + 1);
                    //유니티짱 액션
                    UnityChanAction(unityHand);
                    //결과
                    flagResult = tableResult[unityHand, myHand];
                    modeJanken++;
                    break;
                case 3: //판정 대기
                    waitDelay += Time.deltaTime;
                    if(waitDelay > 1.5f)
                    {
                        UnityChanAction(flagResult);
                        waitDelay = 0;
                        modeJanken++;
                    }
                    break;
                default:
                    flagJanken = false;
                    modeJanken = 0;
                    break;
            }
        }
    }
}
