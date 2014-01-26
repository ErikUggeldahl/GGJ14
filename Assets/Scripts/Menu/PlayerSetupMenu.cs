using UnityEngine;
using System.Collections;

public class PlayerSetupMenu : MonoBehaviour
{
    public GUISkin skin;
    public GameObject canvasObj;

    public FaceTextureStore faceStore;
    public Camera rayCam;
    public GUIText countDownText;
    public FaceTransfer transfer;

    public Transform playerCam;
    Vector3 playerCamOPos;
    Quaternion playerCamORot;

    public Transform liegeFaceLock;
    public Transform blueFaceLock;
    public Transform redFaceLock;

    string liegeName = "";
    string blueVassalName = "";
    string redVassalName = "";

    float drawTime = 15;

    bool activeGUI = true;
    bool activeUpdate = true;

    void Start()
    {
        playerCamOPos = playerCam.position;
        playerCamORot = playerCam.rotation;
    }

    void Update()
    {
        if (!activeUpdate)
            return;

        if (Input.GetKey(KeyCode.Alpha1))
        {
            playerCam.position = liegeFaceLock.position;
            playerCam.rotation = liegeFaceLock.rotation;
        } else if (Input.GetKey(KeyCode.Alpha2))
        {
            playerCam.position = redFaceLock.position;
            playerCam.rotation = redFaceLock.rotation;
        } else if (Input.GetKey(KeyCode.Alpha3))
        {
            playerCam.position = blueFaceLock.position;
            playerCam.rotation = blueFaceLock.rotation;
        } else
        {
            playerCam.position = playerCamOPos;
            playerCam.rotation = playerCamORot;
        }
    }

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

        string newName = GUILayout.TextField(liegeName, GUILayout.Height(30f), GUILayout.Width(140f));
        if (newName.Length > 12)
            newName = newName.Substring(0, 12);
        if (newName != liegeName)
        {
            faceStore.LiegeName = newName;
            transfer.Transfer();
        }
        liegeName = newName;

        if (GUILayout.Button("Draw Face!", GUILayout.Width(150f), GUILayout.Height(30f)))
            Draw(0);
        GUILayout.EndHorizontal();

        GUILayout.Label("Blue Vassal Name");
        GUILayout.BeginHorizontal();

        newName = GUILayout.TextField(blueVassalName, GUILayout.Height(30f), GUILayout.Width(140f));
        if (newName.Length > 12)
            newName = newName.Substring(0, 12);
        if (newName != blueVassalName)
        {
            faceStore.BlueVassalName = newName;
            transfer.Transfer();
        }
        blueVassalName = newName;

        if (GUILayout.Button("Draw Face!", GUILayout.Width(150f)))
            Draw(1);
        GUILayout.EndHorizontal();

        GUILayout.Label("Red Vassal Name");
        GUILayout.BeginHorizontal();

        newName = GUILayout.TextField(redVassalName, GUILayout.Height(30f), GUILayout.Width(140f));
        if (newName.Length > 12)
            newName = newName.Substring(0, 12);
        if (newName != redVassalName)
        {
            faceStore.RedVassalName = newName;
            transfer.Transfer();
        }
        redVassalName = newName;

        if (GUILayout.Button("Draw Face!", GUILayout.Width(150f)))
            Draw(2);
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Ready!", GUILayout.Width(100f), GUILayout.Height(50f)))
            StartCoroutine(StartGame());

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

            if (Input.GetKeyDown(KeyCode.Space))
                break;
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

        transfer.Transfer();
    }

    IEnumerator StartGame()
    {
        activeUpdate = false;
        activeGUI = false;

        countDownText.transform.position = new Vector3(0.25f, 0.5f, 0f);

        countDownText.text = "3";
        playerCam.position = redFaceLock.position;
        playerCam.rotation = redFaceLock.rotation;
        yield return new WaitForSeconds(1f);
        countDownText.text = "2";
        playerCam.position = blueFaceLock.position;
        playerCam.rotation = blueFaceLock.rotation;
        yield return new WaitForSeconds(1f);
        countDownText.text = "1";
        playerCam.position = liegeFaceLock.position;
        playerCam.rotation = liegeFaceLock.rotation;
        yield return new WaitForSeconds(1f);
    }
}
