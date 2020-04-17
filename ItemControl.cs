using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemControl : MonoBehaviour
{
    public Item info;
    
    // Start is called before the first frame update
    void Start()
    {
        SetInfo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetInfo()
    {
        info.level = Random.Range(1, 3);
        if (info.level == 1)
        {
            info.damage = Random.Range(3, 7);
        }
        else if (info.level == 2)
        {
            info.damage = Random.Range(10, 15);
        }
        else if (info.level == 3)
        {
            info.damage = Random.Range(25, 35);
        }
        info.shapeflag = Random.Range(0, 3);
        this.transform.GetComponent<SpriteRenderer>().sprite = info.shape[info.shapeflag];

        if(info.shapeflag == 0)
        {
            info.name = "장 궁";
        }
        else if(info.shapeflag == 1)
        {
            info.name = "장 검";
        }
        else if(info.shapeflag == 2)
        {
            info.name = "염주";
        }
        Debug.Log(info.level);
        Debug.Log(info.damage);
    }
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Item 습득");
            GameManager.Player_Item.Add(info);
            Destroy(this.gameObject);
        }
    }
}

[System.Serializable]
public class Item
{
    public string name;
    public int level;
    public int damage;
    public int shapeflag = 0;
    public List<Sprite> shape = new List<Sprite>();
}