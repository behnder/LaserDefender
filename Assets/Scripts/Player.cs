using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Configuration parameters
    [Header("Player")]
    [SerializeField] private float movespeed = 10;
    [SerializeField] private float paddingScale = 0.04f;
    [SerializeField] private int health = 200;
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplotion = 1f;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float laserSpeed = 10f;
    [SerializeField] float projectileFirePeriod = 0.3f;

    [Header("Audio")]
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.9f;

    [SerializeField] AudioClip laserSound;
    [SerializeField] [Range(0, 1)] float laserSoundVolume = 0.9f;

    Coroutine firingCoroutine;

    public CameraShake cameraShake;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    private void Start()
    {
        SetUpMoveBoundaries();

    }

    void Update()
    {
        Move();
        Fire();
        //StartCoroutine(WaitForFire());

    }
    public int GetHealth()
    {
        return health;
    }
    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());

        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }
    private IEnumerator FireContinuously()
    {

        while (true)
        {
            AudioSource.PlayClipAtPoint(laserSound, Camera.main.transform.position, laserSoundVolume);

            GameObject laser = Instantiate(
                laserPrefab,
                transform.position + new Vector3(0,1,0),
                Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
            yield return new WaitForSeconds(projectileFirePeriod);

        }
    }


    private void Move()
    {
        float newXPos = HorizontalMove();
        float newYPos = VerticalMove();
        transform.position = new Vector2(newXPos, newYPos);

    }

    private float VerticalMove()
    {
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * movespeed;
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        return newYPos;
    }
    private float HorizontalMove()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * movespeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);

        return newXPos;
    }
    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;

        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer)
        {
            return;
        }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {

        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        health = 0;
        cameraShake.Shake();
        FindObjectOfType<Level>().LoadGameOver();
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, durationOfExplotion);
        Destroy(gameObject);
    }
}
