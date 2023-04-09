using UnityEngine;
using System;
using UnityEngine.UI;
using LootLocker.Requests;
using TMPro;

public class WhiteLabelLogin : MonoBehaviour
{
    // Input fields
    [Header("User")]
    public TMP_InputField UserEmailInputField;
    public TMP_InputField UserPasswordInputField;

    public TMP_InputField SignUp_UserEmailInputField;
    public TMP_InputField SignUp_UserPasswordInputField;
    public TMP_Text Name;

    [Header("Canvases")]
    public GameObject PlayerName;
    public GameObject Player;
    public GameObject LoginMenu;
    public GameObject MainMenu;




    [Header("Buttons")]
    public GameObject ResendVerify;
    public GameObject CreateAcc;

    public Text infoText;


    // Called when pressing "Log in"
    private void Start()
    {
        /*LootLockerSDKManager.StartWhiteLabelSession((startSessionResponse) =>
        {
            if (startSessionResponse.success)
            {
                // Session was succesfully started;
                // After this you can use LootLocker features
                infoText.text = "Session started successfully";
                LootLockerSDKManager.GetPlayerName((response) =>
                {
                    if (response.success)
                    {
                        if (String.IsNullOrEmpty(response.name))
                        {
                            LoginMenu.SetActive(false);
                            PlayerName.SetActive(true);
                        }
                        else
                        {
                            Debug.Log("Player Name received " + response.name);
                            LoginMenu.SetActive(false);
                            PlayerName.SetActive(false);
                            Player.SetActive(true);
                            PlayerData.name = response.name;
                            Name.text = response.name;
                            MainMenu.SetActive(true);
                           
                        }
                    }
                    else
                    {
                        Debug.Log("Servers Not available");
                    }
                });
            }
            else
            {
                // Error
                infoText.text = "Session Expired:" + startSessionResponse.Error+"Login Again";
            }
        });*/
    }
    public void Login()
    {
        string email = UserEmailInputField.text;
        string password = UserPasswordInputField.text;
        LootLockerSDKManager.WhiteLabelLogin(email, password, false, loginResponse =>
        {
            if (!loginResponse.success)
            {
                // Error
                infoText.text = "Error logging in:" + loginResponse.Error;
                return;
            }
            else
            {
                infoText.text = "Player was logged in succesfully";
            }

            // Is the account verified?
            if (loginResponse.VerifiedAt == null)
            {
                //infoText.text = "Please verify your email address";
            }

            // Player is logged in, now start a game session
            LootLockerSDKManager.StartWhiteLabelSession((startSessionResponse) =>
            {
                if (startSessionResponse.success)
                {
                    // Session was succesfully started;
                    // After this you can use LootLocker features
                    infoText.text = "Session started successfully";
                    LootLockerSDKManager.GetPlayerName((response) =>
                    {
                        if (response.success)
                        {   if (String.IsNullOrEmpty(response.name))
                            {   
                                LoginMenu.SetActive(false);
                                PlayerName.SetActive(true);
                            }
                            else
                            {
                                Debug.Log("Player Name received " + response.name);
                                LoginMenu.SetActive(false);
                                PlayerName.SetActive(false);
                                Player.SetActive(true);
                                PlayerData.name = response.name;
                                Name.text = response.name;
                                MainMenu.SetActive(true);
                               
                            }
                        }
                        else
                        {
                            Debug.Log("Servers Not available");
                        }
                    });
                }
                else
                {
                    // Error
                    infoText.text = "Error starting LootLocker session:" + startSessionResponse.Error;
                }
            });
        });
    }

    // Called when pressing "Create account"
    public void CreateAccount()
    {
        string email = SignUp_UserEmailInputField.text;
        string password = SignUp_UserPasswordInputField.text;

        LootLockerSDKManager.WhiteLabelSignUp(email, password, (response) =>
        {
            if (!response.success)
            {
                infoText.text = "Error signing up:" + response.Error;
                return;
            }
            else
            {
                // Succesful response
                infoText.text = "Account created,Please verify your email and login";
                CreateAcc.SetActive(false);
                ResendVerify.SetActive(true);



            }
        });
    }

    public void ResendVerificationEmail()
    {
        string email = UserEmailInputField.text;
        if(email== null)
        {
            email = SignUp_UserEmailInputField.text;
        }
        // Request a verification email to be sent to the registered user, 
        LootLockerSDKManager.WhiteLabelRequestVerification(email, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Verification email sent!");
            }
            else
            {
                Debug.Log("Error sending verification email:" + response.Error);
            }

        });
    }

    public void SendResetPassword()
    {
        // Sends a password reset-link to the email
        LootLockerSDKManager.WhiteLabelRequestPassword(UserEmailInputField.text, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Password reset link sent!");
            }
            else
            {
                Debug.Log("Error sending password-reset-link:" + response.Error);
            }
        });
    }
}
