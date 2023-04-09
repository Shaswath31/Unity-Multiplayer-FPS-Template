using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayInfo : MonoBehaviour
{
    void OnEnable()
    {
        Player.OnPlayerInfo += NameUpdater;
    }

    private void OnDisable()
    {
        Player.OnPlayerInfo -= NameUpdater;
    }
    // Start is called before the first frame update
    public void NameUpdater()
    {
        Debug.Log("Updating name");
        gameObject.GetComponent<TextMeshProUGUI>().text = $"{PlayerData.name} \n  coins: {PlayerData.coins} \n rank: {PlayerData.rank}\n accuracy: {PlayerData.accuracy}\n kills: {PlayerData.kills} ";
    }


}
