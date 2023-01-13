using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMovement : MonoBehaviour
{
    [SerializeField] float spinningSpeed=300;
    private float random = 1;

    private void Awake()
    {
        spinningSpeed = Random.Range(200, spinningSpeed);
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, spinningSpeed * Time.deltaTime);
    }
}
