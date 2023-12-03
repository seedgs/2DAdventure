using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

using UnityEditor.Experimental.GraphView;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    //���Բ��Բ��Բ��Բ���
    void Start()
    {

    }

    // Update is called once per frame

    //ÿһ֡����ְ���������
    void Update()
    {
        //��ȡinputControl����� GamePalyer ����� Move �� Vector2 ���inputDirection���������Vector2��ҪReadValue

        inputDirection = inputControl.GamePlayer.Move.ReadValue<Vector2>();
    }

    //��ȡ PlayerInputControl(�����豸)�����inputControl
    //PlayerInputControl��Seetings�ļ��������InputSystem�ļ�����
    public PlayerInputControl inputControl;

    public Vector2 inputDirection;

    //��Ҫ��ñ��������ʹ�����������Ӧ�����ͣ�һ������
    //��Ҫ���PhysicsCheckl����Ķ����������+�������������⣩
    private PhysicsCheck physicsCheck;

    public Rigidbody2D rb;

    public SpriteRenderer sp;

    private CapsuleCollider2D coll;

    //��ȡ�������
    private PlayerAnimation playerAnimation;

    //title����
    [Header("��������")]
  
    private float runSpeed;

    private float walkSpeed;

    public float speed;

    public float jumpForce;

    public float hurtForce;

    private Vector2 originalOffset;

    private Vector2 originalSize;

    [Header("״̬")]
    public bool IsHurt;

    public bool IsCrouch;

    public bool IsDead;

    public bool isAttack;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputControl = new PlayerInputControl();

        //��ȡphysicsCheck���
        physicsCheck = GetComponent<PhysicsCheck>();
        //started�ǾͰ�����һ��
        //��Jump�������������ӵ��㰴�����µİ������µ���һ�̣�started������ִ��
        inputControl.GamePlayer.Jump.started += Jump;

        coll = GetComponent<CapsuleCollider2D>();

        playerAnimation = GetComponent<PlayerAnimation>();

        originalOffset = coll.offset;

        originalSize = coll.size;

        //����
        inputControl.GamePlayer.Attack.started += Attack;

        //�ܲ�����·�л�
        #region

        //��Awake��������д����Ϊ��Awake��������Ϸ��������ͻ�ִ�У������Ϸ����رպ���ִ�У�AwakeҲ���ٴο���
        runSpeed = speed;

        walkSpeed = speed / 2.5f;
        //ctxΪ�ص�����
        //performedΪ���°���
        inputControl.GamePlayer.Walkbutton.performed += ctx =>
        {
            if (physicsCheck.isGround)
            {
                speed = walkSpeed;
            }
        };

        //canceledΪ�ɿ�����
        inputControl.GamePlayer.Walkbutton.canceled += ctx =>
        {
            if (physicsCheck.isGround)
            {
                speed = runSpeed;
            }
        };
        #endregion


    }



    /*//��ײ����
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(collision.name);
    }*/


    //��Ծ����


    //��ǰ����������ʱ��
    private void OnEnable()
    {
        //������Ҳ��������
        inputControl.Enable();
    }

    //��ǰ����رյ�ʱ��
    private void OnDisable()
    {
        //������Ҳ���Źر�
        inputControl.Disable();
    }

    private void FixedUpdate()
    {
        if(!IsHurt)
        Move();
    }

    //�ƶ�����
    public void Move()
    {
        //�����¶׵�ʱ��ſ����ƶ�
        if (IsCrouch != true)
            rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.velocity.y);

        //����1
        //�������������Sprite Renderer�����Flip��X���Ǳ���ѡ��Ҳ���ǲ���ֵ��True��Flase�����ж�
        if (inputDirection.x > 0)
        {
            sp.flipX = false;
        }
        else if (inputDirection.x < 0)
        {
            sp.flipX = true;
        }

        /*
  
        //����2
        //����ǳ���ı䷭תx�ķ���
        //������ҪFaceDir���������ͣ�������Ϊint�������ͣ�����localScale.x��float�������ͣ�������Ҫǿ��ת����int
        int FaceDir = (int)transform.localScale.x;

        //��inputDirection.x > 0 ʱ����ʵ�ڰ������Ҽ�����ʱ��FaceDirΪ1
        if (inputDirection.x > 0)
        {
            FaceDir = 1;
            //��inputDirection.x < 0 ʱ����ʵ�ڰ������Ҽ�����ʱ��FaceDirΪ-1
        }
        else if(inputDirection.x < 0)
        {
            FaceDir = -1;
        }

        //Ҫ�������������豸�����̡��ֱ���ʵ�ַ�ת��˼·�ǰ���������x�ᷭת��������Ҫ��ȡ��tranform�����Scale��x�����
        //������ǻ�ȡ��Scale����ת���������x����-1����1��ʱ���Ǿ���ת�ģ�����Ҫ�ı�x�����ֵ��������ΪFackDir��������y��z���ֲ���
        transform.localScale = new Vector3(FaceDir, 1, 1);

        */


        //inputDirectionΪ�����˵ı�����x����Ϊ�����ƶ���y����Ϊ�������¶�
        //yΪ���������¶ף�yΪ����������Ծ

        IsCrouch = inputDirection.y < -0.5f && physicsCheck.isGround;

        if (IsCrouch)
        {
            //�޸���ײ���С��λ��
            coll.offset = new Vector2(-0.09440199f, 0.939046f);
            coll.size = new Vector2(0.4867882f, 1.6f);
        }
        else
        {
            //��ԭ֮ǰ��ײ����
            coll.size = originalSize;
            coll.offset = originalOffset;
        }

    }



    //��������Ծ�����ǲ���һֱ��ͣ����������Ҫ���ϵķ�����ʩ��һ����
    private void Jump(InputAction.CallbackContext context)
    {

        //physicsCheck.isGround����������ײ
        if (physicsCheck.isGround)

            //������ʩ��һ����
            //Debug.Log("Jump");
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    //���˺�ʩ��һ��������
    public void GetHurt(Transform attacker)
    {
        IsHurt = true;

        //ֹͣ�˶�
        rb.velocity = Vector2.zero;

        Vector2 dir = new Vector2(transform.position.x - attacker.position.x, 0).normalized;
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
    }


    public void PlayerDead()
    {
        IsDead = true;
        Debug.Log("1");
        inputControl.GamePlayer.Disable();
    }

    private void Attack(InputAction.CallbackContext context)
    {
        playerAnimation.PlayerAttack();
        isAttack = true;

    }
}