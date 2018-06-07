using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AumentaStatus : MonoBehaviour {


    void OnTriggerEnter(Collider other)
    {
        HungryTest.hungry += 200;
        Destroy(this.gameObject);  
    }

}
