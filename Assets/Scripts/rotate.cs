using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    	//transform.position = new Vector3(1,1,1);
    }

    // Update is called once per frame
    void Update()
    {
    	transform.Rotate(0,1,0);    
    }
}
