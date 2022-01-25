using UnityEngine;

public class SpriteStretch : MonoBehaviour
{
    public bool KeepAspectRatio;

    void Start()
    {
        Vector3 topRightCorner = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        float worldSpaceWidth = topRightCorner.x * 2;
        float worldSpaceHeight = topRightCorner.y * 2;
        Vector3 spriteSize = GetComponent<SpriteRenderer>().bounds.size;
        float scaleFactorX = worldSpaceWidth / spriteSize.x;
        float scaleFactorY = worldSpaceHeight / spriteSize.y;

        if (KeepAspectRatio)
        {
            if (scaleFactorX > scaleFactorY)
            {
                scaleFactorY = scaleFactorX;
            }
            else
            {
                scaleFactorX = scaleFactorY;
            }
        }

        transform.localScale = new Vector3(scaleFactorX, scaleFactorY, 1);
    }
}
