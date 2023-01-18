using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public void Shoot(bool toRight)
    {
        transform.LeanMoveLocalX(toRight ? 12 : -12, 0.4f).setOnComplete(() => Destroy(gameObject, 0.1f));
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Destroy(gameObject);
    }
}
