using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;

    private Rigidbody2D rb;


    private void Awake()
    {
        //�ʼ��Ҫ��ȡ���
        anim = GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //ÿһ֡��Ҫִ��
        setAnimation();
    }

    public void setAnimation()
    {
        //Velocity��Animator���Զ���Ĳ���
        //rb.velocity.x���������ƶ���ʱ���������ĺ����ٶ�
        anim.SetFloat("VelocityX", Mathf.Abs(rb.velocity.x));
    }
}
