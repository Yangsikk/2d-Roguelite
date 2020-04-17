using UnityEngine;
using System.Collections;

public class BulletControl : MonoBehaviour
{
    Vector3 target;
    public static float attackspeed = 10.0f;
    // Use this for initialization
    void Start()
    {
        target = PlayerControl.target;

        Destroy(this.gameObject, 0.3f);

        Vector3 MousePosition = Input.mousePosition;
        Vector3 ObjectPosition = transform.position;

        MousePosition.z = ObjectPosition.z - Camera.main.transform.position.z;

        Vector3 rotate = Camera.main.ScreenToWorldPoint(MousePosition);

        float dy = rotate.y - ObjectPosition.y;
        float dx = rotate.x - ObjectPosition.x;

        float rotateDegree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, rotateDegree);

    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    void Shoot()
    {
    
        this.transform.position = Vector3.MoveTowards(this.transform.position, target, Time.deltaTime * attackspeed);
    }
}
