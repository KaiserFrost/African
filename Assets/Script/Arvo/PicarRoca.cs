using UnityEngine;
using System.Collections;

public class PicarRoca : MonoBehaviour {

	public float Salud;
	[HideInInspector]
	public float Salud2 = 10;
	[Header("Maxima distancia que quita salud")]
	public float MaxDis = 2;
	public GameObject[] Rocas;
	[Header("Tiempo en reaparecer la roca")]
	public float TiempoReaparece = 5;
	[HideInInspector]
	public float TiempoReaparece2 = 5;
	[Header("Stacks aleatorios")]
	public int Minimo = 1;
	public int Maximo = 100;
	private Camera camara;
	private bool Reaparece;

	// Use this for initialization
	void Start () {
		camara = GameObject.FindWithTag("FirstPersonCAM").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Ray ray = camara.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, MaxDis))
		{
			if(hit.collider.gameObject == gameObject)
			{
				if(Input.GetKey ("e"))
				{
					Salud2 -= Random.Range(2,11) * Time.deltaTime;
				}
			}
		}
		
		
		if(Salud2 <= 0.5f && !Reaparece)
		{
			gameObject.GetComponent<BoxCollider>().enabled = false;
			gameObject.GetComponent<MeshRenderer>().enabled = false;
				foreach(GameObject Roca in Rocas)
				{

				GameObject Clon = Instantiate(Roca,transform.position + transform.up * Random.Range(1,5) ,transform.rotation)as GameObject;
					Clon.GetComponent<Stacks>().Stack = Random.Range(Minimo,Maximo);
//					Destroy(gameObject);
				}

				Reaparece = true;
		}

		if(Reaparece)
		{
			TiempoReaparece2 -= Time.deltaTime;
			if(TiempoReaparece2 <= 0)
			{
				Salud2 = Salud;
				gameObject.GetComponent<BoxCollider>().enabled = true;
				gameObject.GetComponent<MeshRenderer>().enabled = true;
				TiempoReaparece2 = TiempoReaparece;
				Reaparece = false;
			}

		}
	}
}
