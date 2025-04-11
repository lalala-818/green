using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    private GameObject Crop_Empty;
    public GameObject Crop_Sunlower;
    public Crop_Empty Empty;

    public List<GameObject> Crops = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        Crop_Empty = Resources.Load<GameObject>("Crop_Empty");
        Empty = GameObject.Instantiate<GameObject>(Crop_Empty).GetComponent<Crop_Empty>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, int.MaxValue, 1 << LayerMask.NameToLayer("Ground")))
        {
            //碰撞点地面
            if (hit.collider != null && hit.collider.gameObject.tag == "Ground")
            {
                GameObject crop = null;
                for (int i = 0; i < Crops.Count; i++)
                {
                    //土地吸附条件
                    if (Vector3.Distance(hit.point, Crops[i].transform.position) < 12)
                    {
                        crop = Crops[i];
                        break;
                    }
                }
                if (crop != null)
                {
                    //可以吸附的四个方向
                    Vector3 top = crop.transform.position + new Vector3(0, 0, 10);
                    Vector3 bottom = crop.transform.position + new Vector3(0, 0, -10);
                    Vector3 left = crop.transform.position + new Vector3(-10, 0, 0);
                    Vector3 right = crop.transform.position + new Vector3(10, 0, 0);
                    Vector3[] points = new Vector3[] { top, bottom, left, right };

                    float dis = 10000;
                    Vector3 tempPoint=Vector3.zero;
                    //找到最近的吸附点
                    for (int i = 0; i < points.Length; i++)
                    {
                        if (Vector3.Distance(hit.point, points[i]) < dis)
                        {
                            dis = Vector3.Distance(hit.point, points[i]);
                            tempPoint = points[i];
                        }
                    }
                    //确定坐标
                    Empty.transform.position = tempPoint;
                }
                else
                {
                    Empty.transform.position = hit.point;
                }
            }
            //土地建造
            if (Input.GetMouseButtonDown(0))
            {
                if(Empty.CanCreat)
                {
                    GameObject temp = GameObject.Instantiate<GameObject>(Crop_Sunlower, Empty.transform.position, Quaternion.identity, null);
                    Crops.Add(temp);
                }
                else
                {
                    Debug.Log("重叠");
                }
            }
        }

    }
}
