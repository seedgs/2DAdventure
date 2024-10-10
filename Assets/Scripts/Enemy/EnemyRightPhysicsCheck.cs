using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRightPhysicsCheck : MonoBehaviour
{
    [Header("����ײԲ��ƫ����")]
    public Vector2 RightOffset;

    [Header("����ײ�뾶")]
    public float CheckRightRadius;

    [Header("����ײ�뾶")]
    public LayerMask GroundRightlayer;

    [Header("����ײ�ж�")]
    public bool isRightGround;

    private void Update()
    {
        EnemyRightCheck();
    }

    public void EnemyRightCheck()
    {
        //������
        isRightGround = Physics2D.OverlapCircle((Vector2)transform.position + RightOffset, CheckRightRadius, GroundRightlayer);
    }

    //������ײ�뾶����
    private void OnDrawGizmos()
    {
        //DrawWireSphere�ǻ���һ��Բ���뾶���뾶���ȣ�
        Gizmos.DrawWireSphere((Vector2)transform.position + RightOffset, CheckRightRadius);

    }
}
