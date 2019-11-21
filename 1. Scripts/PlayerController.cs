using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject bulletPrefeb;
    public GameObject emptyMessage;
    public Transform bulletSpawn;
    public bool canShot;
    private int bulletNum;

    [SerializeField]
    private float walkSpeed;


    [SerializeField]
    private float lookSensitivity;

    [SerializeField]
    private Rigidbody myRigid;
    private Animator myAni;

    // Use this for initialization
    void Start()
    {
        myRigid = GetComponent<Rigidbody>();
        myAni = GetComponent<Animator>();
        bulletNum = 5;
        emptyMessage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        GetShot();
    }

    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetAxisRaw("Horizontal") != 0f || Input.GetAxisRaw("Vertical") != 0f)
        {
            if (Input.GetAxisRaw("Vertical") > 0f)
            {
                myAni.SetBool("Walk", true);
            }

            else if (Input.GetAxisRaw("Vertical") < 0f)
            {
                myAni.SetBool("Walk", true);
            }

            else if (Input.GetAxisRaw("Horizontal") < 0f)
            {
                myAni.SetBool("Walk", true);
            }

            else if (Input.GetAxisRaw("Horizontal") > 0f)
            {
                myAni.SetBool("Walk", true);
            }
        }
        else
        {
            myAni.SetBool("Walk", false);
        }

        transform.Translate(new Vector3(horizontal, 0, vertical) * walkSpeed * Time.deltaTime);
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0));

        if (bulletNum == 0)
        {
            canShot = false;
            Invoke("bulletRefill", 3f);
            emptyMessage.SetActive(true);
        }

        else
        {
            canShot = true;
            emptyMessage.SetActive(false);
        }
    }

    private void CharacterRotation()
    {
        // 좌우 캐릭터 회전
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY));
    }

    void GetShot()
    {
        if (Input.GetMouseButtonDown(0) && canShot == true)
        {
            bulletNum--;
            //myAni.Play("Shoot_single", -1, 0);
            myAni.SetBool("GetShot", true);
            Debug.Log("공격");

            var bullet = (GameObject)Instantiate(bulletPrefeb, bulletSpawn.position, bulletSpawn.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 15;
            Destroy(bullet, 2.0f);
        }
        else myAni.SetBool("GetShot", false);
    }

    void bulletRefill()
    {
        bulletNum = 10;
    }
}
