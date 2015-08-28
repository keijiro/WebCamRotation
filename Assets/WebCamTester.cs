using UnityEngine;

public class WebCamTester : MonoBehaviour
{
    WebCamTexture webCam;
    Material material;

    void Start()
    {
        webCam = new WebCamTexture(4096, 4096);

        material = GetComponent<Renderer>().material;
        material.mainTexture = webCam;

        webCam.Play();
    }

    void Update()
    {
        transform.rotation = Quaternion.AngleAxis(webCam.videoRotationAngle, -Vector3.forward);

        var screenAspect = (float)Screen.width / Screen.height;
        var webCamAspect = (float)webCam.width / webCam.height;

        var rot90 = (webCam.videoRotationAngle / 90) % 2 != 0;
        if (rot90) webCamAspect = 1.0f / webCamAspect;

        float sx, sy;
        if (webCamAspect < screenAspect)
        {
            sx = webCamAspect;
            sy = 1.0f;
        }
        else
        {
            sx = screenAspect;
            sy = screenAspect / webCamAspect;
        }

        if (rot90)
            transform.localScale = new Vector3(sy, sx, 1);
        else
            transform.localScale = new Vector3(sx, sy, 1);

        var mirror = webCam.videoVerticallyMirrored;
        material.mainTextureOffset = new Vector2(0, mirror ? 1 : 0);
        material.mainTextureScale = new Vector2(1, mirror ? -1 : 1);
    }

    void OnGUI()
    {
        var text = "web cam size = " + webCam.width + " x " + webCam.height;
        text += "\nrotation = " + webCam.videoRotationAngle;
        text += "\nscreen size = " + Screen.width + " x " + Screen.height;
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), text);
    }
}
