using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    public FPSController controller;

    public GameObject camera;

    public CharacterController characterController;

    public void IsLocalPlayer()
    {
        controller.enabled = true;
        camera.SetActive(true);
        characterController.enabled = true;
    }
}
