using UnityEngine;
using System.Collections;

public class AnimalEstado : MonoBehaviour
{

    public enum itemType
    {
        Nada,
        Cavalo,
        Lobo
    };

    public itemType ItemType = itemType.Nada;
    public int Vida = 5;
    public bool Morto = false;
    public GameObject CavaloMorto;


    // Update is called once per frame
    void Update()
    {
        if (ItemType == itemType.Cavalo && Vida < 1)
        {
            Instantiate(CavaloMorto, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        if (ItemType == itemType.Lobo && Vida < 0)
        {
            Debug.Log("Lobo morreu");
        }
    }
}