using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class FPSDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text txtFps;

    float deltaTime = 0.0f;
    float minFpsPrev = 120;
    float maxFpsPrev = 0;
    private void Start()
    {
        StartCoroutine(ResetTempCache());
    }

    IEnumerator ResetTempCache()
    {
        yield return new WaitForSecondsRealtime(10f);
        minFpsPrev = 120;
        maxFpsPrev = 0;

        StartCoroutine(ResetTempCache());
    }

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;

        if (minFpsPrev > fps)
        {
            minFpsPrev = fps;
        }

        if (maxFpsPrev < fps)
        {
            maxFpsPrev = fps;
        }

        string text = string.Format("<color=white>↑ {0:0.}</color>\n{1:0} fps ({2:0.0} ms) \n<color=white>↓ {3:0.}</color>",
        maxFpsPrev, fps, msec, minFpsPrev);

        txtFps.text = text;
    }

}