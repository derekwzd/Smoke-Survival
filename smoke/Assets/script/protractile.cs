using UnityEngine;
using System.Collections;

namespace smoke{
	public class protractile : MonoBehaviour {
		public float maxStretch;
		public LineRenderer catapultLineLeft;
		public LineRenderer catapultLineRight;
		public Rigidbody2D  body;
		public Rigidbody2D stick;
		public Rigidbody2D stick2;
		public Transform ring;
		public bool clickOn = false;
		public static protractile current;

		private SpringJoint2D spring;
		private Transform catapult;
		private Ray rayToMouse;
		private Ray leftCatapultToProjectile;
		private float maxStretchSqr;
		private float circleRadius;
		private Vector2 prevVelocity;
		private Renderer rend;


		void Awake(){
			spring = GetComponent<SpringJoint2D> ();
			catapult = spring.connectedBody.transform;
			if (current == null)
				current = this;
			else if(current != this)
				Destroy (gameObject);
		}

		void Start () {
			rayToMouse = new Ray (catapult.position, Vector3.zero);
			LineRendererSetup();
			leftCatapultToProjectile = new Ray(catapultLineRight.transform.position, Vector3.zero);
			maxStretch = 3f;
			maxStretchSqr = maxStretch * maxStretch;
			CircleCollider2D circle = GetComponent<Collider2D>() as CircleCollider2D;
			circleRadius = circle.radius;
			body = GetComponent<Rigidbody2D>();
			rend = GetComponent<Renderer> ();
		}

		void Update () {
			if (clickOn) {
				Dragging ();
			}
			if (GM.current.gameState) {
				changeScaleByLife ();
				if (spring.enabled == true) {
					//launch
					if (!body.isKinematic && prevVelocity.sqrMagnitude > body.velocity.sqrMagnitude) {
						spring.enabled = false;
						body.velocity = prevVelocity;
						GM.current.canTouch = false;
					}

					if (!clickOn) {
						prevVelocity = body.velocity;
					}
					LineRenderUpdate ();
				
				} else {
					catapultLineLeft.enabled = false;
					catapultLineRight.enabled = false;
				}
			}
		}

		void changeScaleByLife(){
			float colorSim = 132 + (float)1.0 * GM.current.score / GM.current.originalLife * 123;
			rend.material.color = new Color ((float)colorSim/255, (float)colorSim/255, (float)colorSim/255, 1.0F);
		}

		void LineRendererSetup (){
			catapultLineLeft.SetPosition (0, catapultLineLeft.transform.position);
			catapultLineRight.SetPosition (0, catapultLineRight.transform.position);

			catapultLineLeft.sortingLayerName = "Foreground";
			catapultLineRight.sortingLayerName = "Foreground";

			catapultLineLeft.sortingOrder = 1;
			catapultLineRight.sortingOrder = 1;
		}
		
		void OnMouseDown(){
			if (GM.current.canTouch) {
				stick.GetComponent<Rigidbody2D>().transform.position = new Vector3(body.GetComponent<Rigidbody2D>().transform.position.x, body.GetComponent<Rigidbody2D>().transform.position.y,  body.GetComponent<Rigidbody2D>().transform.position.z);
				catapult = spring.connectedBody.transform;
				rayToMouse = new Ray (catapult.position, Vector3.zero);
				cameraScript.current.move = false;
				spring.enabled = false;
				clickOn = true;
				body.isKinematic = true;
				ring.GetComponent<Rigidbody2D> ().transform.position = transform.position;
			}
		}
		void OnBecameInvisible(){
			GM.current.Reset ();
		}

		void OnMouseUp(){
			if (!GM.current.gameState) {
				GM.current.gameState = true;
				GM.current.startDec();
			}
			if (GM.current.canTouch) {
				spring.enabled = true;
				body.isKinematic = false;
				clickOn = false;
				cameraScript.current.move = true;
			}
		}

		void Dragging(){
			Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			Vector2 catapultToMouse = mouseWorldPoint - catapult.position;

			if (catapultToMouse.sqrMagnitude > maxStretchSqr) {
				rayToMouse.direction = catapultToMouse;
				mouseWorldPoint = rayToMouse.GetPoint(maxStretch); 
			}
			mouseWorldPoint.z = -1.63f;
			transform.position = mouseWorldPoint;
		}
		
		void LineRenderUpdate(){
			Vector2 catapultToProjectile = transform.position - catapultLineRight.transform.position;
			leftCatapultToProjectile.direction = catapultToProjectile;
			Vector3 holdPoint = leftCatapultToProjectile.GetPoint (catapultToProjectile.magnitude + circleRadius);
			catapultLineRight.SetPosition (1, holdPoint);
			catapultLineLeft.SetPosition (1, holdPoint);
		}
	}
}