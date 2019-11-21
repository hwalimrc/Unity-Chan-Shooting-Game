using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallowCam : MonoBehaviour
{
    //추적할 대상
    public Transform target;
    //카메라와의 거리
    public float dist = 4f;

    //카메라 회전 속도
    public float xSpeed = 220.0f;
    public float ySpeed = 100.0f;

    //카메라 초기 위치
    private float x = 0.0f;
    private float y = 0.0f;

    //y값 제한 (위 아래 제한)
    public float yMinLimit = -20f;
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
        Cursor.lockState = CursorLockMode.Locked; //커서 고정
        Vector3 angles = transform.eulerAngles;

        x = angles.y;
        y = angles.x;
    }

    void Update()
    {
        //카메라 회전속도 계산
        x += Input.GetAxis("Mouse X") * xSpeed * 0.015f;
        y -= Input.GetAxis("Mouse Y") * ySpeed * 0.015f;

        //앵글값 정하기(y값 제한)
        y = ClampAngle(y, yMinLimit, yMaxLimit);

        //카메라 위치 변화 계산
        Quaternion rotation = Quaternion.Euler(y, x, 0);
        Vector3 position = rotation * new Vector3(0, 0.9f, -dist) + target.position + new Vector3(0.0f, 0, 0.0f);

        transform.rotation = rotation;
        target.rotation = Quaternion.Euler(0, x, 0);
        transform.position = position;

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
        if (pause == true) // 게임이 멈췄을 때,
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
