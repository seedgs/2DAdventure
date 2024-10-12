using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunPhysicsCheck : MonoBehaviour
{
    [Header("��ײԲ��ƫ����")]
    public Vector2 RunOffset;

    [Header("��ײ�뾶")]
    public float CheckRunRadius;

    [Header("ѡ����ײͼ��")]
    public LayerMask Runlayer;

    [Header("��ײ�ж�")]
    public bool isRunCheck;

    private void Update()
    {
        EnemyRunCheck();
    }

    public void EnemyRunCheck()
    {
        //������
        isRunCheck = Physics2D.OverlapCircle((Vector2)transform.position + RunOffset, CheckRunRadius, Runlayer);
    }

    //������ײ�뾶����
    private void OnDrawGizmos()
    {
        //DrawWireSphere�ǻ���һ��Բ���뾶���뾶���ȣ�
        Gizmos.DrawWireSphere((Vector2)transform.position + RunOffset, CheckRunRadius);
    }
}
