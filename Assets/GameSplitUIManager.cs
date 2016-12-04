using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using trinus;

public class GameSplitUIManager : MonoBehaviour {

	public GameObject gamePage;
	static Slider healthSlider;
	static Text score;
	static Image damage;
	static Text gameOver;
	static Image fader;

	static bool gameOverFlag;
	// Use this for initialization
	void Start () {

		healthSlider = gamePage.transform.FindChild ("HealthUI/HealthSlider").GetComponent<Slider> ();
		score = gamePage.transform.FindChild ("ScoreText").GetComponent<Text> ();
		damage = gamePage.transform.FindChild ("DamageImage").GetComponent<Image> ();
		gameOver = gamePage.transform.FindChild ("GameOverText").GetComponent<Text> ();
		fader = gamePage.transform.FindChild ("ScreenFader").GetComponent<Image> ();

		gameOverFlag = false;

		setHealth ();
		setScore ();
	}
	
	void Update(){
		if (gameOverFlag) {
			fader.color = Color.Lerp (fader.color, Color.black, Time.deltaTime * 2);
			gameOver.color = Color.Lerp (gameOver.color, Color.white, Time.deltaTime);
		}
	}
	public static void setHealth(){
		setHealth (100);
	}
	public static void setHealth(int h){
		healthSlider.value = h;
	}
	public static void setScore(){
		setScore (0);
	}
	public static void setScore(int val){
		string text = string.Format (Localization.getText ("labelScore"), val);
		score.text = text;
	}
	public static void setDamageColor(Color color){
		if (damage != null)
			damage.color = color;
	}
	public static Color getDamageColor(){
		if (damage == null)
			return Color.red;
		return damage.color;
	}
	public static void setGameOver(){
		gameOverFlag = true;
	}
}
