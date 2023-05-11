using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : PlayerManager
{
    public CalDistance calDistance;

    private void FixedUpdate()
    {
        if (calDistance.distance > 10f)
        {
            Debug.Log("move");
        }
        else
        {
            Debug.Log("use skill");
        }
    }
    public void Move()
    {

    }

    public void UseSkill()
    {

    }


}
