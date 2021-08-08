using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Janken : MonoBehaviour
{
    bool flagJanken = false; // ����� ���� �÷���
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
        // GUI ũ�� : 16:9�� 1280 x 720 �ػ� ����
        const float guiScreen = 1280;
        const float guiPadding = 10; // gui�� �׵θ����� ����
        const float guiButton = 200;
        const float guiTop = 720 - guiButton - guiPadding;

        //���� ȭ����� ����
        float gui_scale = Screen.width / guiScreen;
        float scaledPadding = guiPadding * gui_scale;
        float scaledButton = guiButton * gui_scale;
        float scaledTop = guiTop * gui_scale;

        // ��ư�� ��ġ ����
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

        //������� �ƴϸ�
        if (!flagJanken)
        {
            // UI�� ���� ��ư �߰�
            flagJanken = GUI.Button(rtbtnGame, "�����", guiBtnGame);
        }
        if(modeJanken == 1)
        {
            // UI�� ���� ��ư �߰�
            if(GUI.Button(rtbtnGoo, "��", guiBtnGoo))
            {
                myHand = GOO;
                modeJanken++;
            }
            if (GUI.Button(rtBtnChoki, "��", guiBtnChoki))
            {
                myHand = CHOKI;
                modeJanken++;
            }
            if (GUI.Button(rtBtnPar, "��", guiBtnPar))
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

        //��� ���̺� �̸� ����[����Ƽ¯, �÷��̾�]
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
            //���Ӹ�忡 ����
            switch(modeJanken)
            {
                case 0://����� ����
                    UnityChanAction(JANKEN);
                    modeJanken++;
                    break;
                case 1://�÷��̾� �Է� ���
                    animator.SetBool("Janken", false);
                    animator.SetBool("Aiko", false);
                    animator.SetBool("Goo", false);
                    animator.SetBool("choki", false);
                    animator.SetBool("Par", false);
                    animator.SetBool("Win", false);
                    animator.SetBool("Loose", false);
                    break;
                case 2: // ����
                    flagResult = JANKEN;
                    //����Ƽ¯�� ���� �������� ����
                    unityHand = Random.Range(GOO, PAR + 1);
                    //����Ƽ¯ �׼�
                    UnityChanAction(unityHand);
                    //���
                    flagResult = tableResult[unityHand, myHand];
                    modeJanken++;
                    break;
                case 3: //���� ���
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
