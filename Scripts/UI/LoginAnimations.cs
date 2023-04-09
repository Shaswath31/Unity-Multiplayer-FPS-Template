using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.Events;

public class LoginAnimations : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject LoginScreen, SignupScreen;
    [SerializeField] AudioClip UI_click;
    

    public void clickAnimation(GameObject button)
    {
        LeanTween.scale(button, new Vector3(1.1f, 1.1f, 1.1f), 0.6f).setEase(LeanTweenType.easeOutBack);
        LeanAudio.play(UI_click);
        LeanTween.scale(button, new Vector3(1f, 1f, 1f), 0.6f).setEase(LeanTweenType.easeOutBack).setDelay(0.7f);
        switch (button.name)
        {
            case "CreateAccount":
                if (LoginScreen.activeInHierarchy){
                    LoginScreen.SetActive(false);
                    SignupScreen.SetActive(true);
                }
                else
                {
                    LoginScreen.SetActive(true);
                    SignupScreen.SetActive(false);
                }

                break;
        }



    }

}
