using UnityEngine;
using System.Collections;
namespace smoke{
	public class stickposition : MonoBehaviour {
		public Transform projectile;
		void Start () {
		}
		
		void Update () {

		}
		public void changePosition(){
			Vector3 newPosition = transform.position;
			newPosition.y = projectile.position.y + 1;
			newPosition.x = projectile.position.x + 1;
			transform.position = newPosition;
		}
	}
}