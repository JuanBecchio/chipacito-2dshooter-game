using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private GameObject owner;
    public void Shoot(bool toRight, GameObject _owner = null)
    {
        if (_owner) owner = _owner;
        transform.LeanMoveLocalX(toRight ? 12 : -12, 0.4f).setOnComplete(() => Destroy(gameObject, 0.1f));
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (owner && col.gameObject == owner) return;
        Destroy(gameObject);
    }
}
