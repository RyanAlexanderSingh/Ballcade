using UnityEngine;

namespace Ballcade.Utils
{
	public class FPSDisplay : MonoBehaviour
	{
		float deltaTime = 0.0f;
 
		void Update()
		{
			deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
		}
 
		void OnGUI()
		{
			int w = Screen.width, h = Screen.height;
 
			GUIStyle style = new GUIStyle();
 
			Rect rect = new Rect(0, 0, w, height: h * 0.1f);
			style.alignment = TextAnchor.UpperLeft;
			style.fontSize = (int)(h * 0.05f);
			style.normal.textColor = Color.white;
			float msec = deltaTime * 1000.0f;
			float fps = 1.0f / deltaTime;
			string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
			GUI.Label(rect, text, style);
		}
	}
}