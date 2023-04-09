using LootLocker.Requests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public delegate void PlayerInfo();
    public static event PlayerInfo OnPlayerInfo;

    private void Start()
    {
        StartCoroutine(FetchPlayerData());  

    }
    IEnumerator FetchPlayerData()
    {
        bool done = false;
        LootLockerSDKManager.GetEntirePersistentStorage((response) =>
        {
            if (response.success)
            {
                Debug.Log("successfully retrieved player storage: " + response.payload.Length);
                PlayerData.accuracy = response.payload[0].value;
                PlayerData.kills= response.payload[1].value;
                PlayerData.rank = response.payload[2].value;
                PlayerData.coins = response.payload[3].value;
                done = true;
            }
            else
            {
                Debug.Log("error getting player storage");
                done = false;
            }
        });
        yield return new WaitWhile(() => done == false);

        if (OnPlayerInfo != null)
        {
            OnPlayerInfo();
        }
    }

}
