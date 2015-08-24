using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace smoke{
	public class cameraScript : MonoBehaviour {
		public static cameraScript current;
		public Transform projectile;
		public bool move;

		void Awake(){
			if (current == null)
				current = this;
			else if(current != this)
				Destroy (gameObject);
		}

		void Start () {
			if (current == null)
				current = this;
			else if(current != this)
				Destroy (gameObject);
			move = false;

		}

		void Update () {

			if (move) {
				Vector3 nowPosition = transform.position;
				nowPosition.y = Mathf.Max(projectile.position.y + 4.5f, nowPosition.y);
				transform.position = nowPosition;
			}
		}
	}
}