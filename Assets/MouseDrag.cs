using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDrag : MonoBehaviour {

    private Vector3 mOffset;
    private float mZCoord;

	private void OnMouseDown()
	{
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        mOffset = gameObject.transform.position - GetMouseAsWorldPoint();
	}

    private Vector3 GetMouseAsWorldPoint()
    {

        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);

    }
    private void OnMouseDrag()
	{
		transform.position = GetMouseAsWorldPoint() + mOffset;
	}

}