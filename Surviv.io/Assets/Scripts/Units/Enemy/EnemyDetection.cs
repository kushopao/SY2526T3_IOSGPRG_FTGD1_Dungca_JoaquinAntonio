using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Unit>())
        {
            this.GetComponentInParent<EnemyFSM>().targetList.Add(other.gameObject.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Unit>())
        {
            this.GetComponentInParent<EnemyFSM>().targetList.Remove(other.gameObject.transform);
        }
    }
}
