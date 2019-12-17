using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G14_L3_Tabinda2 : MonoBehaviour
{
    public Camera cam;
    public GameObject glass, glass2, glass3;
    // Start is called before the first frame update
    void Start()
    {
        //CreatGlass();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CreatGlass();
        }
    }

    void CreatGlass()
    {
        Vector3 pos = cam.transform.position;
        pos.x = pos.x - 2f;
        pos.y = 3;
        pos.z = 0;
        Vector3 pos1 = pos;
        //pos1.x = pos1.x + 3;
        Vector3 pos2 = pos;
        //pos2.x = pos2.x + 6;
        pos2.z = pos.z + 1;
        GameObject cap = Instantiate(glass);
        cap.transform.position = pos;
        GameObject cap1 = Instantiate(glass);
        cap1.transform.position = pos1;
        GameObject cap2 = Instantiate(glass) ;
        cap2.transform.position = pos2;
    }
}
