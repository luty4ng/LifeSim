using UnityEngine;

[ExecuteInEditMode]
public class CameraAdjust : MonoBehaviour
{
    private const float DEFAULT_RATIO = 1080f / 1920f;
    private int m_ScreenWidth;
    private const float DEFAULT_SIZE = 5f;

    private void Start()
    {
        m_ScreenWidth = Screen.width;
    }

    private void LateUpdate()
    {
        if (m_ScreenWidth != Screen.width)
        {
            m_ScreenWidth = Screen.width;

            float width = Screen.width;
            float height = Screen.height;
            float ratio = width / height;
            if (ratio > DEFAULT_RATIO)
            {
                if (Camera.main.orthographicSize != DEFAULT_SIZE)
                    Camera.main.orthographicSize = DEFAULT_SIZE;
            }
            else
            {
                Camera.main.orthographicSize = DEFAULT_SIZE * (DEFAULT_RATIO) / (width / height);
            }
        }
    }
}