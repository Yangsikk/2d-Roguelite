using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class GameManager : MonoBehaviour
{
    public GameObject Camera;
    public List<GameObject> Monsters;
    public List<GameObject> Spawn;
    public List<GameObject> Spawn2;
    public List<string> ability;
    public List<Vector3> position;
    public List<Vector3> position2;
    public List<Sprite> Item_sprite;
    public Text Level;
    public Text Hp;
    public Text Item_name;
    public Text Item_level;
    public Text Item_damage;
    public Text Status_point;
    public Text Hp_status;
    public Text Attack_status;
    public Text Attackdelay_status;
    public Text Gold_status;
    public Text Kill_text;
    public Text Gold_text;
    public Text Exp_text;
    public Image hp_image;
    public Image info;
    public Image Equip;
    public Image Item_Detail;
    public Image Item_Menu;
    public Image notice;
    public GameObject levelup_panel;
    public GameObject start_panel;
    public GameObject result_panel;
    public GameObject inventory;
    public GameObject Sword;
    public GameObject scarecrow;
    public Text ability_1_txt;
    public Text ability_2_txt;
    public Text ability_3_txt;
    public int ability1 = 0;
    public int ability2 = 0;
    public int ability3 = 0;
    public static int hp_status = 0;
    public static int attack_status = 0;
    public static int attackdelay_status = 0;
    public static int gold_status = 0;
    public static float Player_hp = 0;
    public static float Player_maxhp = 0;
    public static float Player_attack = 0;
    public static float Player_attackspeed = 0;
    public static float Player_attackdelay = 0;
    public Button ability1_Button;
    public Button ability2_Button;
    public Button ability3_Button;
    public Button Hp_up;
    public Button Hp_down;
    public Button Attack_up;
    public Button Attack_down;
    public Button Attackdelay_up;
    public Button Attackdelay_down;
    public Button Gold_up;
    public Button Gold_down;
    public Button Retry;
    public Button Return_main;
    public static int abilityFlag = 0;
    public static List<Item> Player_Item = new List<Item>();
    public List<GameObject> Inven = new List<GameObject>();
    public List<GameObject> Compose = new List<GameObject>();
    public static List<Item> Equip_Item = new List<Item>();
    public Camera camera;
    public string item_name;
    public float pre_gold;
    public static int stage_flag = 0;

    public Vector3 p1 = new Vector3(-20.45f, 0.04f, 0.8f);
    public Vector3 p2 = new Vector3(-32.13f, 1.53f, 0.8f);
    public Vector3 p3 = new Vector3(-26.33f, 3.63f, 0.8f);
    public Vector3 p4 = new Vector3(-28.94f, -1.49f, 0.8f);
    public Vector3 p5 = new Vector3(-11.74f, 4.54f, 0.8f);
    public Vector3 p6 = new Vector3(0.5f, 6.82f, 0.8f);
    public Vector3 p7 = new Vector3(3.88f, 3.84f, 0.8f);
    public Vector3 p8 = new Vector3(2.68f, -0.76f, 0.8f);
    public Vector3 p9 = new Vector3(27.44f, -1.12f, 0.8f);
    public Vector3 next = new Vector3(22.3f, -1.08f, 0.8f);

    public Vector3 p2_1 = new Vector3(-20.75f, 4.41f, 0.8f);
    public Vector3 p2_2 = new Vector3(18.25f, 1.5f, 0.8f);
    public Vector3 p2_3 = new Vector3(-2.44f, 6.0f, 0.8f);
    public Vector3 p2_4 = new Vector3(1.34f, -0.02f, 0.8f);
    public Vector3 p2_5 = new Vector3(21.5f, -0.69f, 0.8f);
    public Vector3 p2_6 = new Vector3(22.27f, 6.11f, 0.8f);

    public Canvas canvas;
    GraphicRaycaster raycaster;
    PointerEventData ped;
    Vector3 mouse;

    public static float gold;


    // Start is called before the first frame update
    void Start()
    {
        pre_gold = gold;
        SetMonsters();
        stage_flag = Spawn.Count + 1;
        Equip_Item.Add(CreateItem());
        Player_Item.Add(CreateItem());
        Player_Item.Add(CreateItem());
        Player_Item.Add(CreateItem());
        Player_Item.Add(CreateItem());

    }

    // Update is called once per frame
    void Update()
    {             
        Inventory();
        HpBar();
        LevelUp();
        Status_manage();
        Die();

        if (stage_flag == 0 && StageControl.next_stage == false)
        {
            Instantiate(scarecrow, next, Quaternion.identity);
            notice.gameObject.SetActive(true);
        }
    }

    void SetMonsters()
    {
        if (SceneManager.GetActiveScene().name != "MainScene")
        {
            notice.gameObject.SetActive(false);
            for (int i = 0; i < 8; i++)
            {
                Spawn.Add(Monsters[Random.Range(0, 3)]);
                Debug.Log(Spawn[i]);
            }
            

            position.Add(p1);
            position.Add(p2);
            position.Add(p3);
            position.Add(p4);
            position.Add(p5);
            position.Add(p6);
            position.Add(p7);
            position.Add(p8);
            position.Add(p9);

            for (int i = 0; i < 5; i++)
            {
                Spawn2.Add(Monsters[Random.Range(3, 6)]);
            }

            position2.Add(p2_1);
            position2.Add(p2_2);
            position2.Add(p2_3);
            position2.Add(p2_4);
            position2.Add(p2_5);
            position2.Add(p2_6);

            if (SceneManager.GetActiveScene().name == "field1")
            {
                for (int i = 0; i < Spawn.Count; i++)
                {
                    Instantiate(Spawn[i], position[i], Quaternion.identity);
                }
                Instantiate(Monsters[3], position[8], Quaternion.identity);
            }
            else if(SceneManager.GetActiveScene().name == "field2")
            {
                for(int i = 0; i<Spawn2.Count;i++)
                {
                    Instantiate(Spawn2[i], position2[i], Quaternion.identity);
                }
                Instantiate(Monsters[5], position2[5], Quaternion.identity);
            }
            
        }
    }
    void LevelUp()
    {
        if(PlayerControl.inlevelupflag == true)
        {
            PlayerControl.inlevelupflag = false;
            Time.timeScale = 0;
            levelup_panel.SetActive(true);

            ability1 = Random.Range(0, 5);
            ability2 = Random.Range(0, 5);
            ability3 = Random.Range(0, 5);

            if(ability[ability1] == "Hp_up")
            {
                ability_1_txt.text = "체력 증가";
            }
            else if(ability[ability1] == "Attack_up")
            {
                ability_1_txt.text = "공격력 증가";
            }
            else if(ability[ability1] == "Attackspeed_up")
            {
                ability_1_txt.text = "공격속도 증가";
            }
            else if(ability[ability1] == "Attackrange_up")
            {
                ability_1_txt.text = "공격범위 증가";
            }
            else if(ability[ability1] == "Heal")
            {
                ability_1_txt.text = "체력 회복";
            }

            if (ability[ability2] == "Hp_up")
            {
                ability_2_txt.text = "체력 증가";
            }
            else if (ability[ability2] == "Attack_up")
            {
                ability_2_txt.text = "공격력 증가";
            }
            else if (ability[ability2] == "Attackspeed_up")
            {
                ability_2_txt.text = "공격속도 증가";
            }
            else if (ability[ability2] == "Attackrange_up")
            {
                ability_2_txt.text = "공격범위 증가";
            }
            else if (ability[ability2] == "Heal")
            {
                ability_2_txt.text = "체력 회복";
            }

            if (ability[ability3] == "Hp_up")
            {
                ability_3_txt.text = "체력 증가";
            }
            else if (ability[ability3] == "Attack_up")
            {
                ability_3_txt.text = "공격력 증가";
            }
            else if (ability[ability3] == "Attackspeed_up")
            {
                ability_3_txt.text = "공격속도 증가";
            }
            else if (ability[ability3] == "Attackrange_up")
            {
                ability_3_txt.text = "공격범위 증가";
            }
            else if (ability[ability3] == "Heal")
            {
                ability_3_txt.text = "체력 회복";
            }

            Debug.Log(ability[ability1]);
            Debug.Log(ability[ability2]);
            Debug.Log(ability[ability3]);

            
        }
    }

    void Die()
    {      
        if (PlayerControl.dieflag == true)
        {
            PlayerControl.dieflag = false;
            float result_gold = gold - pre_gold;
            result_gold = result_gold + result_gold * PlayerControl.reward;
            gold += result_gold * PlayerControl.reward;
            int result_exp = 5 * PlayerControl.kill_count;
            PlayerControl.exp += result_exp;
            Debug.Log("DIE");
            Debug.Log("exp : " + PlayerControl.exp);
            result_panel.SetActive(true);
            Kill_text.text = PlayerControl.kill_count.ToString();
            Gold_text.text = result_gold.ToString();
            Exp_text.text = result_exp.ToString();
                     
        }

        
    }
    Item CreateItem()
    {
        Item item = new Item();
        item.level = Random.Range(1, 4);
        if (item.level == 1)
        {
            item.damage = Random.Range(3, 7);
        }
        else if (item.level == 2)
        {
            item.damage = Random.Range(10, 15);
        }
        else if (item.level == 3)
        {
            item.damage = Random.Range(25, 35);
        }
        item.shapeflag = Random.Range(0, 3);
        if (item.shapeflag == 0)
        {
            item.name = "장 검";
        }
        else if (item.shapeflag == 1)
        {
            item.name = "장 궁";
        }
        else if (item.shapeflag == 2)
        {
            item.name = "염주";
        }
        //transform.GetComponent<SpriteRenderer>().sprite = item.shape[item.shapeflag];

        return item;
    }
    void Inventory()
    {
        if (GameObject.Find("Inven_Panel") != null)
        {           
            Equip.GetComponent<Image>().sprite = Item_sprite[Equip_Item[0].shapeflag];
            if (Player_Item.Count > 0)
            {
                // Debug.Log(Player_Item[0].name);
                for (int i = 0; i < Player_Item.Count; i++)
                {
                    Inven[i].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                    Inven[i].GetComponent<Image>().sprite = Item_sprite[Player_Item[i].shapeflag];
                }
            }

            raycaster = canvas.GetComponent<GraphicRaycaster>();
            ped = new PointerEventData(null);
            ped.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            raycaster.Raycast(ped, results);           

            if (results[0].gameObject.name == "Equip_Image")
            {
                Item_Detail.transform.position = Input.mousePosition;
                Item_Detail.transform.gameObject.SetActive(true);
                Item_name.text = Equip_Item[0].name.ToString();
                Item_level.text = Equip_Item[0].level.ToString();
                Item_damage.text = Equip_Item[0].damage.ToString();
            }
            else if(results[0].gameObject.name == "Item1" && Player_Item[0] != null)
            {
                Item_Detail.transform.position = Input.mousePosition;
                Item_Detail.transform.gameObject.SetActive(true);
                Item_name.text = Player_Item[0].name.ToString();
                Item_level.text = Player_Item[0].level.ToString();
                Item_damage.text = Player_Item[0].damage.ToString();
            }
            else if(results[0].gameObject.name == "Item2" && Player_Item[1] != null)
            {
                Item_Detail.transform.position = Input.mousePosition;
                Item_Detail.transform.gameObject.SetActive(true);
                Item_name.text = Player_Item[1].name.ToString();
                Item_level.text = Player_Item[1].level.ToString();
                Item_damage.text = Player_Item[1].damage.ToString();


            }
            else if(results[0].gameObject.name == "Item3" && Player_Item[2] != null)
            {
                Item_Detail.transform.position = Input.mousePosition;
                Item_Detail.transform.gameObject.SetActive(true);
                Item_name.text = Player_Item[2].name.ToString();
                Item_level.text = Player_Item[2].level.ToString();
                Item_damage.text = Player_Item[2].damage.ToString();
            }
            else if(results[0].gameObject.name == "Item4" && Player_Item[3] != null)
            {
                Item_Detail.transform.position = Input.mousePosition;
                Item_Detail.transform.gameObject.SetActive(true);
                Item_name.text = Player_Item[3].name.ToString();
                Item_level.text = Player_Item[3].level.ToString();
                Item_damage.text = Player_Item[3].damage.ToString();
            }
            else if(results[0].gameObject.name == "Item5" && Player_Item[4] != null)
            {
                Item_Detail.transform.position = Input.mousePosition;
                Item_Detail.transform.gameObject.SetActive(true);
                Item_name.text = Player_Item[4].name.ToString();
                Item_level.text = Player_Item[4].level.ToString();
                Item_damage.text = Player_Item[4].damage.ToString();
            }
            else if(results[0].gameObject.name == "Item6" && Player_Item[5] != null)
            {
                Item_Detail.transform.position = Input.mousePosition;
                Item_Detail.transform.gameObject.SetActive(true);
                Item_name.text = Player_Item[5].name.ToString();
                Item_level.text = Player_Item[5].level.ToString();
                Item_damage.text = Player_Item[5].damage.ToString();
            }
            else if(results[0].gameObject.name == "Item7" && Player_Item[6] != null)
            {
                Item_Detail.transform.position = Input.mousePosition;
                Item_Detail.transform.gameObject.SetActive(true);
                Item_name.text = Player_Item[6].name.ToString();
                Item_level.text = Player_Item[6].level.ToString();
                Item_damage.text = Player_Item[6].damage.ToString();
            }
            else if(results[0].gameObject.name == "Item8" && Player_Item[7] != null)
            {
                Item_Detail.transform.position = Input.mousePosition;
                Item_Detail.transform.gameObject.SetActive(true);
                Item_name.text = Player_Item[7].name.ToString();
                Item_level.text = Player_Item[7].level.ToString();
                Item_damage.text = Player_Item[7].damage.ToString();
            }
            else
            {
                Item_Detail.transform.gameObject.SetActive(false);
            }

            if (Input.GetMouseButtonDown(1))
            {
                item_name = results[0].gameObject.name;
                Debug.Log("아이템 이름 : " + item_name);
                Item_Detail.gameObject.SetActive(false);
                Item_Menu.gameObject.SetActive(true);
                mouse = Input.mousePosition;
                Item_Menu.gameObject.transform.position = mouse;
            }
        }
    }

    public void EquipItem()
    {
        if(item_name == "Item1")
        {
            Item swap;
            swap = Equip_Item[0];
            Equip_Item[0] = Player_Item[0];
            Player_Item[0] = swap;           
            Item_Menu.gameObject.SetActive(false);
        }
    }
    void HpBar()
    {
        if(SceneManager.GetActiveScene().name == "MainScene")
        {
            Level.text = PlayerControl.level.ToString();
        }
        else
        {
            Level.text = PlayerControl.inlevel.ToString();
        }
        
        Hp.text = PlayerControl.hp + " / " + PlayerControl.maxHp;
        hp_image.fillAmount = PlayerControl.hp / PlayerControl.maxHp;
    }

    public void Addability1()
    {
        PlayerControl.player_ability.Add(ability[ability1]);
        Debug.Log(PlayerControl.player_ability[0]);
        Time.timeScale = 1;
    }
    public void Addability2()
    {
        PlayerControl.player_ability.Add(ability[ability2]);
        Debug.Log(PlayerControl.player_ability[0]);
        Time.timeScale = 1;
    }
    public void Addability3()
    {
        PlayerControl.player_ability.Add(ability[ability3]);
        Debug.Log(PlayerControl.player_ability[0]);
        Time.timeScale = 1;
    }

    public void SetAbility()
    {
        for (int i = 0; i < PlayerControl.player_ability.Count; i++)
        {
            if (PlayerControl.player_ability[i] == "Hp_up")
            {
                PlayerControl.maxHp += 10;
                PlayerControl.hp += 10;
            }
            else if (PlayerControl.player_ability[i] == "Attack_up")
            {
                PlayerControl.attack += 5;
            }
            else if (PlayerControl.player_ability[i] == "Attackspeed_up")
            {
                PlayerControl.attackdelay -= 0.25f;
            }
            else if (PlayerControl.player_ability[i] == "Attackrange_up")
            {
                PlayerControl.attackspeed += 0.5f;
            }
            else if (PlayerControl.player_ability[i] == "Heal")
            {
                PlayerControl.hp += 15;
                if (PlayerControl.hp > PlayerControl.maxHp)
                {
                    PlayerControl.hp = PlayerControl.maxHp;
                }
            }
        }
        Debug.Log(PlayerControl.maxHp);
        Debug.Log(PlayerControl.hp);
        Debug.Log(PlayerControl.attack);
        Debug.Log(PlayerControl.attackdelay);
        Debug.Log(PlayerControl.attackspeed);
    }
    void Status_manage()
    {
        if (GameObject.Find("Status_Panel") != null)
        {
            Status_point.text = PlayerControl.status.ToString();
            Hp_status.text = hp_status.ToString();
            Attack_status.text = attack_status.ToString();
            Attackdelay_status.text = attackdelay_status.ToString();
            Gold_status.text = gold_status.ToString();
        }
        
    }
    public void Hp_plus()
    {
        if(PlayerControl.status > 0)
        {
            hp_status++;
            PlayerControl.status--;
            PlayerControl.hp = 30 + (5 * hp_status);
            PlayerControl.maxHp = 30 + (5 * hp_status); ;
            Debug.Log("체력 : " + PlayerControl.maxHp);
        }
    }
    public void Hp_minus()
    {
        if(hp_status > 0)
        {
            hp_status--;
            PlayerControl.status++;
            PlayerControl.hp -= 5;
            PlayerControl.maxHp -= 5;
            Debug.Log("체력 : " + PlayerControl.maxHp);
        }
    }
    public void Attack_plus()
    {
        if (PlayerControl.status > 0)
        {
            attack_status++;
            PlayerControl.status--;
            PlayerControl.attack = 10 + (2 * attack_status); ;
            Debug.Log("공격력 : " + PlayerControl.attack);
        }
    }
    public void Attack_minus()
    {
        if (attack_status > 0)
        {
            attack_status--;
            PlayerControl.status++;
            PlayerControl.attack -= 5;
            Debug.Log("공격력 : " + PlayerControl.attack);
        }
    }
    public void Attackdelay_plus()
    {
        if (PlayerControl.status > 0)
        {
            attackdelay_status++;
            PlayerControl.status--;
            PlayerControl.attackdelay = 0.75f - (0.05f * attackdelay_status);
            Debug.Log("공격속도 : " + PlayerControl.attackdelay);
        }
    }
    public void Attackdelay_minus()
    {
        if (attackdelay_status > 0)
        {
            attackdelay_status--;
            PlayerControl.status++;
            PlayerControl.attackdelay += 0.05f;
            Debug.Log("공격속도 : " + PlayerControl.attackdelay);
        }
    }
    public void Gold_plus()
    {
        if (PlayerControl.status > 0)
        {
            gold_status++;
            PlayerControl.status--;
            PlayerControl.reward = 0 + (0.1f * gold_status);
            Debug.Log("보상 : " + PlayerControl.reward);
        }
    }
    public void Gold_minus()
    {
        if (gold_status > 0)
        {
            gold_status--;
            PlayerControl.status++;
            PlayerControl.reward -= 5;
            Debug.Log("보상 : " + PlayerControl.reward);
        }
    }
    public void EntertheField()
    {
        Player_hp = PlayerControl.hp;
        Player_maxhp = PlayerControl.maxHp;
        Player_attack = PlayerControl.attack;
        Player_attackspeed = PlayerControl.attackspeed;
        Player_attackdelay = PlayerControl.attackdelay;
        PlayerControl.isUnbeat = false;

        Debug.Log(Player_hp);
        Debug.Log(Player_maxhp);


        SceneManager.LoadScene("field1");
        Time.timeScale = 1;
    }

    public void Retry_stage()
    {
        PlayerControl.hp = Player_hp;
        PlayerControl.maxHp = Player_maxhp;
        PlayerControl.attack = Player_attack;
        PlayerControl.attackspeed = Player_attackspeed;
        PlayerControl.attackdelay =  Player_attackdelay;
        PlayerControl.inlevel = 1;
        PlayerControl.inexp = 0;
        PlayerControl.kill_count = 0;
        PlayerControl.isUnbeat = false;
        PlayerControl.dieflag = false;

        result_panel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Return_home()
    {
        PlayerControl.hp = Player_hp;
        PlayerControl.maxHp = Player_maxhp;
        PlayerControl.attack = Player_attack;
        PlayerControl.attackspeed = Player_attackspeed;
        PlayerControl.attackdelay = Player_attackdelay;
        PlayerControl.inlevel = 1;
        PlayerControl.inexp = 0;
        PlayerControl.kill_count = 0;
        PlayerControl.player_ability.Clear();
        PlayerControl.dieflag = false;

        result_panel.SetActive(false);
        SceneManager.LoadScene("MainScene");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Player" )
        {
            info.gameObject.SetActive(true);
            if (Input.GetKey(KeyCode.C))
            {
                Time.timeScale = 0;
                start_panel.SetActive(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            info.gameObject.SetActive(false);
        }
    }

    public void TimeBack()
    {
        Time.timeScale = 1;
    }
}

