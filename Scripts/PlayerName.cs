
using TMPro;
using UnityEngine;
using LootLocker.Requests;
using UnityEngine.Events;
using System.Collections;

public class PlayerName : MonoBehaviour
{
    public TMP_InputField playerName;
    public GameObject MainMenu;
    public UnityEvent Set;
    [SerializeField] AudioClip UI_click;

    
    public void SetPlayerName()
    {   if (playerName.text == string.Empty)
        {
            playerName.text = "Player Name cannot be empty";
        }
        else
        {
            PlayerData.name = playerName.text;
            StartCoroutine(SendPlayerName());
        }

    }

    public void OnClick(GameObject button)
    {
        LeanTween.scale(button, new Vector3(1.1f, 1.1f, 1.1f), 0.2f).setEase(LeanTweenType.easeOutBack);
        LeanAudio.play(UI_click);
        LeanTween.scale(button, new Vector3(1f, 1f, 1f), 0.1f).setEase(LeanTweenType.easeOutBack).setDelay(0.2f);
    }

    IEnumerator SendPlayerName()
        {   
            bool done = false;
            LootLockerSDKManager.SetPlayerName(playerName.text, (response) =>
            {
                if (response.success)
                {
                    Debug.Log("Successfully set Player Name");
                    //MainMenu.GetComponent<NetworkTestScript>().playerName = playerName.text;
                    gameObject.SetActive(false);
                    MainMenu.SetActive(true);
                }
                else
                {
                    Debug.Log("Could not set Player Name");
                }
            });

            PlayerData.coins = "200";
            PlayerData.rank = "1";
            PlayerData.accuracy = "100";
            PlayerData.kills = "0";
            LootLockerGetPersistentStorageRequest data = new LootLockerGetPersistentStorageRequest();
            data.AddToPayload(new LootLockerPayload { key = "wallet", value = "200", order = 0, is_public = true });
            data.AddToPayload(new LootLockerPayload { key = "rank", value = "1", order = 1, is_public = true });
            data.AddToPayload(new LootLockerPayload { key = "kills", value = "0", order = 2, is_public = true });
            data.AddToPayload(new LootLockerPayload { key = "accuracy", value = "100", order = 3, is_public = true });

        LootLockerSDKManager.UpdateOrCreateKeyValue(data, (getPersistentStoragResponse) =>
            {
                if (getPersistentStoragResponse.success)
                {
                    Debug.Log("Successfully updated player storage");
                    done= true;
                }
                else
                {
                    Debug.Log("Error updating player storage");
                    done = true;
                }
            });
            Set.Invoke();
            yield return new WaitWhile(() => done == true);
    }
}

