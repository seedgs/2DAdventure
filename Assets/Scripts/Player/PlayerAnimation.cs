using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;

    private Rigidbody2D rb;

    private PhysicsCheck pc;

    private PlayerController pcl;

    private void Awake()
    {
        //��ȡAnimator(����)���
        anim = GetComponent<Animator>();

        //��ȡRigidbody2D(����)���
        rb = GetComponent<Rigidbody2D>();

        //��ȡPhysicsCheck�ű����
        pc = GetComponent<PhysicsCheck>();

        //��ȡPlayerController�ű����
        pcl = GetComponent<PlayerController>();
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

        //Velocity��Animator���Զ���Ĳ���
        //rb.velocity.y���������ƶ���ʱ���������������ٶ�
        anim.SetFloat("VelocityY", rb.velocity.y);

        //IsGround�Ǽ������Ǵ�������Ĳ���ֵ��Ϊtrue��ʾ�����ڵ����ϣ�Ϊfalse��ʾ���ﲻ�ڵ�����
        //�����ڵ����ϵ�ʱ�򶯻�����
        anim.SetBool("IsGround", pc.isGround);

        //�¶׶�������
        anim.SetBool("IsCrouch", pcl.IsCrouch);
    }
}
