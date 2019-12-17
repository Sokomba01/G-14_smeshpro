using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CubeGenerate : MonoBehaviour
{
    // Start is called before the first frame update
    string mystring;
    public Text acp, current, stateLabel;
    public TMP_InputField myInput;
    public Button btn, backBtn;
    public GameObject btns, input, state;
    bool changeValue, count;
    GameObject cube;
    public GameObject text;
    public Camera cam;
    string cubeText="s";
    string direction = "f", strt;
    Ray ray;
    RaycastHit hit;
    public AudioSource beep;
    bool Acceptcall=false;
    bool rejectCall = false;
    //public GameObject ubox;
    void Start()
    {
        enabled = false;
        state.SetActive(false);
        
        btn.onClick.AddListener(getInput);
        backBtn.onClick.AddListener(back);
        //btn1.onClick.AddListener(plandrom);       
    }

   // void OnGUI()
    //{
        //if(Event.current.Equals(Event.KeyboardEvent("a")))
        //{
          //  plandrom();
        //}
    //}

    // Update is called once per frame


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            beep.Play();
            plandrom();
        }

        if (Acceptcall)
        {
            rotateBoxes();
        }
        if(rejectCall)
        {
            resetrigidbody();
        }
    }
    void getInput()
    {
        enabled = true;
        
        mystring =myInput.text;
        int j = 0;
        string pattren = @"([0-1]+)$";
        Match m = Regex.Match(myInput.text, pattren);
        if (m.Success || mystring=="")
        {
            acp.text = "";
            btns.SetActive(false);
            input.SetActive(false);
            state.SetActive(true);
            mystring = "Δ" + myInput.text + "ΔΔ";
            stateLabel.text = "On State:";
            current.text = "q0";



            char[] str = mystring.ToCharArray();
            for (int i = 0; i < mystring.Length; i++)
            {
                cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = new Vector3(j, 0, 0);
                cube.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
                cube.AddComponent<Rigidbody>();
                var rb = cube.GetComponent<Rigidbody>();
                rb.drag = 0;
                rb.angularDrag = 0;
                rb.useGravity = false;
                GameObject go = new GameObject();
                go.transform.parent = cube.transform;
                //go.AddComponent<TextMeshPro>();
                GameObject txt = cube.transform.GetChild(0).gameObject;
                txt.AddComponent<TextMeshPro>();
                txt.GetComponent<TextMeshPro>().fontSize = 10;
                txt.GetComponent<TextMeshPro>().alignment = TextAlignmentOptions.Center;
                txt.GetComponent<TextMeshPro>().text = str[i].ToString();
                txt.transform.Rotate(90, 0, 0);
                Vector3 pos = txt.transform.localPosition;
                pos.x = 0;
                pos.y = 0.65f;
                pos.z = 0;
                txt.transform.localPosition = pos;
                j = j + 2;

            }
        }
        else
        {
            acp.text = "Please Enter only 0s and 1s";
        }
    }



    void plandrom()
    {
        if (cubeText == "s" && direction == "f")
        {
            ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(ray, out hit))
            {
                text = hit.transform.GetChild(0).gameObject;
                cubeText = text.GetComponent<TextMeshPro>().text;
                strt = cubeText;
                text.GetComponent<TextMeshPro>().text = "Δ";
            }
            if (cubeText == "1")
            {
                current.text = "q2";
            }
            else
            {
                current.text = "q1";
            }
            count = true;
            direction = "f";
            moveFunction(direction);
        }
        else if (cubeText != "Δ" && direction == "f")
        {
            if(cubeText =="1" && count == true)
            {
                current.text = "q2";
                count = false;
            }
            else if(cubeText == "0" && count == true)
            {
                current.text = "q1";
                count = false;
            }
            direction = "f";
            moveFunction(direction);
            if (changeValue == true)
            {
                changeValue = false;
                strt = cubeText;
                text.GetComponent<TextMeshPro>().text = "Δ";
            }
            ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(ray, out hit))
            {
                text = hit.transform.GetChild(0).gameObject;
                cubeText = text.GetComponent<TextMeshPro>().text;
            }
        }
        else if (cubeText == "Δ" && direction == "f")
        {
            direction = "b";
            moveFunction(direction);
            ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(ray, out hit))
            {
                text = hit.transform.GetChild(0).gameObject;
                cubeText = text.GetComponent<TextMeshPro>().text;
            }
            changeValue = true;
            if (cubeText == "1")
            {
                current.text = "q4";
                count = true;
            }
            else
            {
                current.text = "q3";
                count = true;
            }
            if (cubeText == "Δ")
            {
                current.text = "q7";
                acp.text = "String is accepted";
                Acceptcall = true;
                enabled = false;
                
            }

        }
        else if(cubeText!= "Δ" && direction=="b")
        {
            if (cubeText == "1" && count == true)
            {
                current.text = "q6";
                count = false;
            }
            else if (cubeText == "0" && count == true)
            {
                current.text = "q5";
                count = false;
            }
            direction = "b";
            moveFunction(direction);
            if (changeValue == true)
            {
                string last = cubeText;
                changeValue = false;
                if (last == strt)
                {
                    text.GetComponent<TextMeshPro>().text = "Δ";
                }
                else
                {
                    rejectCall = true;
                    acp.text = "String is Rejected";
                    acp.color = new Color(255, 0, 0);
                    enabled = false;
                }
            }
            ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(ray, out hit))
            {
                text = hit.transform.GetChild(0).gameObject;
                cubeText = text.GetComponent<TextMeshPro>().text;
            }
        }
        else if(cubeText == "Δ" && direction =="b")
        {
            direction = "f";
            moveFunction(direction);
            ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if(Physics.Raycast(ray, out hit))
            {
                text = hit.transform.GetChild(0).gameObject;
                cubeText = text.GetComponent<TextMeshPro>().text;
                
            }
            changeValue = true;
            if (cubeText == "1")
            {
                current.text = "q0";
                count = true;
            }
            else
            {
                current.text = "q0";
                count = true;
            }
            if (cubeText == "Δ")
            {
                current.text = "q7";
                acp.text = "String is Accepted";
                Acceptcall = true;
                enabled = false;

            }

        }
        /*else if((cubeText =="0" || cubeText =="1") && direction =="f")
        {
            ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(ray, out hit))
            {
                text = hit.transform.GetChild(0).gameObject;
                cubeText = text.GetComponent<TextMeshPro>().text;
                if (changeValue == true)
                {
                    changeValue = false;
                    strt = cubeText;
                    text.GetComponent<TextMeshPro>().text = "x";
                }
            }
            direction = "f";
            moveFunction(direction);
        }*/

    }
        void moveFunction(string ins)
        {
            if (ins == "f")
            {
                Vector3 position = this.transform.position;
                position.x = position.x +2;
                this.transform.position = position;
            }
            else if (ins == "b")
            {
                Vector3 position = this.transform.position;
                position.x = position.x - 2;
                this.transform.position = position;
            }
        }
    void rotateBoxes()
    {
        ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out hit))
        {
            var animateBox = hit.transform.gameObject;
            var childText = hit.transform.GetChild(0).gameObject;
            childText.GetComponent<TextMeshPro>().text = "Accepted";
            childText.GetComponent<TextMeshPro>().fontSize = 5;
            animateBox.GetComponent<Renderer>().material.color = Color.green;
            var boxRB = animateBox.GetComponent<Rigidbody>();
            boxRB.angularVelocity = Random.insideUnitSphere;
        }
    }
    void resetrigidbody()
    {
        ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if(Physics.Raycast(ray, out hit))
        {
            var mybox = hit.transform.gameObject;
            var mychild = hit.transform.GetChild(0).gameObject;
            mychild.GetComponent<TextMeshPro>().text = "Rejected";
            mychild.GetComponent<TextMeshPro>().fontSize = 5;
            mybox.GetComponent<Renderer>().material.color = Color.black;
            var rig = mybox.GetComponent<Rigidbody>();
            rig.angularVelocity = Random.insideUnitSphere;
        }
    }
    void back()
    {
        SceneManager.LoadScene("G14_Main_Menu");
    }

    /*void upbox()
    {
        Vector3 pos = ubox.transform.position;
        pos.y = pos.y - 1;
        ubox.transform.position = pos;
        ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out hit))
        {
            GameObject firstBox = hit.transform.gameObject;
            Vector3 b1 = firstBox.transform.position;
            b1.y = b1.y + 1;
            ubox = firstBox;
        }
        Debug.Log("abc");
    }*/
}