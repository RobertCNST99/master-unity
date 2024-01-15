using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Wall : MonoBehaviour
{
   /** TilemapCollider2D _collider;

    void Start()
    {
        _collider = GetComponent<TilemapCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Enemy")
        {
            UnityEngine.Debug.Log("colision");
            StartCoroutine(WaitForCollider());
        }
        else if (collision.gameObject.tag == "Player")
        {
            _collider.enabled = true;
        }
    }

    IEnumerator WaitForCollider()
    {
        _collider.enabled = false;
        yield return new WaitForSeconds(3);
        _collider.enabled = true;
    }
   */
}
