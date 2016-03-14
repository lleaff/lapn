using UnityEngine;
using System.Collections;

public class GM : MonoBehaviour
{

	public static GM i = null; /* Game manager instance */

	void Awake()
	{
		if (i == null) {
			i = this;
		} else if (i != this) {
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);
	}

	void Update()
	{
	
	}
}
