using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TalarArbol : MonoBehaviour {

	public float Vida;
	[Header("Maximp")]
	public float MaxDis = 2;
	public GameObject Semente;
	public GameObject[] Troncos;
	[Header("Tempo para aparecer os troncos")]
	public float Tempo;
	private Camera camara;

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
				if(Input.GetKey ("e") && gameObject.transform.localScale.y >= 1)
			    {
					Vida -= Random.Range(2,11) * Time.deltaTime;
			    }
			}
		}


		if(Vida <= 0.5f)
		{
			Tempo -= Time.deltaTime;
			GetComponent<Rigidbody>().useGravity = true;
			GetComponent<Rigidbody>().isKinematic = false;

			if(Tempo <= 0)
			{
				foreach(GameObject Tronco in Troncos)
				{
					Destroy(gameObject);
					GameObject Clon = Instantiate(Tronco,transform.position + transform.up * Random.Range(0,10) ,transform.rotation)as GameObject;
					Clon.GetComponent<Stacks>().Stack = Random.Range(2,51);
				}
				GameObject ClonSemilla = Instantiate(Semente,transform.position,transform.rotation)as GameObject;
				ClonSemilla.GetComponent<Stacks>().Stack = Random.Range(1,4);
			}
		}
	}
}
