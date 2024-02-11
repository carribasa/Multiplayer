using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHandler : MonoBehaviour
{
    [SerializeField] private Transform shootHandler;
    [SerializeField] private GameObject bullet;

    void Update()
    {
        
    }

    private void Shoot()
    {
        Destroy(Instantiate(bullet, shootHandler.position, shootHandler.rotation), 3f);
    }


}
