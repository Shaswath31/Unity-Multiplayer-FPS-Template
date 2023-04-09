using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.Events;

public class MainMenuAnimations : MonoBehaviour
{
    [SerializeField] AudioClip UI_click;
    [SerializeField] GameObject Play, Quit,Player,MainCamera,Leaderboard;

    [SerializeField] AudioSource Maintrack;
    public UnityEvent Connect;

    private void OnDisable()
    {

        if (Maintrack != null )
        {
            Maintrack.Pause();
        }
        
        
    }

    private void OnEnable()
    {
        Maintrack.Play();
    }
    private void Start()
    {

        Maintrack.Play();
    }

    public void clickAnimation(GameObject button)
    {
        LeanTween.scale(button, new Vector3(1.1f, 1.1f, 1.1f), 0.2f).setEase(LeanTweenType.easeOutBack).setIgnoreTimeScale(true);
        LeanAudio.play(UI_click);
        LeanTween.scale(button, new Vector3(1f, 1f, 1f), 0.1f).setEase(LeanTweenType.easeOutBack).setDelay(0.2f).setIgnoreTimeScale(true);
        
        switch (button.name)
        {
            case "ButtonPlay":
                Player.SetActive(true);
                MainCamera.SetActive(false);
                this.gameObject.SetActive(false);
                FPSController.canMove =true;
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Connect.Invoke();
                break;

            case "LeaderBoardEnable":
                if (Leaderboard.activeInHierarchy)
                {
                    LeanTween.moveLocalX(Leaderboard, 878, 2).setEaseInOutBack().setOnComplete(() => Leaderboard.SetActive(false)).setIgnoreTimeScale(true);
                    Debug.Log("Hid Leaderboard");
                }
                else
                {
                    Leaderboard.SetActive(true);
                    LeanTween.moveLocalX(Leaderboard, 0, 2).setEaseInOutBack().setIgnoreTimeScale(true);
                    Debug.Log("Show Leaderboard");

                }
                break;

            case "ButtonQuit":
                Application.Quit();
                break;




        }
    }
}
