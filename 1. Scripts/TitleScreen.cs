using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    public GameObject loginPanel;
    public GameObject signUpPanel;

    public static bool sign = false;
    public static bool signUp = false;

	// Use this for initialization
	void Start () {
        signUpPanel.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (sign) loginPanel.SetActive(false);
        if (signUp) signUpPanel.SetActive(true);
        if(!signUp) signUpPanel.SetActive(false);
	}
}
