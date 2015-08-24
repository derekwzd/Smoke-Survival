using UnityEngine;
using System.Collections;
namespace smoke{
	public class resetter : MonoBehaviour {
		public Rigidbody2D player;
		public Transform ring;

		private float resetSpeed;
		private float resetSpeedSqr;
		private SpringJoint2D spring;

		void Start () {
			resetSpeed = 1f;
			resetSpeedSqr = resetSpeed * resetSpeed;
			spring = player.GetComponent<SpringJoint2D>();
		}
		
		void Update () {
			if (spring.enabled == false && player.velocity.sqrMagnitude < resetSpeedSqr && protractile.current.clickOn == false) {
				Debug.Log (1);
				spring.enabled = true;
				player.GetComponent<Rigidbody2D>().isKinematic = true;
				GM.current.canTouch = true;
				ring.GetComponent<Rigidbody2D> ().transform.position = new Vector3(player.position.x, player.position.y+0.05F, -1.63F); 
			}
		}
	}
}