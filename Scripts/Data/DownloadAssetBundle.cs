using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;

public class DownloadAssetBundle : MonoBehaviour
{
    public UnityEvent Levelloaded;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DownloadAssets());
    }

    private IEnumerator DownloadAssets()
    {
        GameObject placeholder= null;
        string url = "https://drive.google.com/uc?export=download&id=1v2XzyWFhLDRoxxSFmoJ4covcP8lMBOvl";
        using (UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(url))
        {
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.ConnectionError || www.result ==UnityWebRequest.Result.ProtocolError) 
            {
                Debug.LogWarning("Error on the get request at " + "url" + " " + www.error);
            }
            else
            {
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
                placeholder = bundle.LoadAsset(bundle.GetAllAssetNames()[0]) as GameObject;
                bundle.Unload(false);
                yield return new WaitForEndOfFrame();
            }
            www.Dispose();
        }
        InstantiateGameObject(placeholder);
    }

    private void InstantiateGameObject(GameObject go)
    {
        if (go != null)
        {
               GameObject instanceGo = Instantiate(go);
            instanceGo.transform.position = Vector3.zero;
            Levelloaded.Invoke();
        }
        else
        {
            Debug.LogWarning("Asset bundle is null");
        }   
    }
}
