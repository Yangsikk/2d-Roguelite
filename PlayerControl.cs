using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PlayerControl : MonoBehaviour
{
    public static float maxHp = 30;
    public static float hp = 30;
    public static float attack = 10;
    public static float attackspeed = 0.7f;
    public static int level = 5;
    public static int inlevel = 1;
    public static float reward = 0;
    public static int status = 6;
    public static bool inlevelupflag = false;
    public static int exp = 25;
    public static int kill_count = 0;
    public static float inexp = 20;
    public static bool dieflag = false;
    public float weaponstate = 0;
    public static bool isUnbeat = false;
    public static List<string> player_ability = new List<string>();
    public static float attackdelay = 0.7f;
    public Vector3 camera_position = new Vector3(-25.5f, 2.2f, -0.42f);
    public Vector3 Player_position;
    public bool stage1 = false;
    public bool stage2 = false;
    

    public float mspeed = 0.75f;
    public float jspeed = 3.5f;
    public float dspeed = 7.0f;
    public bool isjumping = false;

    bool isAttack = false;

    Rigidbody2D rigid;
    public SpriteRenderer renderer;
    public List<GameObject> bullets;

    public static Vector3 target;
    Vector3 mouse;

    // Start is called before the first frame update
    void Start()
    {
        
        Player_position = this.transform.position;
        rigid = gameObject.GetComponent<Rigidbody2D>();
        renderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        mouse = Input.mousePosition;
        mouse.z = 0.8f;

        if (this.transform.position.x < mouse.x)
        {
            renderer.flipX = false;
        }
        else
        {
            renderer.flipX = true;
        }

        if(transform.position.x  < -36.7f)
        {
            this.transform.position = new Vector3(-36.7f, transform.position.y, 0.8f);
        }

        if(transform.position.x < -14f)
        {
            camera_position.x = -25.5f;
            Camera.main.transform.position = camera_position;
        }
        else if(transform.position.x > -14.0f && transform.position.x < 9.0f)
        {
            camera_position.x = -2.0f;
            Camera.main.transform.position = camera_position;
        }        
        else if(transform.position.x > 9.0f)
        {
            camera_position.x = 21.5f;
            Camera.main.transform.position = camera_position;
        }

        Move();
        Jump();
        Avoid();
        Attack();
        LevelUp();
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if(renderer.flipX == false)
            {
                renderer.flipX = true;
            }
            transform.position = new Vector3(transform.position.x - mspeed * Time.deltaTime, transform.position.y, transform.position.z);

        }
        if (Input.GetKey(KeyCode.D))
        {
            if(renderer.flipX == true)
            {
                renderer.flipX = false;
            }
            transform.position = new Vector3(transform.position.x + mspeed * Time.deltaTime, transform.position.y, transform.position.z);
        }
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (isjumping == false)
            {
                rigid.AddForce(Vector3.up * jspeed, ForceMode2D.Impulse);
                isjumping = true;
            }
        }
    }

    void Avoid()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mouse = Input.mousePosition;
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = 0.8f;

            this.transform.position = Vector3.MoveTowards(this.transform.position, target, Time.deltaTime * dspeed);
            Debug.Log(transform.position + " , " + target);
        }
    }
    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isAttack == false)
            {
                isAttack = true;
                Vector3 mouse = Input.mousePosition;
                target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                target.z = 0.8f;
                Instantiate(bullets[0], transform.position, Quaternion.identity);
                StartCoroutine(Wait());
            }
        }

    }
    
    void LevelUp()
    {
        if(inlevel == 1 && inexp >= 30)
        {
            inlevel++;
            inlevelupflag = true;
            Debug.Log(inlevel);
        }
        else if(inlevel ==2 && inexp >= 70)
        {
            inlevel++;
            inlevelupflag = true;
        }
        else if(inlevel == 3 && inexp >= 110)
        {
            inlevel++;
            inlevelupflag = true;
        }
        else if(inlevel == 4 && inexp >= 170)
        {
            inlevel++;
            inlevelupflag = true;
        }

        if(level == 1 && exp >= 50)
        {
            Debug.Log("1 : " + status);
            level++;
            status++;
            Debug.Log("2 : " + status);
        }
        else if (level == 2 && exp >= 120)
        {
            level++;
            status++;
        }
        else if (level == 3 && exp >= 200)
        {
            level++;
            status++;
        }
        else if (level == 4 && exp >= 290)
        {
            level++;
            status++;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isjumping = false;

        if (collision.gameObject.tag == "Enemy" && isUnbeat == false)
        {
            GameObject monster = collision.gameObject;
            Debug.Log(monster);
            Attacked(monster);
            Debug.Log(hp);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Finish")
        {
            hp = 0;
            dieflag = true;
            this.transform.position = new Vector2(this.transform.position.x, -5.0f);
        }
      
    }

    void Attacked(GameObject monster)
    {

        if(monster.transform.position.x > transform.position.x)
        {
            rigid.AddForce(Vector3.left * 3.0f, ForceMode2D.Impulse);
        }
        else
        {
            rigid.AddForce(Vector3.right * 3.0f, ForceMode2D.Impulse);
        }


        if(hp >= 1)
        {
            if(monster.name == "rabbit(Clone)")
            {
                hp -= RabbitControl.attack;              
            }
            else if(monster.name == "rat(Clone)")
            {
                hp -= RatControl.attack;
            }
            else if(monster.name == "bat(Clone)")
            {
                hp -= BatControl.attack;
            }
            else if(monster.name == "hog(Clone)")
            {
                hp -= HogControl.attack;
            }
            else if(monster.name == "Bear(Clone)")
            {
                hp -= BearControl.attack;
            }
            else if(monster.name == "Tiger(Clone")
            {
                hp -= TigerControl.attack;
            }
            Debug.Log(hp);
            isUnbeat = true;
            StartCoroutine("Unbeat");
        }
        if(hp <= 0)
        {
            hp = 0;
            dieflag = true;
        }
    }


    IEnumerator Unbeat()
    {
        int count = 0;
        while(count < 10)
        {
            if(count%2 == 0)
            {
                renderer.color = new Color32(255, 255, 255, 90);
            }
            else
            {
                renderer.color = new Color32(255, 255, 255, 180);
            }

            yield return new WaitForSeconds(0.2f);

            count++;
        }

        renderer.color = new Color32(255, 255, 255, 255);

        isUnbeat = false;

        yield return null;
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(attackdelay);
        isAttack = false;
    }
}
