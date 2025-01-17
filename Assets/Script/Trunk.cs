using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trunk : MonoBehaviour
{
    public float speed = 10.0f;
    Rigidbody2D rb;
    public GameObject invisbleWall;
    Vector3 test;

    public GameObject Score;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-speed, 0);
        Score = GameObject.FindGameObjectWithTag("Points");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "InvisibleWall")
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D colli)
    {
        if(colli.gameObject.name == "Samurai")
        {
            Destroy(this.gameObject);
            Score.GetComponent<Points>().SavePoints();

        }

        if(colli.gameObject.name == "AttackBox")
        {
            Destroy(this.gameObject);
        }
    }
}
