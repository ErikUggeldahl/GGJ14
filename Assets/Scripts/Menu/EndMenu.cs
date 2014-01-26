using UnityEngine;
using System.Collections;

public class EndMenu : MonoBehaviour
{
    public GUISkin skin;

    FaceTextureStore store;

    bool pictureTaken = false;

    void Start()
    {
        GameObject storeObj = GameObject.FindGameObjectWithTag("FaceStore");
        if (storeObj != null)
            store = storeObj.GetComponent<FaceTextureStore>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            System.Diagnostics.Process.Start(Application.persistentDataPath);
    }

    void OnGUI()
    {
        GUI.skin = skin;
        
        GUILayout.BeginArea(new Rect(100f, 100f, 300f, 300f));
        GUILayout.BeginVertical();
        
        GUILayout.Label("Blast Off!", "BigLabel");

        if (store != null)
            GUILayout.Label("Final Score: " + store.FinalScore);

        if (GUILayout.Button("Replay?", GUILayout.Width(100f), GUILayout.Height(50f)))
                return;
        
        GUILayout.EndVertical();
        GUILayout.EndArea();

        if (pictureTaken)
            return;
        string fileName = store != null ? "/" + store.LiegeName + store.BlueVassalName + store.RedVassalName + ".png" : "/Default.png";
        Application.CaptureScreenshot(Application.persistentDataPath + fileName);
        pictureTaken = true;
    }
}
