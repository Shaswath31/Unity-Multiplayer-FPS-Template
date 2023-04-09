
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using LootLocker.Requests;

public class FetchLeaderBoard : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerNames, playerScores;

    // Update is called once per frame
    void Start()
    {
        StartCoroutine(FetchTopScores());
    }

    public IEnumerator FetchTopScores()
    {
        bool done = false;
        LootLockerSDKManager.GetScoreList("killCount", 20, 0, (response) =>
            {
                if (response.success)
                {
                    string tempPlayerNames = "Names\n";
                    string tempPlayerScores = "Kills\n";

                    LootLockerLeaderboardMember[] members = response.items;

                    for (int i=0;i<members.Length;i++)
                    {
                        tempPlayerNames += members[i].rank + ".";
                        if (members[i].player.name != "")
                        {
                            tempPlayerNames += members[i].player.name;
                        }
                        else
                        {
                            tempPlayerNames += members[i].player.id;
                        }
                        tempPlayerScores+= members[i].score+"\n";
                        tempPlayerNames += "\n";
                    }
                    done = true;
                    playerNames.text = tempPlayerNames;
                    playerScores.text = tempPlayerScores;

                }
                else
                {
                    playerNames.text = "Error in retreiving Leaderboard";
                    Debug.Log(response.Error);
                    done = true;
                }
            });
        yield return new WaitWhile(()=> done==true);
    }

    public void onFetch()
    {
        StartCoroutine(FetchTopScores());
    }
}
