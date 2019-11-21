using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    private Rigidbody rbody;
    private Animator ani;
    private float speed = 5.0f;

    private float horizontalMove = 0f;
    private float verticalMove = 0f;

    // Use this for initialization
    void Start()
    {
        //컴포넌트를 가져온다.
        rbody = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");//좌우 입력. -1이 왼쪽. 1이 오른쪽
        float v = Input.GetAxisRaw("Vertical"); //상하 입력. -1이 아래, 1이 위

        if (!(h == 0 && v == 0)) //방향키를 입력한경우
        {
            //ani.SetFloat("Direction", -1);
            Vector3 move = new Vector3(h, 0, v); //볼 방향을 가리킨다.
            Quaternion dir = Quaternion.LookRotation(move.normalized);//해당 방향을 보도록 회전하는 Quaternion 변수 생성
            dir.x = 0; //몸체 방향은 y축만 회전하면 되므로 x,z축은 0으로 강제고정.
            dir.z = 0;
            transform.rotation = dir;//현재 객체의 방향을 생성한 Quaternion 변수로 맞춤.
        }

        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        Vector3 move = new Vector3(horizontalMove, 0, verticalMove);
        rbody.MovePosition(rbody.position + move * speed * Time.deltaTime); //이동할 위치 = 현재 위치 + 위치값

        if (horizontalMove == 0 && verticalMove == 0)
        { ani.SetBool("RunChk", false); }//방향키가 입력되있지 않은경우 현재 이동상태를 false로 둔다.

        else
        {
            ani.SetBool("RunChk", true); //방향키가 입력된경우 현재 이동상태를 true로 둔다.       
            //isAttack = false;
        }
    }


}
