using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Photon.Pun;

public class weapon : MonoBehaviour
{
    public int damage;
    public Camera camera;
    
  

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Fire();
        }
        
    }

    void Fire()
    {
        LeanTween.moveLocalZ(gameObject, 0.2f, 0.2f).setEase(LeanTweenType.easeOutQuad).setOnComplete(()=> LeanTween.moveLocalZ(gameObject, 0.58f, 0.2f).setEase(LeanTweenType.easeInCubic));
        Ray ray = new Ray(camera.transform.position,camera.transform.forward);

        RaycastHit hit;

        if (Physics.Raycast(ray.origin,ray.direction,out hit, 100f))
        {
            if (hit.transform.gameObject.GetComponent<Health>())
            {
                hit.transform.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, damage);
            }
        }
    }
}
