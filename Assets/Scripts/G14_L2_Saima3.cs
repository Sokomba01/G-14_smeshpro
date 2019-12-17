using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G14_L2_Saima3 : MonoBehaviour
{
    public GameObject ballhit;
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
        Instantiate(ballhit);
        //print("abc");
    }
}
