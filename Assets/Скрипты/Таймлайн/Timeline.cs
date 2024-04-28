using UnityEngine;
using UnityEngine.Playables;

public class Timeline : MonoBehaviour
{
    [HideInInspector] public PlayableDirector director;

    [Header("StopWork")]
    public PointView pointView;
    public Animator menuButtons;
    public PlayerMovement player;
    public Inventory inventory;
    public HintControl[] hintControls;

    [HideInInspector] public bool eternalCutcene;

    void Awake()
    {
        director = gameObject.GetComponent<PlayableDirector>();
        director.stopped += Stopped;
    }

    void OnDisable()
    {
        director.stopped -= Stopped;
    }

    void Stopped(PlayableDirector director)
    {
        if (!eternalCutcene)
        {
            if (menuButtons) menuButtons.SetBool("show", true);
            if (player) player.SwitchStateWork();
            if (inventory) inventory.SwitchStateWork();
            foreach (HintControl hint in hintControls) hint.SwitchStateWork();
        }
    }

    public void PlayCutscene(PlayableAsset clip)
    {
        director.Play(clip);
        if (eternalCutcene)
        {
            eternalCutcene = false;
            return;
        }

        if (pointView) pointView.StopWork();
        if (menuButtons) menuButtons.SetBool("show", false);
        if (player) player.SwitchStateWork();
        if (inventory) inventory.SwitchStateWork();
        foreach (HintControl hint in hintControls) hint.SwitchStateWork();

    }

    public void PlayEternalCutscene(PlayableAsset clip)
    {
        director.Play(clip);
        eternalCutcene = true;

        if (pointView) pointView.StopWork();
        if (menuButtons) menuButtons.SetBool("show", false);
        if (player) player.SwitchStateWork();
        if (inventory) inventory.SwitchStateWork();
        foreach (HintControl hint in hintControls) hint.SwitchStateWork();
    }
}