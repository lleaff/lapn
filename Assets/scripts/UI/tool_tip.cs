using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class tool_tip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{

	public GameObject tooltip;

	public void OnPointerEnter (PointerEventData eventData) {
		tooltip.SetActive (true);
	}

	public void OnPointerExit (PointerEventData eventData) {
		tooltip.SetActive (false);
	}
}
