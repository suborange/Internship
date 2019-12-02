using System; 

using UnityEngine;


//maybe stays idle for number of seconds? add where they can change the line to other conditions. 

public class IdleState : BaseAIState
{

    private EnemyAI _obj;


    public IdleState(EnemyAI enemy_obj) : base(enemy_obj.gameObject)
    {
        _obj = enemy_obj;
    }

    public override Type Tick()
    {
        //idle state
        //do nothing




        return null;
    }
}
