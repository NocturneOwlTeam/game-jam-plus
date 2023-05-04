using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{


    public GameObject BulletPrefabs;
    public Transform poinFire;

    private void Update()
    {
      /*  if (Input.GetKeyDown(KeyCode.Space))
        {
            FireProjectile();
        }*/



        if (Input.GetKeyDown(KeyCode.Space))
        {
            Invoke("PlayerShooting", 0f);
        }
    } 

    public void PlayerShooting()
    {
        Instantiate(BulletPrefabs, poinFire.position, poinFire.rotation);
    }
}
