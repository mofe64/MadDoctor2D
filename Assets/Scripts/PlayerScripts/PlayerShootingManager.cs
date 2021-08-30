using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingManager : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPreFab; //initialzie via unity inspector
    [SerializeField]
    private Transform bulletSpawnPosition;// initialize via unity inspector

    public void Shoot(float facingDirection)
    {
        GameObject newBullet = Instantiate(bulletPreFab, bulletSpawnPosition.position, Quaternion.identity);//Quarternions are used to represent rotations, identity rotation is read only
        if (facingDirection < 0)
        {
            newBullet.GetComponent<Bullet>().SetNegativeSpeed();
        }
    }
}
