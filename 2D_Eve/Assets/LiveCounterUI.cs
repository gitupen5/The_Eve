using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LiveCounterUI : MonoBehaviour {

	private Text livesText;

	void Awake () {
		livesText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		livesText.text = "LIVES: " + GameMaster.RemainingLives.ToString();
	}
}
 