using UnityEngine;
using System.Collections;

public enum Axis {Horizontal, Vertical}

public class Formation : MonoBehaviour 
{
	public Transform unit;
	public Vector3 dimensions = Vector3.one;
	public Vector3 offset = Vector3.zero;
	Bounds boundingBox;

	void Start () 
	{
		//boundingBox = GetComponent<Collider2D>().bounds;
		//boundingBox = GetComponent<Collider>().bounds;
		//FormUp(dimensions);
	}

	public void FormUp(Vector3 count, bool includeEdge = true)
	{
		boundingBox = GetComponent<Collider>().bounds;

		if (includeEdge)
		{
			Vector3 spacing = new Vector3
			(
				count.x == 1f ? 1 : boundingBox.size.x / (count.x - 1),
				count.y == 1f ? 1 : boundingBox.size.y / (count.y - 1),
				count.z == 1f ? 1 : boundingBox.size.z / (count.z - 1)
			);
			Debug.Log("Spacing: " + spacing);
			for (int i = 0; i < count.x; i++)
			{
				for (int j = 0; j < count.y; j++)
				{
					for (int k = 0; k < count.z; k++)
					{
						Transform nextUnit = Instantiate
						(
							unit,
							//new Vector3
							//(
							//	((spacing.x * i) - boundingBox.extents.x) + transform.localPosition.x,
							//	((spacing.y * j) - boundingBox.extents.y) + transform.localPosition.y,
							//	((spacing.z * k) - boundingBox.extents.z) + transform.localPosition.z
							//),
							new Vector3
							(
								((spacing.x * i) - boundingBox.extents.x) + transform.position.x,
								((spacing.y * j) - boundingBox.extents.y) + transform.position.y,
								((spacing.z * k) - boundingBox.extents.z) + transform.position.z
							) + offset,
							Quaternion.identity,
							transform
						) as Transform;

						nextUnit.name = "Unit " + i + j + k;
					}
				}
			}
		}
		else
		{
			Vector3 spacing = new Vector3
			(
				boundingBox.size.x / (count.x + 1), 
				boundingBox.size.y / (count.y + 1),
				boundingBox.size.z / (count.z + 1)
			);
			for (int i = 1; i <= count.x; i++)
			{
				for (int j = 1; j <= count.y; j++)
				{
					for(int k = 1; k <= count.z; k++)
					{
						Transform nextUnit = Instantiate
						(
							unit,
							new Vector3
							(
								((spacing.x * i) - boundingBox.extents.x) + transform.localPosition.x,
								((spacing.y * j) - boundingBox.extents.y) + transform.localPosition.y,
								((spacing.z * k) - boundingBox.extents.z) + transform.localPosition.z
							),
							transform.rotation,
							transform
						) as Transform;
					}
				}
			}
		}
	}
}
