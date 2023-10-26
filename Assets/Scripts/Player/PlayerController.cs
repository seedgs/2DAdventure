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
    //title����
    [Header("��������")]
    public float speed;

    public float jumpForce;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputControl = new PlayerInputControl();

        //��ȡphysicsCheck���
        physicsCheck = GetComponent<PhysicsCheck>();
        //started�ǾͰ�����һ��
        //��Jump�������������ӵ��㰴�����µİ������µ���һ�̣�started������ִ��
        inputControl.GamePlayer.Jump.started += Jump;
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
    }
    private void Jump(InputAction.CallbackContext context)
    {


        if (physicsCheck.isGround)

        //Debug.Log("Jump");
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }


}
