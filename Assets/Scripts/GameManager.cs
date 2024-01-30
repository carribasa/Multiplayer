using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    void Start()
    {
       if(PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate("PlayerA", new Vector2(-8, -0.4f), Quaternion.identity); // Instanciamos al PlayerA en la escena si es MasterClient
        } else
        {
            PhotonNetwork.Instantiate("PlayerB", new Vector2(8, -0.4f), Quaternion.identity); // Instanciamos al que NO es MasterClient PlayerB en la escena
        }
    }

    void Update()
    {
        
    }
}
