using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AircraftController : MonoBehaviour {
    float circleRadius;
    float circleSpeed;
    float circleAngle;
    float selfRotationSpeed;
    Vector3 lastDirection;
		private int skyColor;
		private int dirLightInd;
		private int pointLightInd;
		private int spotLightInd;
		public GameObject dirLight;
		public GameObject pointLight;
		public GameObject spotLight;
		public GameController gameController;
		private bool canMove;

     void Start () {
			 	skyColor = 0;
				dirLightInd = 0;
				pointLightInd = 0;
        circleRadius = 10;
        circleSpeed = 0.5f;
        circleAngle = 0;
        selfRotationSpeed = 100;
        lastDirection = new Vector3(1, 0, 0);
        lastDirection.Normalize();
				startHack();
				startHack();
				canMove = true;
    }

    void Update() {
				if(Input.GetKeyDown("a"))
				{
					if(skyColor == 0)
					{
						RenderSettings.ambientSkyColor = Color.red;
						skyColor = 1;
					}
					else
					{
						RenderSettings.ambientSkyColor = Color.black;
						skyColor = 0;
					}
				}

				if(Input.GetKeyDown("d"))
				{
					if(dirLightInd == 0)
					{
						dirLight.active = false;
						dirLightInd = 1;
					}
					else
					{
						dirLight.active = true;
						dirLightInd = 0;
					}
				}

				if(Input.GetKeyDown("p"))
				{
					if(pointLightInd == 0)
					{
						pointLight.active = false;
						pointLightInd = 1;
					}
					else
					{
						pointLight.active = true;
						pointLightInd = 0;
					}
				}

				if(Input.GetKeyDown("s"))
				{
					if(spotLightInd == 0)
					{
						spotLight.active = false;
						spotLightInd = 1;
					}
					else
					{
						spotLight.active = true;
						spotLightInd = 0;
					}
				}

				if(Input.GetKey("left"))
				{
					transform.Rotate(Vector3.forward, selfRotationSpeed * Time.deltaTime, Space.Self);
				}
				if(Input.GetKey("right"))
				{
					transform.Rotate(Vector3.forward, selfRotationSpeed * Time.deltaTime * -1, Space.Self);
				}

				if(Input.GetKey("up") && canMove && gameController.getGameOver() == false)
				{
					circleAngle += circleSpeed * Time.deltaTime;

					circleAngle = (circleAngle + 360) % 360;

					float newPositionX = circleRadius * (float)Mathf.Cos(circleAngle);
					float newPositionZ = circleRadius * (float)Mathf.Sin(circleAngle);

					Vector3 newPosition = new Vector3(newPositionX, transform.position.y, newPositionZ);
					Vector3 newDirection = newPosition - transform.position;

					newDirection.Normalize();

					float rotationAngle = -Vector3.Angle(lastDirection, newDirection);
					transform.Rotate(Vector3.up, rotationAngle, Space.World);
					transform.position = newPosition;
					lastDirection = newDirection;
				}
    }

		void startHack()
		{
			circleAngle += circleSpeed * Time.deltaTime;

			circleAngle = (circleAngle + 360) % 360;

			float newPositionX = circleRadius * (float)Mathf.Cos(circleAngle);
			float newPositionZ = circleRadius * (float)Mathf.Sin(circleAngle);

			Vector3 newPosition = new Vector3(newPositionX, transform.position.y, newPositionZ);
			Vector3 newDirection = newPosition - transform.position;

			newDirection.Normalize();

			float rotationAngle = -Vector3.Angle(lastDirection, newDirection);
			transform.Rotate(Vector3.up, rotationAngle, Space.World);
			transform.position = newPosition;
			lastDirection = newDirection;
		}


		void OnTriggerStay(Collider c)
		{
      if(c.gameObject.tag == "Obstacle")
      {
				canMove = false;
      }
		}

		void OnTriggerExit(Collider c)
		{
      if(c.gameObject.tag == "Obstacle")
      {
				canMove = true;
      }
      if(c.gameObject.tag == "end")
      {
        gameController.NewRound();
      }
      if(c.gameObject.tag == "pass")
      {
        gameController.SetRemaining();
      }
		}
/*
		void OnTriggerEnter(Collider c)
		{
			//you will update these in a later step
			if (c.gameObject.tag == "Obstacle") {
					//gameController.GameOverLose();
			} else if (c.gameObject.tag == "Ground") {
					//gameController.GameOverLose();
			}
			/*if (c.gameObject.tag == "Gap") {
					gameController.addScore();
					c.gameObject.SetActive (false);
			}
		}*/
}
