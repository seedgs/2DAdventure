 

public abstract class BaseState 
{
    //����EnemyController�ű�������ΪcurrentEnemy
    protected EnemyController currentEnemy;

    //��Ҫ���ĸ�״̬����EnemyController�Ϳ����ڶ�Ӧ�������ڴ��룬������
    public abstract void OnEnter(EnemyController enemy);

    public abstract void LogicUpdate();

    public abstract void PhysicsUpdate();

    public abstract void OnExit();


}
