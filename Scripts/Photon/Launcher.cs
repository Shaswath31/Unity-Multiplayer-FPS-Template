using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class Launcher : MonoBehaviourPunCallbacks
{
    public GameObject player;
    [Space]
    public Transform spawnPoint;
    public bool canInstance = false;
    public bool connected = false;
    public void EstablishConnecion()
    {
        UnityEngine.Debug.Log("Joined room");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        UnityEngine.Debug.Log("Joined room");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        PhotonNetwork.JoinOrCreateRoom("test", null,null);
        UnityEngine.Debug.Log("Joined room");
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        UnityEngine.Debug.Log("Joined room");
        connected= true;
        if(canInstance)
        {
            GameObject _player = PhotonNetwork.Instantiate(player.name, spawnPoint.position, Quaternion.identity);
            _player.GetComponent<PlayerSetup>().IsLocalPlayer();
        }
        else
        {
            Debug.Log("Level could not be downloaded, Please Retry");
        }


    }

   public void InstantiatePlayer()
   {
       canInstance= true;
        if (connected)
        {
            GameObject _player = PhotonNetwork.Instantiate(player.name, spawnPoint.position, Quaternion.identity);
            _player.GetComponent<PlayerSetup>().IsLocalPlayer();
        }
        else
        {
            Debug.Log("Level could not be downloaded, Please Retry");
        }
    }
}


