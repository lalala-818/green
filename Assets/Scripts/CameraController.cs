using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float MoveSpeed = 5.0f;
    private Camera camera;

    public Vector2 borderX = new Vector2(25,105);
    public Vector2 borderZ = new Vector2(160,260);
    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        CameraMove();
    }

    private void CameraMove()
    {
        //视角移动
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);
        //视角加速
        if(Input.GetKey(KeyCode.LeftShift))
        {
            dir *= 3;
        }
        //视角范围
        transform.position += dir * Time.deltaTime * MoveSpeed;

        if (transform.position.x > borderX.y)
        {
            transform.position = new Vector3(borderX.y, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < borderX.x)
        {
            transform.position = new Vector3(borderX.x, transform.position.y, transform.position.z);
        }
        if (transform.position.z > borderZ.y)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, borderZ.y);
        }
        else if (transform.position.z < borderZ.x)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, borderZ.x);
        }
        //视角缩放
        float mouseScrollWheel = Input.GetAxis("Mouse ScrollWheel");

        if (mouseScrollWheel > 0 && camera.fieldOfView < 80)
        {
            camera.fieldOfView -= mouseScrollWheel * -10 * 5;
        }
        else if (mouseScrollWheel < 0 && camera.fieldOfView > 15)
        {
            camera.fieldOfView += mouseScrollWheel * 10 * 5;
        }
    }
}
