using UnityEngine;
using System.Collections;

public class SM : MonoBehaviour
{

    public static SM i = null; /* Scene manager instance */

	public Vector2 CellDimensions = new Vector2(5f, 5f);

	private int money;
	public int Money {
		get {
			return money;
		}
		set {
			money = value;
		}
	}
	public bool TryPay(int cost) {
		if (cost >= money) {
			money -= cost;
			return true;
		} else {
			return false;
		}
	}

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
