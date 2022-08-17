using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCreatorForCarpet : MonoBehaviour
{
	[SerializeField] private float force = 10f;
	[SerializeField] private float forceOffset = 0.1f;
	private bool isActive = false;
	[SerializeField] private LayerMask layerMask;

    private void Start()
    {
        
    }
    void FixedUpdate()
	{
		if (isActive)
		{
			RaycasToDown();
		}
	}

	private void RaycasToDown()
	{
		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.position-new Vector3(0,50,0), out hit, Mathf.Infinity, layerMask))
		{
			MeshDeformer deformer = hit.collider.GetComponent<MeshDeformer>();
			if (deformer)
			{
				Vector3 point = hit.point;
				point += hit.normal * forceOffset;
				deformer.AddDeformingForce(point, force);
			}
			
		}
	}
	private void MakeItActive()
    {
		isActive = true;
    }
	void OnEnable()
	{
		EventManager.myLevelStarted += MakeItActive;

	}
	void OnDisable()
	{
		EventManager.myLevelStarted -= MakeItActive;
	}
}
