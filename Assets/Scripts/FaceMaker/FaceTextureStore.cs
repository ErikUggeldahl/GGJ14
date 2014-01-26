using UnityEngine;
using System.Collections;

public class FaceTextureStore : MonoBehaviour
{
    public string LiegeName;
    public string BlueVassalName;
    public string RedVassalName;

    public Texture2D LiegeFace;
    public Texture2D BlueVassalFace;
    public Texture2D RedVassalFace;

    public int FinalScore;

    public bool Old = false;

    void Awake()
    {
        if (Old)
            return;

        GameObject other = GameObject.FindGameObjectWithTag("FaceStore");
        if (other != null && other != gameObject)
            Destroy(other);

    }

    void Start()
    {
        DontDestroyOnLoad(this);
        Old = true;
    }
}
