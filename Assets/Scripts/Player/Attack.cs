using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int attackDamage;

    public float attackRange;

    public float attackRate;



    private void OnTriggerStay2D(Collider2D collider)
    {
        //���û�� ? �Ļ���unity�ն˻ᵯ������ı���
        //NullReferenceException: Object reference not set to an instance of an object
        //Attack.OnTriggerStay2D(UnityEngine.Collider2D collider)
        
        //�����ǻ�ȡ��ײ�壨Ҳ����Ұ��Character������ڵ�TakeDamage�ķ�������Ϊ����TakeDamage����Ĳ�����������Ϊthis
        collider.GetComponent<Character>()?.TakeDamage(this);

    }

}
