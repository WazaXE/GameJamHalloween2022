using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{

    [SerializeField] private GameObject player;

    private Animation anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            anim.Play("OpenDoor");
        }


    }


    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject == player)
        {
            anim.Play("CloseDoor");
        }
    }
}
