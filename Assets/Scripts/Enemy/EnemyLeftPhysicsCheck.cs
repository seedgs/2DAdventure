using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLeftPhysicsCheck : MonoBehaviour
{
    [Header("����ײԲ��ƫ����")]
    public Vector2 LeftOffset;

    [Header("����ײ�뾶")]
    public float CheckLeftRadius;

    [Header("ѡ����ײͼ��")]
    public LayerMask GroundLeftlayer;

    [Header("����ײ�ж�")]
    public bool isLeftGround;

    private void Update()
    {
        EnemyLeftCheck();
    }

    public void EnemyLeftCheck()
    {
        //������
        isLeftGround = Physics2D.OverlapCircle((Vector2)transform.position + LeftOffset, CheckLeftRadius, GroundLeftlayer);
    }

    //������ײ�뾶����
    private void OnDrawGizmos()
    {
        //DrawWireSphere�ǻ���һ��Բ���뾶���뾶���ȣ�
        Gizmos.DrawWireSphere((Vector2)transform.position + LeftOffset, CheckLeftRadius);

    }
}
