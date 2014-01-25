using UnityEngine;
using System.Collections;

public class Draw : MonoBehaviour
{
	public Material canvas;
	public Camera rayCam;

	int lastX = -1;
	int lastY = -1;

	Texture2D tex;

	void Start()
	{
		tex = new Texture2D(256, 256);
	}

	void Update()
	{
		RaycastHit hit;
		if (!Physics.Raycast (rayCam.ScreenPointToRay (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0.5f)), out hit, float.PositiveInfinity, 1 << 8))
			return;

		int x = CanvasPtToPixel(hit.point.x);
		int y = CanvasPtToPixel(hit.point.y);

		if (lastX > 0 && Input.GetMouseButton(0))
			DrawLine(x, y, lastX, lastY);

		lastX = x;
		lastY = y;
	}

	int CanvasPtToPixel(float point)
	{
		point += 0.5f;
		point *= 256f;
		return (int)point;
	}

	void DrawAroundPoint(int x, int y)
	{
		Color c = Color.black;
		tex.SetPixels(x, y, 2, 2, new Color[]{c, c, c, c});
	}

	void DrawLine(int x1, int y1, int x2, int y2)
	{
		int diffX = Mathf.Abs(x2 - x1);
		int diffY = Mathf.Abs(y2 - y1);

		int steps = Mathf.Max(diffX, diffY);


		for (int i = 0; i < steps; i++)
		{
			float t = (float)i/steps;
			int x = (int)Mathf.Lerp((float)x1, (float)x2, t);
			int y = (int)Mathf.Lerp((float)y1, (float)y2, t);

			DrawAroundPoint((int)x, (int)y);
		}

		tex.Apply();
		canvas.mainTexture = tex;
	}
}
