using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class G14_L1_Ali1 : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem par;
    public GameObject parObj;
    string mystring;
    public Text AcceptLabelTxt, currentStateTxt, stateLabelTxt;
    public TMP_InputField myInputField;
    public Button btn, backBtn, resetBtn;
    public GameObject btns, inputField, state, bbtn, resetBtnObj, lang, animCap, animGlass;
    GameObject cube;
    public GameObject text, ChildOfCube, precube, ball1, ball2, ball3;
    public Camera cam;
    string cubeText, movement, onState = "q0";
    Ray ray;
    RaycastHit hit;
    public AudioSource beep;
    bool animationAccept = false;
    bool animationReject = false;
    public Sprite[] Acceptimages;
    public Image A_images;

    public Sprite[] Rejectimages;
    public Image R_images;


    void Start()
    {
        resetBtnObj.SetActive(false);
       A_images.GetComponent<Image>().enabled = false;
       R_images.GetComponent<Image>().enabled = false;
        parObj.SetActive(false);
        enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (animationAccept)
        {
            A_images.GetComponent<Image>().enabled = true;
            A_images.sprite = Acceptimages[(int)(Time.time * 10) % Acceptimages.Length];
        }
        if (animationReject)
        {
            createObj();
            R_images.GetComponent<Image>().enabled = true;
            R_images.sprite = Rejectimages[(int)(Time.time * 10) % Rejectimages.Length];
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            beep.Play();
            MyTMRun();
        }
    }


    public void getInput()
    {
        enabled = true;
        mystring =myInputField.text;
        float j = 0;
        string pattren = @"^([1#]+)$";
        Match m = Regex.Match(myInputField.text, pattren);
        if (m.Success || mystring=="")
        {
            animCap.gameObject.GetComponent<Animator>().enabled = true;
            animGlass.gameObject.GetComponent<Animator>().enabled = true;
            AcceptLabelTxt.text = "";
            lang.SetActive(false);
            resetBtnObj.SetActive(true);
            btns.SetActive(false);
            inputField.SetActive(false);
            bbtn.SetActive(false);
            state.SetActive(true);
            mystring = "Δ" + myInputField.text + "ΔΔ";
            stateLabelTxt.text = "On State:";
            currentStateTxt.text = "q0";

            char[] str = mystring.ToCharArray();
            for (int i = 0; i < mystring.Length; i++)
            {
                cube = Instantiate(precube);
                //cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = new Vector3(j, 0, 0.32f);
               cube.transform.localScale = new Vector3(2.3f, 2.3f, 2f);
                //cube.GetComponent<Renderer>().material.color = Color.white;
                cube.AddComponent<Rigidbody>();
                cube.AddComponent<BoxCollider>();
                cube.AddComponent<Animator>();
                //var rb = cube.GetComponent<Rigidbody>();
                //rb.drag = 0;
                //rb.angularDrag = 0;
                //rb.useGravity = false;
                GameObject go = new GameObject();
                go.transform.parent = cube.transform;
                //go.AddComponent<TextMeshPro>();
                GameObject txt = cube.transform.GetChild(0).gameObject;
                txt.AddComponent<TextMeshPro>();
                txt.GetComponent<TextMeshPro>().fontSize = 20;
                txt.GetComponent<TextMeshPro>().color = Color.green;
                txt.GetComponent<TextMeshPro>().alignment = TextAlignmentOptions.Center;
                txt.GetComponent<TextMeshPro>().text = str[i].ToString();
                txt.transform.Rotate(90, 0, 0);
                Vector3 pos = txt.transform.localPosition;
                pos.x = 0;
                pos.y = 0.65f;
                pos.z = 0;
                txt.transform.localPosition = pos;
                j = j + 5f;
            }
        }
        else
        {
            AcceptLabelTxt.text = "Please Enter only 1s and #s";
        }
    }

    void MyTMRun()
    {
        ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out hit))
        {
            ChildOfCube = hit.transform.GetChild(0).gameObject;
            cubeText = ChildOfCube.GetComponent<TextMeshPro>().text;
        }
        switch(onState)
        {
            case "q0":
                {
                    if (cubeText == "1" || cubeText == "y")
                    {
                        onState = "q1";
                        currentStateTxt.text = "q1";
                        ChildOfCube.GetComponent<TextMeshPro>().text = "x";
                        movement = "f";
                        moveFunction(movement);
                    }
                    else if (cubeText == "#")
                    {
                        onState = "q4";
                        currentStateTxt.text = "q4";
                        movement = "f";
                        moveFunction(movement);
                    }
                    else
                    {
                        AcceptLabelTxt.color = Color.red;
                        AcceptLabelTxt.text = "String is Rejected";
                        anim_reject();
                    }
                    break;
                }
            case "q1":
                {
                    if (cubeText == "#")
                    {
                        onState = "q2";
                        currentStateTxt.text = "q2";
                        movement = "f";
                        moveFunction(movement);
                    }
                    else if (cubeText == "1" || cubeText == "y")
                    {
                        onState = "q1";
                        currentStateTxt.text = "q1";
                        movement = "f";
                        moveFunction(movement);
                    }
                    else
                    {
                        AcceptLabelTxt.color = Color.red;
                        AcceptLabelTxt.text = "String is Rejected";
                        anim_reject();
                    }
                    break;
                }
            case "q2":
                {
                    if (cubeText == "1")
                    {
                        ChildOfCube.GetComponent<TextMeshPro>().text = "y";
                        onState = "q3";
                        currentStateTxt.text = "q3";
                        movement = "b";
                        moveFunction(movement);
                    }
                    else if (cubeText == "y")
                    {
                        onState = "q2";
                        currentStateTxt.text = "q2";
                        movement = "f";
                        moveFunction(movement);
                    }
                    else if (cubeText == "#")
                    {
                        onState = "q9";
                        currentStateTxt.text = "q9";
                        movement = "b";
                        moveFunction(movement);
                    }
                    else if (cubeText == "Δ")
                    {
                        onState = "q7";
                        currentStateTxt.text = "q7";
                        movement = "s";
                        moveFunction(movement);
                    }
                    else
                    {
                        anim_reject();
                        AcceptLabelTxt.color = Color.red;
                        AcceptLabelTxt.text = "String is Rejected";
                    }
                    break;
                }
            case "q3":
                {
                    if(cubeText =="1" || cubeText =="y" || cubeText=="#")
                    {
                        onState = "q3";
                        currentStateTxt.text = "q3";
                        movement = "b";
                        moveFunction(movement);
                    }
                    else if(cubeText =="x")
                    {
                        onState = "q0";
                        currentStateTxt.text = "q0";
                        movement = "f";
                        moveFunction(movement);
                    }
                    else
                    {
                        anim_reject();
                        AcceptLabelTxt.color = Color.red;
                        AcceptLabelTxt.text = "String is Rejected";
                    }
                    break;
                }
            case "q4":
                {
                    if(cubeText =="y")
                    {
                        onState = "q4";
                        currentStateTxt.text = "q4";
                        movement = "f";
                        moveFunction(movement);
                    }
                   else if (cubeText == "1")
                    {
                        onState = "q5";
                        currentStateTxt.text = "q5";
                        movement = "f";
                        moveFunction(movement);
                    }
                    else
                    {
                        anim_reject();
                        AcceptLabelTxt.color = Color.red;
                        AcceptLabelTxt.text = "String is Rejected";
                    }
                    break;
                }
            case "q5":
                {
                    if (cubeText == "1")
                    {
                        onState = "q5";
                        currentStateTxt.text = "q5";
                        movement = "f";
                        moveFunction(movement);
                    }
                    else if (cubeText == "#")
                    {
                        onState = "q8";
                        currentStateTxt.text = "q8";
                        movement = "b";
                        moveFunction(movement);
                    }
                    else if (cubeText == "Δ")
                    {
                        onState = "q6";
                        currentStateTxt.text = "q6";
                        movement = "s";
                        moveFunction(movement);
                    }
                    else
                    {
                        anim_reject();
                        AcceptLabelTxt.color = Color.red;
                        AcceptLabelTxt.text = "String is Rejected";
                    }
                    break;
                }
            case "q6":
                {
                    currentStateTxt.text = "q6";
                    AcceptLabelTxt.text = "String is Accepted";
                    anim_accept();
                    rotateBoxes();
                    break;
                }
            case "q7":
                {
                    currentStateTxt.text = "q7";
                    AcceptLabelTxt.text = "String is Accepted";
                    anim_accept();
                    rotateBoxes();
                    break;
                }
            case "q8":
                {
                    if (cubeText == "1" || cubeText =="y")
                    {
                        onState = "q8";
                        currentStateTxt.text = "q8";
                        movement = "b";
                        moveFunction(movement);
                    }
                   else if (cubeText == "#")
                    {
                        onState = "q0";
                        currentStateTxt.text = "q0";
                        movement = "f";
                        moveFunction(movement);
                    }
                    else
                    {
                        anim_reject();
                        AcceptLabelTxt.color = Color.red;
                        AcceptLabelTxt.text = "String is Rejected";
                        
                    }
                    break;
                }
            case "q9":
                {
                    if (cubeText == "y")
                    {
                        onState = "q9";
                        currentStateTxt.text = "q9";
                        movement = "b";
                        moveFunction(movement);
                    }
                   else if (cubeText == "#")
                    {
                        onState = "q0";
                        currentStateTxt.text = "q0";
                        movement = "f";
                        moveFunction(movement);
                    }
                    else
                    {
                        anim_reject();
                        AcceptLabelTxt.color = Color.red;
                        AcceptLabelTxt.text = "String is Rejected";
                    }
                    break;
                }
        }
    }
        void moveFunction(string ins)
        {
            if (ins == "f")
            {
                Vector3 position = cam.transform.position;
                position.x = position.x +5f;
                cam.transform.position = position;
            }
            else if (ins == "b")
            {
                Vector3 position = cam.transform.position;
                position.x = position.x - 5f;
                cam.transform.position = position;
            }
        }
    void rotateBoxes()
    {
        animCap.gameObject.SetActive(false);
        animGlass.gameObject.SetActive(false);
        ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out hit))
        {
            var animateBox = hit.transform.gameObject;
            var childText = hit.transform.GetChild(0).gameObject;
            childText.GetComponent<TextMeshPro>().text = "Accepted";
            childText.GetComponent<TextMeshPro>().fontSize = 5;
            animateBox.GetComponent<Renderer>().material.color = Color.green;
            var boxRB = animateBox.GetComponent<Rigidbody>();
            boxRB.drag = 0;
            boxRB.angularDrag = 0;
            boxRB.useGravity = false;
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

    void anim_accept()
    {
        animationAccept = true;
    }
    void anim_reject()
    {
       // createObj();
        animationReject = true;
    }
    public void back()
    {
        SceneManager.LoadScene("G14_Main_Menu");
    }

    public void reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void createObj()
    {
            GameObject b1 = Instantiate(ball1);
            b1.transform.position = cam.transform.position;
            Vector3 b1pos = b1.transform.position;
            b1pos.y = 2;
            b1pos.z = -1;
            b1.transform.position = b1pos;
            b1.GetComponent<Rigidbody>().AddForce(0, -100, 0);

            GameObject b2 = Instantiate(ball2);
            b2.transform.position = cam.transform.position;
            Vector3 b2pos = cam.transform.position;
            b2pos.y = 2;
            b2pos.x = b2pos.x + 3.5f;
            b2pos.z = -1;
            b2.transform.position = b2pos;
            b2.GetComponent<Rigidbody>().AddForce(0, -100, 0);

            GameObject b3 = Instantiate(ball3);
            b3.transform.position = cam.transform.position;
            Vector3 b3pos = b3.transform.position;
            b3pos.y = 2;
            b3pos.x = b3pos.x - 3.5f;
            b3pos.z = -1;
            b3.transform.position = b3pos;
        b3.GetComponent<Rigidbody>().AddForce(0, -100, 0);
    }

    void OnGUI()
    {
        if (animationAccept == true)
        {

            Vector3 parPos = cam.transform.position;
            parPos.y = 1;
            parPos.z = 0;
            par.transform.position = parPos;
            parObj.SetActive(true);
        }
    }

}