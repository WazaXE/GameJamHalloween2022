using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupKey : MonoBehaviour
{

    [SerializeField] BoxCollider box;
    [SerializeField] GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            box.enabled = true;
            Destroy(gameObject);
        }
    }
}
