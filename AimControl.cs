using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimControl : MonoBehaviour
{
    Vector2 MousePosition;
    Camera Camera;
    // Start is called before the first frame update
    void Start()
    {
        Camera = GameObject.Find("Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
      
    }

  
}
