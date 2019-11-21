using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class test : MonoBehaviour
{
    //추적할 대상
    public Transform target;
    //카메라와의 거리   
    private float dist = 6f;

    //카메라 회전 속도
    public float xSpeed = 220.0f;
    public float ySpeed = 100.0f;

    //카메라 초기 위치
    private float x = 0.0f;
    private float y = 0.0f;

    //y값 제한
    public float yMinLimit = -10f;
    public float yMaxLimit = 80f;

    private bool isPause;
    public GameObject PausePanel;

    //앵글의 최소,최대 제한
    float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }

    void Start()
    {
        //커서 숨기기
        Cursor.lockState = CursorLockMode.Locked; // 마우스 커서 숨기기
        Cursor.visible = false;
        PausePanel.SetActive(false);

        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    void Update()
    {
        if (target)
        {
            //마우스 스크롤과의 거리계산
            dist = 2;
            //카메라 회전속도 계산
            x += Input.GetAxis("Mouse X") * xSpeed * 0.015f;

            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.015f;

            //앵글값 정하기
            //y값의 Min과 MaX 없애면 y값이 360도 계속 돎
            //x값은 계속 돌고 y값만 제한
            y = ClampAngle(y, 0, 30);
            x = Mathf.Clamp(x, -30f, 30f);

            //카메라 위치 변화 계산
            Quaternion rotation = Quaternion.Euler(y, x, 0);
            Vector3 position = rotation * new Vector3(0, 1.0f, -dist) + target.position + new Vector3(0.0f, 0, 0.0f);

            transform.rotation = rotation;
            transform.position = position;        
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (isPause == false) // 현재 게임이 진행중일 때,
            {
                isPause = true;
            }
            else if (isPause == true) // 현재 게임이 멈췄을 때,
            {
                isPause = false;
            }
            else
                Debug.Log("error");
        }

        PauseGame(isPause);
    }

    void PauseGame(bool pause)
    {
        if(pause == true) // 게임이 멈췄을 때,
        {            
            Cursor.lockState = CursorLockMode.None; // 마우스 커서 숨기기
            Cursor.visible = true;
            PausePanel.SetActive(true);

            Time.timeScale = 0;
        }

        else // 게임이 진행중일 때,
        {
            PausePanel.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked; // 마우스 커서 숨기기
            Cursor.visible = false;
            Time.timeScale = 1;
        }
    }

    public void ResumeGame()
    {
        isPause = false;
    }

    public bool Paused()
    {
        if (isPause == true)
            return true;
        
        else
            return false;        
    }
}
