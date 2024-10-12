using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarPatrolState : BaseState
{


    public override void OnEnter(EnemyController enemy)
    {
        //�󶨴��Σ��Ϳ���ͨ��currentEnemy����EnemyController�ű��Ĳ���
        currentEnemy = enemy;
        
    }

     
     
    public override void LogicUpdate()
    {
        //������Ϸ�󣬵��˴���Ѳ��״̬��һ��������Ҿͽ���׷�������ܣ�״̬
        //base.currentEnemy.Move();
        currentEnemy.anim.SetBool("isWalk", currentEnemy.isWalk);
        currentEnemy.anim.SetTrigger("Walk");
        currentEnemy.move();

        //��Ұ���isGroundΪ0��Ҳ����Ұ���������µ�ʱ��Ұ��ֹͣѲ�ߣ�����ȴ�ģʽ
        //������ߵȴ�
        
        currentEnemy.anim.SetBool("isLeftGround", currentEnemy.isNotLeftWait);
        currentEnemy.wait();


        //�����ұߵȴ�
        currentEnemy.anim.SetBool("isRightGround", currentEnemy.isNotRightWait);
        currentEnemy.wait();


        //�������ܶ���
        currentEnemy.anim.SetBool("isRun", currentEnemy.isRun);
        currentEnemy.Run();
    }




    public override void PhysicsUpdate()
    {
        
    }


    public override void OnExit()
    {
        
    }
}
