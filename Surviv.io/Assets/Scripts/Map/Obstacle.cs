using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<EnemyFSM>())
        {
            if (other.gameObject.GetComponent<Unit>().unitType == UnitType.Enemy)
            {
                other.gameObject.GetComponent<EnemyFSM>().ResetWanderPointAndTravelTime();
            }
        }
    }
}
