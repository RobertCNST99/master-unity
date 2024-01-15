using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameManager _gameManager;
    Rigidbody2D _rb;
    Camera _mainCamera;

    //Player movement settings
    float _moveVertical;
    float _moveHorizontal;
    [SerializeField] float _moveSpeed = 5f;
    [SerializeField] float _speedLimiter = 0.7f;
    Vector2 _moveVelocity;

    //Mouse settings
    Vector2 _mousePosition;
    Vector2 _mouseOffset;

    //Bullet settings
    [SerializeField] GameObject _bullet;
    [SerializeField] GameObject _bulletSpawn;
    bool _isShooting = false;
    float _bulletSpeed = 15f;

    // Start is called before the first frame update
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        _moveHorizontal = Input.GetAxisRaw("Horizontal");
        _moveVertical = Input.GetAxisRaw("Vertical");
        _moveVelocity = new Vector2(_moveHorizontal, _moveVertical) * _moveSpeed;

        if (Input.GetMouseButtonDown(0))
        {
            _isShooting = true;
        }
    }

    void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();

        if (_isShooting)
        {
            StartCoroutine(Fire());
        }
    }

    void MovePlayer()
    {
        if (_moveHorizontal != 0 || _moveVertical != 0)
        {
            if (_moveHorizontal != 0 && _moveVertical != 0)
            {
                _moveVelocity *= _speedLimiter;
            }         
        } 
        else
        {
            _moveVelocity = new Vector2(0f, 0f);       
        }

        _rb.velocity = _moveVelocity;
    }

    void RotatePlayer()
    {
        _mousePosition = Input.mousePosition;

        //Locatia pe ecran a cursorului
        Vector3 screenPoint = _mainCamera.WorldToScreenPoint(transform.localPosition);

        /*
         * Offset-ul este distanta dintre cursor si player
         * Acel normalized returneaza pozitia sub forma de procentaj (0 min, max 1), altfel intorcea pozitia in pixeli
         * Si cand se apela metoda Fire() glontul avea viteza diferita in functie de pozitia cursorului, .normalized ne salveaza de asta 
        */
        _mouseOffset = new Vector2(_mousePosition.x - screenPoint.x, _mousePosition.y -screenPoint.y).normalized;

        float angle = Mathf.Atan2(_mouseOffset.y, _mouseOffset.x) * Mathf.Rad2Deg;

        //Adaugam -90 grade deoarece este orientat spre stanga by default
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
    }

    IEnumerator Fire()
    {
        _isShooting = false;

        //Al 3-lea parametru se refera la rotatie si aici am spus ca nu e nevoie de rotatie
        GameObject bullet = Instantiate(_bullet, _bulletSpawn.transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = _mouseOffset * _bulletSpeed;

        yield return new WaitForSeconds(3);
        Destroy(bullet);
    }
}
