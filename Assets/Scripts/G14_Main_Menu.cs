using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class G14_Main_Menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void palindrome()
    {
        SceneManager.LoadScene("Scene3");
    }
    public void ali()
    {
        SceneManager.LoadScene("G14_L1_Ali");
    }
    public void saima()
    {
        SceneManager.LoadScene("G14_L2_Saima");
    }
    public void tabinda()
    {
        SceneManager.LoadScene("G14_L3_Tabinda");
    }
}
