using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace smoke{
	public class GM : MonoBehaviour {
		public int life = 1;
		public static GM current;
		public bool canTouch;
		public int originalLife = 15;
		public bool gameState = false;

		public Text lifeText;
		public Text highestText;
		public Text scoreText;
		public float nowScore;//true score
		public float score;//life

		void Awake(){
			if (current == null)
				current = this;
			else if(current != this)
				Destroy (gameObject);
			score = originalLife;
		}

		void Start () {
			canTouch = true;
			nowScore = 0;
		}

		void Update () {
			if (score < 0) {
				Reset();
			}
			showScore ();//showscore
			lifeText.text = "LIFE: " + ((int)score).ToString();//showlife
			highestText.text = "HIGHEST: " + (int)PlayerPrefs.GetFloat ("Highest");//showhighest
		}

		public void showScore(){
			if (nowScore < (cameraScript.current.transform.position.y + 17)) {
				nowScore = cameraScript.current.transform.position.y + 17;
			}
			scoreText.text = "SCORE: " + (int)nowScore;
		}

		public void startDec(){
			Debug.Log ("Start dec");
			InvokeRepeating ("decScore", 0.3f, 1f);
			CancelInvoke ("incScore");
		}

		public void stopDec(){
			Debug.Log ("Stop Dec");
			InvokeRepeating ("incScore", 0.3f, 1f);
			CancelInvoke("decScore");
		}

		public void reward(){
			Debug.Log ("reward score");
			score += 1F;
		}
		void incScore(){
			score += 0.3F;
		}

		void decScore(){
			score --;
		}

		public void Reset(){
			gameState = false;
			setHighest ();
			nowScore = 0;
			Application.LoadLevel (Application.loadedLevel);
		}

		public void setHighest(){
			if(PlayerPrefs.HasKey("Highest") == false){
				PlayerPrefs.SetFloat("Highest",nowScore);
				return;
			}else{
				if(PlayerPrefs.GetFloat("Highest") < nowScore){
					PlayerPrefs.SetFloat("Highest",nowScore);
				}
			}
		}
	}
}