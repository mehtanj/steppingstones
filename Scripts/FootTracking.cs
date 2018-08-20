using UnityEngine;
using System.IO;

public class FootTracking3 : MonoBehaviour {

	public GameObject footprint;

	private bool stepped_on = false;

	private RaycastHit hit;

	public string filename;

	Vector3 dwd = new Vector3(0, -0.08f, 0);

	void FixedUpdate(){
		// debug rays in scene view
		Debug.DrawRay (transform.position, dwd, Color.red, 0.2f);

		//cast ray downward from tracker
		if (Physics.Raycast (transform.position, dwd, out hit, 0.2f)) {

			//if collider of a stone is hit
			if (hit.collider != null && hit.collider.tag == "stone") {

				//call to Hit function
				Hit (hit.collider.gameObject.name, name[0]);
			}
		}

		//if raycast does not hit collider
		else {
			stepped_on = false; 

			//make foot model blue when stone is not stepped on
			footprint.GetComponent<Renderer> ().material.color = Color.blue;
		} 
	}

	void Hit(string objectname, char foot) {

		//if tracker is very close to stone (basically stepping on)
		if (transform.position.y <= 0.35 && !stepped_on) {
			stepped_on = true;

			//make foot model red when stone is stepped on
			footprint.GetComponent<Renderer> ().material.color = Color.red;

			//write stone number, foot (L or R), and foot position to text file
			using (StreamWriter sw = File.AppendText(filename))
			{
				sw.Write(objectname + " hit by " + foot + " foot at ");
				sw.Write(transform.position.x + ",");
				sw.Write(transform.position.y + ",");
				sw.WriteLine(transform.position.z);
			}

			//if foot is lifted off the stone
		} else if (transform.position.y > 0.35) {
			stepped_on = false;
			footprint.GetComponent<Renderer> ().material.color = Color.green;
		}
	}
}