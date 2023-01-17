using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        if (!rb2d) rb2d = GetComponent<Rigidbody2D>();
        rb2d.AddForce(Vector2.left * 2, ForceMode2D.Impulse);
    }

    // Update is called once per fram
}
