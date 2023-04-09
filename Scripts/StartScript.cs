using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScript : MonoBehaviour
{   
    
    [SerializeField] GameObject Login, PlayScreen, MainMenu;

    void Start()
    { 

            Login.SetActive(true);
            MainMenu.SetActive(false);
            PlayScreen.SetActive(false);



    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            FPSController.canMove = !FPSController.canMove;
            PauseGame();

        }
    }      
void PauseGame()
    {
        if (FPSController.canMove)
        {
            Time.timeScale = 1;
            MainMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Time.timeScale = 0f;
            MainMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }


}
