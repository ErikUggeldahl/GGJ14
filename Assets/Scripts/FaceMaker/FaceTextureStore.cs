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

    void Start()
    {
        DontDestroyOnLoad(this);
    }
}
