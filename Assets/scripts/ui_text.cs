using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ui_text : MonoBehaviour {

	private GameObject globalValue;
	public Text carrot;
	public Text time;
	public Text money;
	private float m_time;

	void Start () {
		globalValue = GameObject.Find ("GlobalValue");
	}
		
	void Update () {
		
		m_time += Time.deltaTime;
		carrot.text = globalValue.GetComponent<globalValue> ().Carrots.ToString ();
		time.text = m_time.ToString ();
		money.text = globalValue.GetComponent<globalValue> ().Money.ToString();
	}
}
