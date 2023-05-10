using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalDistance : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;

    public float distance;

    private void Update()
    {
        Distance();
    }
    public void Distance()
    {
        distance = Vector3.Distance(player.transform.position, enemy.transform.position);
    }
}
