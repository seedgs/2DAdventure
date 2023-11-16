using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

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

    //title����
    [Header("��������")]
    public float speed;

    private float runSpeed ;

    private float walkSpeed ;

    public float jumpForce;

    public bool IsCrouch;

    private Vector2 originalOffset;

    private Vector2 originalSize;

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

        originalOffset = coll.offset;

        originalSize = coll.size;

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
        Move();
    }

    //�ƶ�����
    public void Move()
    {
        //�����¶׵�ʱ��ſ����ƶ�
        if(IsCrouch!=true)
        rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.velocity.y);

        //����1
        //�������������Sprite Renderer�����Flip��X���Ǳ���ѡ��Ҳ���ǲ���ֵ��True��Flase�����ж�
        if(inputDirection.x > 0)
        {
            sp.flipX = false;
        }
        else if(inputDirection.x < 0)
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

        IsCrouch = inputDirection.y < -0.5f && physicsCheck.isGround ;

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


}
