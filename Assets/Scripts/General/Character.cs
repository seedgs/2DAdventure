using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    public float maxHealth;

    public float currentHealth;

    public float invulnerableDuration;

    private float invulnerableCounter;

    public bool invulnerable;

    public UnityEvent<Transform> OntakeDamage;

    public UnityEvent OnDead;

    //Awake()Ϊһ����������ִֻ��һ��
    //������������������buffer�ͻ���ӣ���Ϊ����ֻ��һ�Σ����Ե�bufferҲ��ֻ����һ��




    //������ʱ��ʼִ��
    //������������Ұ������ײ��ʱ��ʼִ��
    //������������Ƕ��
    private void Start()
    {
        //��ǰѪ���������Ѫ��
        currentHealth = maxHealth;
    }


    //ÿһ֡����Ҫִ���������������ʱ
    private void Update()
    {
        //���޵е�ʱ��
        if (invulnerable)
        {
            //��������ʼ����ʱ
            invulnerableCounter -= Time.deltaTime;
            //��������Ϊ0��ʱ��
            if (invulnerableCounter <= 0)
            {
                //�޵�ֹͣ
                invulnerable = false;
            }
        }
    }

    //��������ǻ�ȡAttack�ű��Ĳ�����������Ϊattacker
    public void TakeDamage(Attack attacker)
    {
       

        if (invulnerable)
            return;

        if (currentHealth > 0)
        {
            //Debug.Log(attacker.attackDamage);
            currentHealth -= attacker.attackDamage;
            OnTriggerInvlnerable();
            OntakeDamage?.Invoke(attacker.transform);
        }
        else 
        {
            currentHealth = 0;
            //����
            OnDead?.Invoke();
        }
        
    }


    public void OnTriggerInvlnerable()
    {
        if (!invulnerable)
        {
            invulnerable = true;
            invulnerableCounter = invulnerableDuration;
        }
    }

 
}
