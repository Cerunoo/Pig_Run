using UnityEngine;
using Cinemachine;
using System.Collections;
using UnityEngine.Playables;

public class TimelineScriptHandler : MonoBehaviour
{
    // General

    public enum SelectScene { Menu, House }
    public SelectScene selectScene;

    public Transform player;

    Timeline timeline;
    CinemachineVirtualCamera VCam;
    CinemachineFramingTransposer VCamFramingTransposer;

    void Start()
    {
        timeline = gameObject.GetComponent<Timeline>();
        VCam = FindObjectOfType<CinemachineVirtualCamera>();
        VCamFramingTransposer = VCam.GetCinemachineComponent<CinemachineFramingTransposer>();

        if (selectScene == SelectScene.Menu)
        {
            Start_Start();
        }
        else if (selectScene == SelectScene.House)
        {
            HouseTransition_Start();
        }
    }

    void OnEnable()
    {
        if (selectScene == SelectScene.Menu)
        {
            Start_OnEnable();
        }
        if (selectScene == SelectScene.House)
        {
            HouseTransition_OnEnable();
        }
    }

    void OnDisable()
    {
        if (selectScene == SelectScene.Menu)
        {
            Start_OnDisable();
        }
        if (selectScene == SelectScene.House)
        {
            HouseTransition_OnDisable();
        }
    }


    // Start

    public Animator animStartButton;

    public PlayableAsset startFromHouse;
    public Animator animStartFromHouseButton;

    public PlayableAsset playerExit;
    public PlayableAsset playerEnter;

    public HintControl enterEvent;
    public Transform pointHandle;

    public void Start_Start()
    {
        // if (TimelineDataTransfer.LeftHouse == true)
        if (TimelineDataTransfer.LeftHouse == true)
        {
            timeline.menuButtons.SetBool("leftHouse", true);
            timeline.PlayEternalCutscene(startFromHouse);
        }
    }

    public void Start_LoadScene()
    {
        animStartButton.SetBool("show", false);
        StartCoroutine(FindObjectOfType<AsyncLoading>().LoadAsync(2));
    }

    public void Start_PlayerExit()
    {
        animStartFromHouseButton.SetBool("show", false);

        VCam.Follow = player;
        VCamFramingTransposer.m_LookaheadTime = 1f;
        VCamFramingTransposer.m_LookaheadSmoothing = 0.25f;
        VCamFramingTransposer.m_LookaheadIgnoreY = true;

        timeline.PlayCutscene(playerExit);
        timeline.player.SwitchStateWork();
    }

    public void Start_PlayerEnter()
    {
        timeline.PlayEternalCutscene(playerEnter);

        if (player.position.x - pointHandle.position.x >= 0)
        {
            player.GetComponent<PlayerMovement>().DirectorLeftRun();
        }
        else
        {
            player.GetComponent<PlayerMovement>().DirectorRightRun();
        }
        StartCoroutine(timePlayerRun());
        IEnumerator timePlayerRun ()
        {
            yield return new WaitForSeconds(0.25f);
            player.GetComponent<PlayerMovement>().DirectorStopRun();
        }

        StartCoroutine(FindObjectOfType<AsyncLoading>().LoadAsync(2, true));
        timeline.director.stopped += waitEnd;
        void waitEnd(PlayableDirector director)
        {
            timeline.director.stopped -= waitEnd;
            FindObjectOfType<AsyncLoading>().StartScene();
        }
    }

    void Start_OnEnable()
    {
        enterEvent.KeycodeEventPressed += Start_PlayerEnter;
    }

    void Start_OnDisable()
    {
        enterEvent.KeycodeEventPressed -= Start_PlayerEnter;
    }


    // HouseTransition

    public PlayableAsset enterHouse;
    public PlayableAsset exitHouse;

    public HintControl exitEvent;

    public Vector2 playerStartPos;
    public Collider2D ignoreCollider;

    void HouseTransition_Start()
    {
        player.position = playerStartPos;
        ignoreCollider.enabled = false;

        timeline.director.stopped += waitEnd;
        void waitEnd(PlayableDirector director)
        {
            timeline.director.stopped -= waitEnd;
            ignoreCollider.enabled = true;
        }

        timeline.PlayCutscene(enterHouse);
    }

    void HouseTransition_LoadScene()
    {
        TimelineDataTransfer.LeftHouse = true;

        ignoreCollider.enabled = false;
        timeline.PlayEternalCutscene(exitHouse);

        StartCoroutine(FindObjectOfType<AsyncLoading>().LoadAsync(1, true));
        timeline.director.stopped += waitEnd;
        void waitEnd(PlayableDirector director)
        {
            timeline.director.stopped -= waitEnd;
            FindObjectOfType<AsyncLoading>().StartScene();
        }
    }

    void HouseTransition_OnEnable()
    {
        exitEvent.KeycodeEventPressed += HouseTransition_LoadScene;
    }

    void HouseTransition_OnDisable()
    {
        exitEvent.KeycodeEventPressed -= HouseTransition_LoadScene;
    }
}
