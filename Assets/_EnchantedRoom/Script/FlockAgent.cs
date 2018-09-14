using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoogleARCoreInternal
{
	public class FlockAgent : MonoBehaviour
	{
		public float speed = 0.1f;
		float rotationSpeed = 4.0f;
		Vector3 avarageHeading;
		Vector3 avaragePosition;
		float neighbourDistance = 1.0f;
		//public bool startUpdate = false;
		bool turning = false;
		private GameObject centerObject;
		private FlockController flockController;
		
		public void Initialize(FlockController flockController)
		{
			this.flockController = flockController;
			this.centerObject = flockController.centerObject;
			speed = Random.Range(0.5f, 1);
		}

		// Update is called once per frame
		void Update()
		{
			if (Vector3.Distance(transform.position, centerObject.transform.position) >= flockController.areaSize)
			{
				turning = true;
			}
			else
			{
				turning = false;
			}
			if (turning)
			{
				Vector3 direction = centerObject.transform.position - transform.position;
				transform.rotation = Quaternion.Slerp(transform.rotation,
				Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
				speed = Random.Range(0.5f, 1);
			}

			else
			{
				if (Random.Range(0, 5) < 1)
				{
					ApplyRules();
				}
			}

			transform.Translate(0, 0, Time.deltaTime * speed);
		}
		void ApplyRules()
		{
			Vector3 goalPos = flockController.goalPos;
			Vector3 Vcentre = centerObject.transform.position;
			Vector3 vavoid = centerObject.transform.position;
			float gSpeed = 0.1f;
			float dist;

			int groupSize = 0;

			foreach (GameObject butteryfly in flockController.allButterfly)
			{
				if (butteryfly != this.gameObject)
				{
					dist = Vector3.Distance(butteryfly.transform.position, this.transform.position);
					if (dist <= neighbourDistance)
					{
						Vcentre += butteryfly.transform.position;
						groupSize++;

						if (dist < 1.0f)
						{
							vavoid = vavoid + (this.transform.position - butteryfly.transform.position);
						}

						FlockAgent anotherFlock = butteryfly.GetComponent<FlockAgent>();
						gSpeed = gSpeed + anotherFlock.speed;
					}
				}

			}
			if (groupSize > 0)
			{
				Vcentre = Vcentre / groupSize + (goalPos - this.transform.position);
				speed = gSpeed / groupSize;

				Vector3 direction = (Vcentre + vavoid) - transform.position;
				if (direction != Vector3.zero)
					transform.rotation = Quaternion.Slerp(transform.rotation,
						Quaternion.LookRotation(direction),
						rotationSpeed * Time.deltaTime);
			}
		}
	}
}
