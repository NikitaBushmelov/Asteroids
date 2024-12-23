using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public Sprite[] sprites;
    public float size,maxSize,minSize;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _body2D;
    public float speed = 50.0f;
    public float maxLifeTime = 20.0f;


    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _body2D = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer.sprite = sprites[Random.Range(0,sprites.Length)];
        this.transform.eulerAngles = new Vector3(0.0f,0.0f,Random.value * 360.0f);
        this.transform.localScale = Vector3.one * this.size;
        _body2D.mass = this.size;
    }

    public void SetTrajectory(Vector2 direction)
    {
        _body2D.AddForce(direction * this.speed);
        Destroy(this.gameObject, this.maxLifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if(this.size * 0.5 >= this.minSize)
            {
                CreateSplit();
                CreateSplit();
            }
            FindObjectOfType<GameManager>().AsteroidDestroyed(this);
            Destroy(this.gameObject);
        } 
    }
    private void CreateSplit()
    {
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.5f;

        AsteroidController half = Instantiate(this,position,this.transform.rotation);
        half.size = this.size*0.5f;
        half.SetTrajectory(Random.insideUnitCircle.normalized * this.speed);
    }
}
