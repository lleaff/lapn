using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class score_board : MonoBehaviour
{
	public bool Lost;
	private Animator board_anim;
	private string time_score;
	public Animator startButton;
	public Animator optionsButton;

	void Start ()
	{

		time_score = TimeManager.i.FormattedTime;
		Lost = score.Lost;
		if (Lost == true) {	
			board_anim = gameObject.GetComponent<Animator> ();
			Text[] texts = gameObject.GetComponentsInChildren <Text> ();
			texts [1].text = "You survived for " + time_score + "!";
			optionsButton.SetBool("isHidden", true);
			board_anim.SetBool ("show_score", true);
			startButton.SetBool("isHidden", false);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

