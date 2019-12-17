using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class G14_L3_Tabinda4 : MonoBehaviour
{
    public GameObject colideSound;
    float t = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        for(int i=0;i<collision.transform.childCount;i++)
        {
            var ch=collision.transform.GetChild(i);
            ch.GetComponent<G14_L3_Tabinda3>().iscollide = true;
        }
        Instantiate(colideSound);
        //gameObject.transform.Rotate(90, 0, 0);
        Destroy(gameObject, t);
    }
}
