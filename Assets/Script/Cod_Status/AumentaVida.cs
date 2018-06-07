using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AumentaVida : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        HungryTest.health += 200;
        Destroy(this.gameObject);
    }

}
