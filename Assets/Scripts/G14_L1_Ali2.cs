using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G14_L1_Ali2 : MonoBehaviour
{
    public GameObject ball1, ball2, ball3, ballcollideSound;
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        //creatObj();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            creatObj();
        }
    }

    void creatObj()
    {
        GameObject b1 = Instantiate(ball1);
        b1.transform.position = cam.transform.position;
        Vector3 b1pos = b1.transform.position;
        b1pos.y = 2;
        b1pos.z = -1;
        b1.transform.position = b1pos;
        b1.GetComponent<Rigidbody>().AddForce(0, -50, 100);

        GameObject b2 = Instantiate(ball2);
        b2.transform.position = cam.transform.position;
        Vector3 b2pos = cam.transform.position;
        b2pos.y = 2;
        b2pos.x = b2pos.x + 3.5f;
        b2pos.z = -1;
        b2.transform.position = b2pos;
        b2.GetComponent<Rigidbody>().AddForce(0, -50, 100);

        GameObject b3 = Instantiate(ball3);
        b3.transform.position = cam.transform.position;
        Vector3 b3pos = b3.transform.position;
        b3pos.y = 2;
        b3pos.x = b3pos.x - 3.5f;
        b3pos.z = -1;
        b3.transform.position = b3pos;
        b3.GetComponent<Rigidbody>().AddForce(0,-50,100);

    }

    void OnCollisionEnter(Collision collision)
    {
        Instantiate(ballcollideSound);
    }
}
