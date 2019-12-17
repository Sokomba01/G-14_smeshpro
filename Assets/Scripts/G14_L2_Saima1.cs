using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class G14_L2_Saima1 : MonoBehaviour
{
    string mystring;
    public ParticleSystem par;
    public Text acp, current, stateLabel;
    public TMP_InputField myInput;
    public Button btn, backBtn;
    public GameObject btns, input, state, Child, parObj, bbtn, resetBtn, lang, dGlassAnim, capAnim;
    GameObject cube;
    public GameObject text, pcube;
    public Camera cam;
    string txt_cube;
    string direction, rec_state = "q0";
    Ray ray;
    RaycastHit hit;
    public AudioSource tm_running_sound, acceptAudio;
    
    bool animationAccept = false;
    bool animationReject = false;
    public Sprite[] Acceptimages;
    public Image A_images;
    public Sprite[] Rejectimages;
    public Image R_images;


    //public GameObject ubox;
    void Start()
    {
        A_images.GetComponent<Image>().enabled = false;
        R_images.GetComponent<Image>().enabled = false;
        parObj.SetActive(false);
        enabled = false;
        resetBtn.SetActive(false);
        capAnim.GetComponent<Animator>().enabled = false;
        //state.SetActive(false);
        
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
            R_images.GetComponent<Image>().enabled = true;
            R_images.sprite = Rejectimages[(int)(Time.time * 10) % Rejectimages.Length];
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TM();
            tm_running_sound.Play();
            genrateSphere();
            moveDGlass();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        



        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void OnGUI()
    {
       if(animationReject==true)
        {

            Vector3 parPos = cam.transform.position;
            parPos.y = 1;
            parPos.z = 0;
            par.transform.position = parPos;
            parObj.SetActive(true);
        }
    }

    public void reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void getInput()
    {
        enabled = true;

        mystring = myInput.text;
        float j = 0;
        string pattren = @"^([ab]+)$";
        Match m = Regex.Match(myInput.text, pattren);
        if (m.Success || mystring == "")
        {
            acp.text = "";
            capAnim.GetComponent<Animator>().enabled = true;
            lang.SetActive(false);
            resetBtn.SetActive(true);
            bbtn.SetActive(false);
            btns.SetActive(false);
            input.SetActive(false);
            state.SetActive(true);
            mystring = "ΔΔ" + myInput.text + "ΔΔ";
            stateLabel.text = "On State:";
            current.text = "q0";



            char[] str = mystring.ToCharArray();
            for (int i = 0; i < mystring.Length; i++)
            {
                cube = Instantiate(pcube);
                //cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = new Vector3(j, 0, 0.32f);
                //cube.GetComponent<Renderer>().material.color = Color.black;
               // Texture2D tex = (Texture2D)Resources.Load("Assets/floor1", typeof(Texture2D));
                //cube.GetComponent<Renderer>().material.mainTexture = tex;
                cube.transform.localScale = new Vector3(2.3f, 2.3f, 2f);
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

            }
        }
        else
        {
            acp.text = "Please Enter Valid Strings of a's and b's";
        }
    }

    void TM()
    {
        ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out hit))
        {
            Child = hit.transform.GetChild(0).gameObject;
            txt_cube = Child.GetComponent<TextMeshPro>().text;
        }

        if (rec_state == "q0")
        {
            if (txt_cube == "a")
            {
                rec_state = "q1";
                current.text = "q1";
                Child.GetComponent<TextMeshPro>().text = "x";
                direction = "r";
                moveFunction(direction);
            }
            else
            {
                anim_reject();
                //enabled = false;
                acp.text = "String is Rejected";
                
            }
        }
        else if (rec_state == "q1")
        {
            if (txt_cube == "b")
            {
                rec_state = "q2";
                current.text = "q2";
                Child.GetComponent<TextMeshPro>().text = "x";
                direction = "r";
                moveFunction(direction);
            }
            else if (txt_cube == "a")
            {
                rec_state = "q1";
                current.text = "q1";
                Child.GetComponent<TextMeshPro>().text = "x";
                direction = "r";
                moveFunction(direction);
            }
            else
            {
                anim_reject();
                //enabled = false;
                acp.text = "String is Rejected";
                
            }
        }
        else if (rec_state == "q2")
        {
            if (txt_cube == "b")
            {
                rec_state = "q2";
                current.text = "q2";
                Child.GetComponent<TextMeshPro>().text = "x";
                direction = "r";
                moveFunction(direction);
            }
            else if (txt_cube == "a")
            {
                rec_state = "q3";
                current.text = "q3";
                Child.GetComponent<TextMeshPro>().text = "y";
                direction = "r";
                moveFunction(direction);
            }
            else
            {
                //enabled = false;
                anim_reject();
                acp.text = "String is Rejected";
                
            }
        }
        else if (rec_state == "q3")
        {
            if (txt_cube == "a")
            {
                rec_state = "q3";
                current.text = "q3";
                Child.GetComponent<TextMeshPro>().text = "y";
                direction = "r";
                moveFunction(direction);
            }
            else if (txt_cube == "b")
            {
                rec_state = "q4";
                current.text = "q4";
                Child.GetComponent<TextMeshPro>().text = "y";
                direction = "r";
                moveFunction(direction);
            }
            else
            {
                //enabled = false;
                anim_reject();
                acp.text = "String is Rejected";
                
            }
        }
        else if (rec_state == "q4")
        {
            if (txt_cube == "b")
            {
                rec_state = "q4";
                current.text = "q4";
                Child.GetComponent<TextMeshPro>().text = "y";
                direction = "r";
                moveFunction(direction);
            }
            else if (txt_cube == "Δ")
            {
                rec_state = "q5";
                current.text = "q5";
                Child.GetComponent<TextMeshPro>().text = "Δ";
                direction = "l";
                moveFunction(direction);
            }
            else
            {
                //enabled = false;
                acp.text = "String is Rejected";
                anim_reject();

            }
        }
        else if (rec_state == "q5")
        {
            if (txt_cube == "x")
            {
                rec_state = "q5";
                current.text = "q5";
                Child.GetComponent<TextMeshPro>().text = "x";
                direction = "l";
                moveFunction(direction);
            }
            else if (txt_cube == "y")
            {
                rec_state = "q5";
                current.text = "q5";
                Child.GetComponent<TextMeshPro>().text = "y";
                direction = "l";
                moveFunction(direction);
            }
            else if (txt_cube == "Δ")
            {
                rec_state = "q6";
                current.text = "q6";
                Child.GetComponent<TextMeshPro>().text = "Δ";
                direction = "r";
                moveFunction(direction);
            }

            else
            {
                //enabled = false;
                anim_reject();
                acp.text = "String is Rejected";
                
            }
        }
        else if (rec_state == "q6")
        {
            if (txt_cube == "x")
            {
                rec_state = "q7";
                current.text = "q7";
                Child.GetComponent<TextMeshPro>().text = "s";
                direction = "r";
                moveFunction(direction);
            }
            else if (txt_cube == "t")
            {
                rec_state = "q9";
                current.text = "q9";
                Child.GetComponent<TextMeshPro>().text = "t";
                direction = "r";
                moveFunction(direction);
            }
            else
            {
                //enabled = false;
                anim_reject();
                acp.text = "String is Rejected";
                
            }
        }
        else if (rec_state == "q7")
        {
            if (txt_cube == "x")
            {
                rec_state = "q7";
                current.text = "q7";
                Child.GetComponent<TextMeshPro>().text = "x";
                direction = "r";
                moveFunction(direction);
            }
            else if (txt_cube == "t")
            {
                rec_state = "q7";
                current.text = "q7";
                Child.GetComponent<TextMeshPro>().text = "t";
                direction = "r";
                moveFunction(direction);
            }
            else if (txt_cube == "y")
            {
                rec_state = "q8";
                current.text = "q8";
                Child.GetComponent<TextMeshPro>().text = "t";
                direction = "l";
                moveFunction(direction);
            }
            else
            {
                //enabled = false;
                anim_reject();
                acp.text = "String is Rejected";
                
            }
        }
        else if (rec_state == "q8")
        {
            if (txt_cube == "t")
            {
                rec_state = "q8";
                current.text = "q8";
                Child.GetComponent<TextMeshPro>().text = "t";
                direction = "l";
                moveFunction(direction);
            }
            else if (txt_cube == "x")
            {
                rec_state = "q8";
                current.text = "q8";
                Child.GetComponent<TextMeshPro>().text = "x";
                direction = "l";
                moveFunction(direction);
            }
            else if (txt_cube == "s")
            {
                rec_state = "q6";
                current.text = "q6";
                Child.GetComponent<TextMeshPro>().text = "s";
                direction = "r";
                moveFunction(direction);
            }
            else
            {
                //enabled = false;
                anim_reject();
                acp.text = "String is Rejected";
                
            }
        }
        else if (rec_state == "q9")
        {
            if (txt_cube == "t")
            {
                rec_state = "q9";
                current.text = "q9";
                Child.GetComponent<TextMeshPro>().text = "t";
                direction = "r";
                moveFunction(direction);
            }
            else if (txt_cube == "y")
            {
                rec_state = "q10";
                current.text = "q10";
                Child.GetComponent<TextMeshPro>().text = "t";
                direction = "r";
                moveFunction(direction);
                acp.text = "String is Accepted";
                anim_accept();
                acceptString();
                acceptAudio.Play();
            }
            else if (txt_cube == "Δ")
            {
                rec_state = "q10";
                current.text = "q10";
                Child.GetComponent<TextMeshPro>().text = "Δ";
                direction = "s";
                moveFunction(direction);
                acp.text = "String is Accepted";
                anim_accept();
                acceptString();
                acceptAudio.Play();
                //enabled = false;
            }
            else
            {
                //enabled = false;
                acp.text = "String is Rejected";
                anim_reject();


            }
        }
    }
    void anim_accept()
    {
        animationAccept = true;
    }
    void anim_reject()
    {
        animationReject = true;
        capAnim.GetComponent<Animator>().enabled = false;
    }

    void moveFunction(string ins)
    {
        if (ins == "r")
        {
            Vector3 position = cam.transform.position;
            position.x = position.x + 5f;
            cam.transform.position = position;
        }
        else if (ins == "l")
        {
            Vector3 position = cam.transform.position;
            position.x = position.x - 5f;
            cam.transform.position = position;
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
        if (Physics.Raycast(ray, out hit))
        {
            var mybox = hit.transform.gameObject;
            var mychild = hit.transform.GetChild(0).gameObject;
            mychild.GetComponent<TextMeshPro>().text = "Rejected";
            mychild.GetComponent<TextMeshPro>().fontSize = 5;
            mybox.GetComponent<Renderer>().material.color = Color.red;
            var rig = mybox.GetComponent<Rigidbody>();
            rig.angularVelocity = Random.insideUnitSphere;
        }
    }

    public void back()
    {
        SceneManager.LoadScene("G14_Main_Menu");
    }

    void genrateSphere()
    {
        Vector3 campos = cam.transform.position;
        Vector3 pos;
        pos.x = campos.x - 1.3f;
        pos.y = campos.y;
        pos.z = campos.z+2;
        GameObject sp = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sp.transform.position = pos;
        sp.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        sp.GetComponent<Renderer>().material.color = Color.green;
        sp.AddComponent<Rigidbody>();
        sp.AddComponent<BoxCollider>();
        Rigidbody rb = sp.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.AddForce(0, -200, 0);

        Vector3 pos1;
        pos1.x = pos.x - 3.4f;
        pos1.y = pos.y;
        pos1.z = pos.z;
        GameObject sp1 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sp1.transform.position = pos1;
        sp1.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        sp1.GetComponent<Renderer>().material.color = Color.blue;
        sp1.AddComponent<Rigidbody>();
        sp1.AddComponent<BoxCollider>();
        Rigidbody rb1 = sp1.GetComponent<Rigidbody>();
        rb1.useGravity = true;
        rb1.AddForce(0, -200, 0);

        Vector3 pos2;
        pos2.x = pos.x + 3.4f;
        pos2.y = pos.y;
        pos2.z = pos.z;
        GameObject sp2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sp2.transform.position = pos2;
        sp2.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        sp2.GetComponent<Renderer>().material.color = Color.yellow;
        sp2.AddComponent<Rigidbody>();
        sp2.AddComponent<BoxCollider>();
        Rigidbody rb2 = sp.GetComponent<Rigidbody>();
        rb2.useGravity = true;
        rb2.AddForce(0, -200, 0);
    }
    void acceptString()
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 campos = cam.transform.position;
            Vector3 pos;
            pos.x = campos.x;
            pos.y = 2;
            pos.z = 0;
            GameObject sp = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sp.transform.position = pos;
            sp.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            sp.GetComponent<Renderer>().material.color = Color.green;
            sp.AddComponent<Rigidbody>();
            sp.AddComponent<BoxCollider>();
            Rigidbody rb = sp.GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.AddForce(0, 30, 0);

            Vector3 pos1;
            pos1.x = pos.x - 3.4f;
            pos1.y = pos.y;
            pos1.z = pos.z;
            GameObject sp1 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sp1.transform.position = pos1;
            sp1.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            sp1.GetComponent<Renderer>().material.color = Color.blue;
            sp1.AddComponent<Rigidbody>();
            sp1.AddComponent<BoxCollider>();
            Rigidbody rb1 = sp1.GetComponent<Rigidbody>();
            rb1.useGravity = false;
            rb1.AddForce(0, 30, 0);

            Vector3 pos2;
            pos2.x = pos.x + 3.4f;
            pos2.y = pos.y;
            pos2.z = pos.z;
            GameObject sp2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sp2.transform.position = pos2;
            sp2.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            sp2.GetComponent<Renderer>().material.color = Color.yellow;
            sp2.AddComponent<Rigidbody>();
            sp2.AddComponent<BoxCollider>();
            Rigidbody rb2 = sp.GetComponent<Rigidbody>();
            rb2.useGravity = false;
            rb2.AddForce(0, 30, 0);
        }
    }

    void moveDGlass()
    {
        Vector3 gPos = dGlassAnim.transform.position;
        Vector3 camPos = cam.transform.position;
        if(gPos.x <= camPos.x)
        {
            dGlassAnim.GetComponent<Rigidbody>().AddForce(200, 0, 0);
        }
        else if(gPos.x >= camPos.x)
        {
            dGlassAnim.GetComponent<Rigidbody>().AddForce(-200, 0, 0);
        }
    }
}