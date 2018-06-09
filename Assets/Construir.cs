using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Construir : MonoBehaviour {

    public string NombreDelTerreno = "terrain";
    public Tipo TipoDeConstruccion = Tipo.SobreOtro;
    public enum Tipo { SobreOtro, EnTerreno, OtroYTerreno, EnInterior }
    public string[] SiChocaCambiaColorPuedeConstruir;
    public bool Rotable = false;
    public float Rotacion = 0;
    public float FixAltura = 0;
    public float MaxDisCons = 10;

    private bool Build = false;
    private bool PoderRotar = false;
    private Inventory Inv;
    private Camera cam;
    private List<Material> materiales = new List<Material>();
    private MeshRenderer rend;


    // Use this for initialization
    void Start()
    {
        Inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        rend = GetComponent<MeshRenderer>();
        for (int i = 0; i < rend.materials.Length; i++)
        {
            materiales.Add(rend.materials[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit hit;
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));

        for (int i = 0; i < Inv.spawn.Length; i++)
        {
            if (Inv.spawn[i].Activado == true && Build == true)
            {
                Inv.spawn[i].PuedeConstruirse = true;
            }
            else
            {
                Inv.spawn[i].PuedeConstruirse = false;
            }
        }

        if (Physics.Raycast(ray, out hit, MaxDisCons))
        {
            switch (TipoDeConstruccion)
            {
                case Tipo.SobreOtro:
                    if (System.Array.IndexOf(SiChocaCambiaColorPuedeConstruir, hit.transform.name) != -1)
                    {
                        if (Rotacion > 0 && PoderRotar == false)
                        {
                            transform.rotation = hit.transform.rotation;
                            PoderRotar = true;
                        }
                        if (Input.GetKeyDown("r") && PoderRotar == true)
                        {
                            transform.Rotate(0, Rotacion, 0);
                        }
                        if (Rotacion == 0)
                        {
                            transform.rotation = hit.transform.rotation;
                        }
                        transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + FixAltura, hit.transform.position.z);
                        for (int i = 0; i < rend.materials.Length; i++)
                        {
                            rend.materials[i].color = Color.green;
                            Color color = rend.materials[i].color;
                            color.a = 0.3f;
                            rend.materials[i].SetColor("_Color", color);
                        }
                        Build = true;
                    }
                    else
                    {
                        PoderRotar = false;
                        for (int i = 0; i < rend.materials.Length; i++)
                        {
                            rend.materials[i].color = Color.red;
                            Color color = rend.materials[i].color;
                            color.a = 0.3f;
                            rend.materials[i].SetColor("_Color", color);
                        }
                        Build = false;
                        if (hit.transform.tag == NombreDelTerreno || hit.transform.tag != NombreDelTerreno)
                        {
                            transform.position = new Vector3(hit.point.x, hit.point.y + FixAltura, hit.point.z);
                        }
                    }
                    break;

                case Tipo.EnTerreno:
                    if (hit.transform.tag == NombreDelTerreno)
                    {
                        if (Rotacion > 0 && PoderRotar == false)
                        {
                            transform.rotation = hit.transform.rotation;
                            PoderRotar = true;
                        }
                        if (Input.GetKey("r") && PoderRotar == true)
                        {
                            transform.Rotate(0, Rotacion, 0);
                        }
                        if (Input.GetMouseButton(0) && PoderRotar == true)
                        {
                            transform.Rotate(0, Rotacion, 0);
                        }
                        if (Input.GetMouseButton(1) && PoderRotar == true)
                        {
                            transform.Rotate(0, -Rotacion, 0);
                        }
                        transform.position = new Vector3(hit.point.x, hit.point.y + FixAltura, hit.point.z);
                        for (int i = 0; i < rend.materials.Length; i++)
                        {
                            rend.materials[i].color = Color.green;
                            Color color = rend.materials[i].color;
                            color.a = 0.3f;
                            rend.materials[i].SetColor("_Color", color);
                        }
                        Build = true;
                    }
                    else
                    {
                        transform.position = new Vector3(hit.point.x, hit.point.y + FixAltura, hit.point.z);
                        for (int i = 0; i < rend.materials.Length; i++)
                        {
                            rend.materials[i].color = Color.red;
                            Color color = rend.materials[i].color;
                            color.a = 0.3f;
                            rend.materials[i].SetColor("_Color", color);
                            Debug.Log("não dá");
                        }

                        Build = false;
                    }
                    break;

                case Tipo.OtroYTerreno:

                    if (hit.transform.tag == NombreDelTerreno)
                    {
                        if (Rotacion > 0 && PoderRotar == false)
                        {
                            transform.rotation = hit.transform.rotation;
                            PoderRotar = true;
                        }
                        if (Input.GetKey("r") && PoderRotar == true)
                        {
                            transform.Rotate(0, Rotacion, 0);
                        }

                        transform.position = new Vector3(hit.point.x, hit.point.y + FixAltura, hit.point.z);
                        for (int i = 0; i < rend.materials.Length; i++)
                        {
                            rend.materials[i].color = Color.green;
                            Color color = rend.materials[i].color;
                            color.a = 0.3f;
                            rend.materials[i].SetColor("_Color", color);
                        }
                        Build = true;
                    }
                    else
                    {

                        if (System.Array.IndexOf(SiChocaCambiaColorPuedeConstruir, hit.transform.name) != -1)
                        {

                            //					if(Rotacion > 0 && PoderRotar == false)
                            //					{
                            //						transform.rotation = hit.transform.rotation;
                            //						PoderRotar = true;
                            //					}
                            //					if(Input.GetKeyDown ("r") && PoderRotar == true){
                            //						transform.Rotate(0,Rotacion,0);
                            //					}

                            transform.position = new Vector3(hit.point.x, hit.point.y + FixAltura, hit.point.z);
                            for (int i = 0; i < rend.materials.Length; i++)
                            {
                                rend.materials[i].color = Color.green;
                                Color color = rend.materials[i].color;
                                color.a = 0.3f;
                                rend.materials[i].SetColor("_Color", color);
                            }
                            Build = true;
                            if (hit.transform.tag != NombreDelTerreno)
                            {
                                transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + FixAltura, hit.transform.position.z);
                                transform.rotation = hit.transform.rotation;
                            }
                        }
                        else
                        {
                            transform.position = new Vector3(hit.point.x, hit.point.y + FixAltura, hit.point.z);
                            transform.GetComponent<Renderer>().material.color = Color.red;
                            Color color = GetComponent<Renderer>().material.color;
                            color.a = 0.3f;
                            GetComponent<Renderer>().material.SetColor("_Color", color);
                            Build = false;
                            PoderRotar = false;
                        }
                    }

                    break;

                case Tipo.EnInterior:
                    if (System.Array.IndexOf(SiChocaCambiaColorPuedeConstruir, hit.transform.name) != -1)
                    {

                        if (Rotacion > 0 && PoderRotar == false)
                        {
                            transform.rotation = hit.transform.rotation;
                            PoderRotar = true;
                        }
                        if (Input.GetKey("r") && PoderRotar == true)
                        {
                            transform.Rotate(0, Rotacion, 0);
                        }

                        transform.position = new Vector3(hit.point.x, hit.point.y + FixAltura, hit.point.z);
                        for (int i = 0; i < rend.materials.Length; i++)
                        {
                            rend.materials[i].color = Color.green;
                            Color color = rend.materials[i].color;
                            color.a = 0.3f;
                            rend.materials[i].SetColor("_Color", color);
                        }
                        Build = true;
                    }
                    else
                    {
                        transform.position = new Vector3(hit.point.x, hit.point.y + FixAltura, hit.point.z);
                        for (int i = 0; i < rend.materials.Length; i++)
                        {
                            rend.materials[i].color = Color.red;
                            Color color = rend.materials[i].color;
                            color.a = 0.3f;
                            rend.materials[i].SetColor("_Color", color);
                        }
                        Build = false;
                        PoderRotar = false;
                    }

                    break;
            }
        }
    }
}
