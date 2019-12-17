using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G14_L2_Saima2 : MonoBehaviour
{
    public Camera cam;
    public GameObject capsole;
    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.Space))
        {
            creatCapsul();
        }
    }
    void creatCapsul()
    {
        Vector3 pos = cam.transform.position;
        pos.y = pos.y-5;
        pos.z = pos.z+2;
        Vector3 pos1 = pos;
        pos1.x = pos1.x + 3;
        Vector3 pos2 = pos;
        pos2.x = pos2.x + 6;
        GameObject cap =  Instantiate(capsole);
        cap.transform.position = pos;
        GameObject cap1 = Instantiate(capsole);
        cap1.transform.position = pos1;
        GameObject cap2 = Instantiate(capsole);
        cap2.transform.position = pos2;
    }
}
