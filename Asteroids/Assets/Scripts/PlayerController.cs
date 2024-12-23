using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool thrusting;
    public float thrustingSpeed = 1.0f;

    private float turnDirection;
    public float turnSpeed = 1.0f;

    public BulletController bulletPrefab;

    public Rigidbody2D rb2D;

    private void Awake()
    {
        //Se ejecuta una vez en el script
        rb2D = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        //Detectar el movimiento del jugador
       thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            turnDirection = 1.0f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            turnDirection = -1.0f; 
        }
        else { turnDirection = 0f; }
        if (Input.GetKeyDown(KeyCode.Space)||Input.GetKeyDown(KeyCode.Mouse0)){
            Shoot();
        }
        
    }

    //FixedUpdate Funciona a unos frames fijos
    private void FixedUpdate()
    {
        if (thrusting)
        {
            rb2D.AddForce(this.transform.up * this.thrustingSpeed);
        }
        if (turnDirection != 0f)
        {
            rb2D.AddTorque(turnDirection * this.turnSpeed);
        }
    }
    private void Shoot()
    {
        BulletController bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
        bullet.Project(this.transform.up);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid") { 
            rb2D.velocity = Vector3.zero;
            rb2D.angularVelocity = 0.0f;

            this.gameObject.SetActive(false);
            FindObjectOfType<GameManager>().PlayerDied();
        }
    }
}
