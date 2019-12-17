using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G14_L1_Ali3 : MonoBehaviour
{
    public GameObject colideSound;
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
        Instantiate(colideSound);
        //collision.gameObject.GetComponent<Animator>().Play("WriteCube");
    }
}
