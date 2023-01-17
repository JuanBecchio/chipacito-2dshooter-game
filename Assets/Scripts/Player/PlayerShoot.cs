using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletOut;

    private PlayerMovement movementScript;

    void Start()
    {
        movementScript = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire"))
        {
            Vector3 bPos = bulletOut.position;
            if (!movementScript.lookR)
                bPos.x = transform.position.x - bulletOut.localPosition.x;
            GameObject bullet = Instantiate(bulletPrefab, bPos, Quaternion.identity);
        }

    }
}
