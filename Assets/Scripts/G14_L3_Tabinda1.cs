using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;

using TMPro;
using UnityEngine.SceneManagement;

public class G14_L3_Tabinda1 : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem par;
    public GameObject parObj;
    string mystring;
    public Text acp, current, stateLabel;
    public TMP_InputField myInput;
    public Button btn, backBtn;
    public GameObject btns, input, state, pcube, anim_cube, langTxt, resetBtn, doubleGlassAnim;
    GameObject cube;
    public GameObject cubeChild, bbtn;
    public Camera cam;
    string cubeText;
    string direction, CurrentState = "q0";
    Ray ray;
    bool animationAccept = false;
    bool animationReject = false;
    RaycastHit hit;
    public AudioSource beep;
    //public GameObject ubox;
    public Sprite[] Acceptimages;
    public Image A_images;

    public Sprite[] Rejectimages;
    public Image R_images;
    public AudioSource tabiMove, tabiReject, tabiAccepted;

    void Start()
    {
        enabled = false;

        A_images.GetComponent<Image>().enabled = false;
        R_images.GetComponent<Image>().enabled = false;
        parObj.SetActive(false);
        resetBtn.SetActive(false);
        //state.SetActive(false);
        
        //btn.onClick.AddListener(getInput);
        //backBtn.onClick.AddListener(back);
        //btn1.onClick.AddListener(plandrom);       
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            tabiMove.Play();
            myLang();
            genSphere();
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (animationAccept)
        {
            A_images.GetComponent<Image>().enabled = true;
            A_images.sprite = Acceptimages[(int)(Time.time * 10) % Acceptimages.Length];
        }
        if (animationReject)
        {
            R_images.GetComponent<Image>().enabled = true;
            R_images.sprite = Rejectimages[(int)(Time.time * 10) % Rejectimages.Length];
        }

    }

    public void reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void getInput()
    {



        enabled = true;
        mystring =myInput.text;
        float j = 0;
        string pattren = @"([a-c]+)$";
        Match m = Regex.Match(myInput.text, pattren);
        if (m.Success || mystring=="")
        {
            acp.text = "";
            resetBtn.SetActive(true);
            bbtn.SetActive(false);
            btns.SetActive(false);
            input.SetActive(false);
            state.SetActive(true);
            langTxt.SetActive(false);
            mystring = "Δ" + myInput.text + "ΔΔ";
            stateLabel.text = "On State:";
            current.text = "q0";
            char[] str = mystring.ToCharArray();
            for (int i = 0; i < mystring.Length; i++)
            {
                cube = Instantiate(pcube);
                //cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = new Vector3(j, 0, 0.32f);
                cube.transform.localScale = new Vector3(2.5f, 2.5f, 2f);
                cube.GetComponent<Renderer>().material.color = Color.cyan;
                //cube.AddComponent<Rigidbody>();
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
                txt.GetComponent<TextMeshPro>().alignment = TextAlignmentOptions.Center;
                txt.GetComponent<TextMeshPro>().text = str[i].ToString();
                txt.transform.Rotate(90, 0, 0);
                Vector3 pos = txt.transform.localPosition;
                pos.x = 0;
                pos.y = 0.65f;
                pos.z = 0;
                txt.transform.localPosition = pos;
                j = j + 5f;

                anim_cube.gameObject.GetComponent<Animator>().enabled = true;
            }
        }
        else
        {
            acp.text = "****Invalid Alphabets****";
        }
    }

    void myLang()
    {
        ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out hit))
        {
            cubeChild = hit.transform.GetChild(0).gameObject;
            cubeText = cubeChild.GetComponent<TextMeshPro>().text;
        }
        if (CurrentState == "q0")
        {
            if (cubeText == "a")
            {
                cubeChild.GetComponent<TextMeshPro>().text = "x";
                direction = "f";
                moveFunction(direction);
                CurrentState = "q1";
                current.text = "q1";
            }
            else if (cubeText == "y")
            {
                CurrentState = "q4";
                current.text = "q4";
                direction = "f";
                moveFunction(direction);
            }
            else
            {
                //enabled = false;
                anim_reject();
                acp.text = "String is Rejected";
                acp.color = Color.red;
            }
        }
        else if (CurrentState == "q1")
        {
            if (cubeText == "y")
            {
                CurrentState = "q1";
                current.text = "q1";
                direction = "f";
                moveFunction(direction);
            }
            else if (cubeText == "a")
            {
                CurrentState = "q1";
                current.text = "q1";
                direction = "f";
                moveFunction(direction);
            }
            else if (cubeText == "b")
            {
                cubeChild.GetComponent<TextMeshPro>().text = "y";
                CurrentState = "q2";
                current.text = "q2";
                direction = "f";
                moveFunction(direction);
            }
            else
            {
                //enabled = false;
                anim_reject();
                acp.text = "String is Rejected";
            }
        }
        else if (CurrentState == "q2")
        {
            if (cubeText == "b" || cubeText =="z")
            {
                CurrentState = "q2";
                current.text = "q2";
                direction = "f";
                moveFunction(direction);
            }
            else if(cubeText =="c")
            {
                CurrentState = "q3";
                current.text = "q3";
                cubeChild.GetComponent<TextMeshPro>().text = "z";
                direction = "b";
                moveFunction(direction);
            }
            else
            {
                anim_reject();
                //enabled = false;
                acp.text = "String is Rejected";
            }
        }
        else if(CurrentState == "q3")
        {
            if(cubeText =="z" || cubeText == "b"|| cubeText == "y" || cubeText == "a")
            {
                CurrentState = "q3";
                current.text = "q3";
                direction = "b";
                moveFunction(direction);
            }
           else if(cubeText =="x")
            {
                CurrentState = "q0";
                current.text = "q0";
                direction = "f";
                moveFunction(direction);
            }
            else
            {
                //enabled = false;
                anim_reject();
                acp.text = "String is Rejected";
            }
        }
        else if(CurrentState =="q4")
        {
            if(cubeText =="y")
            {
                CurrentState = "q4";
                current.text = "q4";
                direction = "f";
                moveFunction(direction);
            }
           else if(cubeText =="b")
            {
                CurrentState = "q5";
                current.text = "q5";
                cubeChild.GetComponent<TextMeshPro>().text = "y";
                direction = "f";
                moveFunction(direction);
            }
            else if(cubeText =="z")
            {
                CurrentState = "q7";
                current.text = "q7";
                direction = "f";
                moveFunction(direction);
            }
            else
            {
                //enabled = false;
                anim_reject();
                acp.text = "String is Rejected";
            }
        }
        else if(CurrentState =="q5")
        {
            if(cubeText =="b" ||cubeText=="z")
            {
                CurrentState = "q5";
                current.text = "q5";
                direction = "f";
                moveFunction(direction);
            }
           else if(cubeText =="c")
            {
                CurrentState = "q6";
                current.text = "q6";
                cubeChild.GetComponent<TextMeshPro>().text = "z";
                direction = "b";
                moveFunction(direction);
            }
            else
            {
                anim_reject();
                //enabled = false;
                acp.text = "String is Rejected";
            }
        }
        else if(CurrentState =="q6")
        {
            if(cubeText =="z")
            {
                CurrentState = "q6";
                current.text = "q6";
                direction = "b";
                moveFunction(direction);
            }
           else if(cubeText =="y")
            {
                CurrentState = "q4";
                current.text = "q4";
                direction = "f";
                moveFunction(direction);
            }
            else
            {
                anim_reject();
                //enabled = false;
                acp.text = "String is Rejected";
            }
        }
        else if(CurrentState =="q7")
        {
            if(cubeText =="z")
            {
                CurrentState = "q7";
                current.text = "q7";
                direction = "f";
                moveFunction(direction);
            }
           else if(cubeText == "Δ")
            {
                CurrentState = "q8";
                current.text = "q8";
                direction = "s";
                moveFunction(direction);
            }
           else if(cubeText =="c")
            {
                CurrentState = "q9";
                current.text = "q9";
                cubeChild.GetComponent<TextMeshPro>().text = "z";
                direction = "s";
                moveFunction(direction);
            }
            else
            {
                anim_reject();
                //enabled = false;
                acp.text = "String is Rejected";
            }
        }
        else if(CurrentState =="q8")
        {
            
            acp.text = "String is Accepted";
            anim_accept();

            //enabled = false;

        }
        else if(CurrentState =="q9")
        {
            acp.text = "String is Accepted";
            anim_accept();
            //enabled = false;
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
    
    void anim_accept()
    {
       // print("Called");
        animationAccept = true;
        tabiAccepted.Play();
    }
    void anim_reject()
    {
        tabiReject.Play();
        animationReject = true;
    }
    public void back()
    {
        SceneManager.LoadScene("G14_Main_Menu");
    }
    void genSphere()
    {
        Vector3 campos = cam.transform.position;
        Vector3 pos;
        pos.x = campos.x;
        pos.y = 4;
        pos.z = 0.5f;
        GameObject sp = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sp.transform.position = pos;
        sp.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        sp.GetComponent<Renderer>().material.color = Color.green;
        sp.AddComponent<Rigidbody>();
        //sp.AddComponent<BoxCollider>();
        Rigidbody rb = sp.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.AddForce(-200, 0, 0);

        Vector3 pos1;
        pos1.x = pos.x+5;
        pos1.y = pos.y-1;
        pos1.z = pos.z+0.5f;
        GameObject sp1 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sp1.transform.position = pos1;
        sp1.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        sp1.GetComponent<Renderer>().material.color = Color.blue;
        sp1.AddComponent<Rigidbody>();
        //sp1.AddComponent<BoxCollider>();
        Rigidbody rb1 = sp1.GetComponent<Rigidbody>();
        rb1.useGravity = false;
        rb1.AddForce(-100, 0, 0);

        Vector3 pos2;
        pos2.x = pos.x+9;
        pos2.y = pos.y;
        pos2.z = pos.z-0.5f;
        GameObject sp2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sp2.transform.position = pos2;
        sp2.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        sp2.GetComponent<Renderer>().material.color = Color.yellow;
        sp2.AddComponent<Rigidbody>();
        //sp2.AddComponent<BoxCollider>();
        Rigidbody rb2 = sp2.GetComponent<Rigidbody>();
        rb2.useGravity = false;
        rb2.AddForce(-300, 0, 0);
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