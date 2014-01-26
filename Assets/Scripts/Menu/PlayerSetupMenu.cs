using UnityEngine;
using System.Collections;

public class PlayerSetupMenu : MonoBehaviour
{
    public GUISkin skin;
    public GameObject canvasObj;

    public FaceTextureStore faceStore;
    public Camera rayCam;
    public GUIText countDownText;

    string liegeName = "";
    string blueVassalName = "";
    string redVassalName = "";

    float drawTime = 15;

    bool activeGUI = true;

    void OnGUI()
    {
        if (!activeGUI)
            return;

        GUI.skin = skin;

        GUILayout.BeginArea(new Rect(100f, 100f, 300f, 300f));
        GUILayout.BeginVertical();

        GUILayout.Label("Setup", "BigLabel");

        GUILayout.Label("Liege Name");
        GUILayout.BeginHorizontal();
        liegeName = GUILayout.TextField(liegeName, GUILayout.Height(30f));
        if (GUILayout.Button("Draw Face!", GUILayout.Width(150f), GUILayout.Height(30f)))
            Draw(0);
        GUILayout.EndHorizontal();

        GUILayout.Label("Blue Vassal Name");
        GUILayout.BeginHorizontal();
        blueVassalName = GUILayout.TextField(blueVassalName);
        if (GUILayout.Button("Draw Face!", GUILayout.Width(150f)))
            Draw(1);
        GUILayout.EndHorizontal();

        GUILayout.Label("Red Vassal Name");
        GUILayout.BeginHorizontal();
        redVassalName = GUILayout.TextField(redVassalName);
        if (GUILayout.Button("Draw Face!", GUILayout.Width(150f)))
            Draw(2);
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Ready!", GUILayout.Width(100f), GUILayout.Height(50f)))
            StartGame();

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }

    void Draw(byte id)
    {
        Draw canvas = ((GameObject)Instantiate(canvasObj)).GetComponent<Draw>();

        canvas.rayCam = rayCam;
        canvas.BeginDraw();

        StartCoroutine(DrawTimer(id, canvas));

        activeGUI = false;
    }

    IEnumerator DrawTimer(byte id, Draw canvas)
    {
        float time = drawTime;

        while (time > 0f)
        {
            time -= Time.deltaTime;
            countDownText.text = ((int)time).ToString();
            yield return null;
        }

        countDownText.text = "";
        activeGUI = true;

        switch (id)
        {
            case 0:
                faceStore.LiegeFace = canvas.EndDraw(); break;
            case 1:
                faceStore.BlueVassalFace = canvas.EndDraw(); break;
            case 2:
                faceStore.RedVassalFace = canvas.EndDraw(); break;
        }
        Destroy(canvas.gameObject);
    }

    void StartGame()
    {
        faceStore.LiegeName = liegeName;
        faceStore.BlueVassalName = blueVassalName;
        faceStore.RedVassalName = redVassalName;
    }
}
