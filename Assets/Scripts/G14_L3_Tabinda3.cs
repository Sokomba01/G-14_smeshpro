using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G14_L3_Tabinda3 : MonoBehaviour
{
    public bool iscollide;
    // Start is called before the first frame update
    void Start()
    {
        iscollide = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(iscollide)
        {
            this.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);

        }
    }

    void OnCollisionEnter(Collision collision)
    {
        
        //print("abc");
    }
}
