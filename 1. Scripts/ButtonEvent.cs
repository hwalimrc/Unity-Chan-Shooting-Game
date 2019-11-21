using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEvent : MonoBehaviour {

	public void SignUP()
    {
        TitleScreen.signUp = true;
        Debug.Log(TitleScreen.signUp);
    }

    public void Sign_In()
    {
        TitleScreen.sign = true;
    }

    public void Sign_In_close()
    {
        TitleScreen.signUp = false;
    }

    public void StartGame()
    {

    }

    public void GameGuide()
    {

    }

    public void ExitGame()
    {

    }
}
