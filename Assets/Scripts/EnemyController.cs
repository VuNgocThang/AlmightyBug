using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : PlayerManager
{
    public CalDistance calDistance;
    public bool canBlock;
    public float speed = 1f;

    private void FixedUpdate()
    {
        UpdateHp();
        if (calDistance.distance > 10f)
        {
            if (IsGrounded())
            {
                transform.Translate(Vector3.left * speed * Time.fixedDeltaTime);
            }
        }
    }


}
