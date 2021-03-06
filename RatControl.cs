﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatControl : MonoBehaviour
{
    public float hp = 25;
    public float damage = 0;
    public float defense = 0;
    public static int attack = 2;
    public GameObject Sword;
    public Vector3 beat;
    public int moveflag = 0;
    public int dropflag = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("ChangeMovement");
    }

    // Update is called once per frame
    void Update()
    {
        Die();
        Move();
        if (transform.position.x < -36)
        {
            moveflag = 2;
        }
        else if (transform.position.x > 30)
        {
            moveflag = 1;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Attack")
        {
            damage = PlayerControl.attack - defense;
            hp -= damage;
            Debug.Log("Damage : " + (PlayerControl.attack - defense));
            Debug.Log("HP : " + hp);
            if (transform.position.x < collision.gameObject.transform.position.x)
            {
                transform.GetComponent<Rigidbody2D>().AddForce(Vector3.left, ForceMode2D.Impulse);
            }
            else
            {
                transform.GetComponent<Rigidbody2D>().AddForce(Vector3.right, ForceMode2D.Impulse);
            }
        }
    }
    void Die()
    {
        if (hp <= 0)
        {
            dropflag = Random.Range(0, 5);
            Debug.Log(dropflag);
            GameManager.gold += 85;
            Debug.Log("gold : " + GameManager.gold);
            PlayerControl.inexp += 9;
            Debug.Log("inexp : " + PlayerControl.inexp);
            PlayerControl.kill_count++;
            GameManager.stage_flag--;
            if (dropflag == 0)
            {
                Instantiate(Sword, this.transform.position, Quaternion.identity);
            }
            Destroy(this.gameObject);
        }
    }
    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;
        if (moveflag == 1)
        {
            moveVelocity = Vector3.left;
            if (this.transform.GetComponent<SpriteRenderer>().flipX == true)
            {
                this.transform.GetComponent<SpriteRenderer>().flipX = false;
            }

        }
        else if (moveflag == 2)
        {
            moveVelocity = Vector3.right;

            if (this.transform.GetComponent<SpriteRenderer>().flipX == false)
            {
                this.transform.GetComponent<SpriteRenderer>().flipX = true;
            }

        }
        transform.position += moveVelocity * 2.0f * Time.deltaTime;

    }
    IEnumerator ChangeMovement()
    {
        moveflag = Random.Range(0, 3);

        yield return new WaitForSeconds(3.0f);
        StartCoroutine("ChangeMovement");
    }
}
