using UnityEngine;
using System.Collections;


namespace smoke{
	public class providesurvive : MonoBehaviour {
		public Rigidbody2D projectile;
		private bool isReward;

		void Start () {
			isReward = false;

		}

		void Update () {
		}

		void OnBecameVisible(){
			InvokeRepeating("getSmaller", 0.1f, 0.1f);
		}

		void getSmaller(){
			if (transform.localScale.x >= 0F) {
				transform.localScale -= new Vector3 (0.003F, 0.003F, 0.003F);
			} else {
				Destroy(GetComponent<CircleCollider2D>());
			}
		}

		void OnTriggerEnter2D(Collider2D other){
			if(other.GetComponent<Rigidbody2D>() == projectile){
				GM.current.stopDec();
				if(!isReward){
					GM.current.reward();
					isReward = true;
				}
			}
		}
		void OnTriggerExit2D(Collider2D other){
			if(other.GetComponent<Rigidbody2D>() == projectile){
//				GM.current.life -= 1;
				GM.current.startDec();
			}
		}
	}
}