using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageControl : MonoBehaviour
{
    public Image notice;
    public static bool next_stage = false;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        nextstage();
    }
    void nextstage()
    {
        if (GameManager.stage_flag == 0)
        {
            if (next_stage == false)
            {
                next_stage = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {           
            if (Input.GetKey(KeyCode.C))
            {
                next_stage = false;
                Destroy(this.gameObject, 1.0f);
                SceneManager.LoadScene("field2");
            }
        }
    }
}
