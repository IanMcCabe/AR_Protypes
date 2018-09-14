using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCoreInternal;

public class FlockController : MonoBehaviour
{
	public GameObject centerObject;
	public int areaSize = 3;
	public Vector3 goalPos = Vector3.zero;
	public Vector3 modifyPos = Vector3.zero;
	public List<GameObject> allButterfly;
	
	void Start()
	{
		goalPos = centerObject.gameObject.transform.position;
	}
	
	void Update()
	{
		goalPos = centerObject.gameObject.transform.position + modifyPos;

		if (Random.Range(0, 10000) < 50)
		{
			modifyPos += new Vector3(Random.Range(-areaSize, areaSize),
				Random.Range(-areaSize, areaSize),
				Random.Range(-areaSize, areaSize));
		}
	}
}
