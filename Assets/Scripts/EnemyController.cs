using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    GameManager _gameManager;
    GameObject _player;

    float _health = 100f;
    float _moveSpeed = 2f;
    Quaternion _targeRotation;
    bool _disableEnemy = false;
    Vector2 _moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!_gameManager._gameOver && !_disableEnemy)
        {
            MoveEnemy();
            RotateEnemy();
        }
    }

    void MoveEnemy()
    {
        transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _moveSpeed * Time.deltaTime);
    }

    void RotateEnemy()
    {
        _moveDirection = _player.transform.position - transform.position;
        _moveDirection.Normalize();

        _targeRotation = Quaternion.LookRotation(Vector3.forward, _moveDirection);

        //Facem acest check ca sa nu mai rulam codul de rotatie in cazul in care deja inamicul este rotit catre player
        if (transform.rotation != _targeRotation)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _targeRotation, 200 * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            StartCoroutine(Damaged());

            _health -= 40f;

            if (_health <= 0f)
            {
                Destroy(gameObject);
                _gameManager.LogEnemyKilledCounter();
            }

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Player")
        {
            _gameManager.GameOver();
            collision.gameObject.SetActive(false);
        }
    }

    IEnumerator Damaged()
    {
        _disableEnemy = true;
        yield return new WaitForSeconds(0.5f);
        _disableEnemy = false;
    }
}
