using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerScript : MonoBehaviour
{

    [SerializeField] private Light pointLight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.value > 0.98)
        {
            if (pointLight.enabled == true)
            {
                pointLight.enabled = false;
            }
            else
            {
                pointLight.enabled = true;
            }
        }
    }
}
