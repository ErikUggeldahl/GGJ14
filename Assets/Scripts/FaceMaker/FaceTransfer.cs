using UnityEngine;
using System.Collections;

public class FaceTransfer : MonoBehaviour
{
    public Renderer liegeFace;
    public Renderer blueVassalFace;
    public Renderer redVassalFace;

    public TextMesh liegeLabel;
    public TextMesh blueVassalLabel;
    public TextMesh redVassalLabel;

    FaceTextureStore store;

    void Start()
    {
        store = GameObject.FindGameObjectWithTag("FaceStore").GetComponent<FaceTextureStore>();

        Transfer();
    }

    public void Transfer()
    {
        liegeFace.material.mainTexture = store.LiegeFace;
        blueVassalFace.material.mainTexture = store.BlueVassalFace;
        redVassalFace.material.mainTexture = store.RedVassalFace;
        
        liegeLabel.text = store.LiegeName;
        blueVassalLabel.text = store.BlueVassalName;
        redVassalLabel.text = store.RedVassalName;
    }
}
