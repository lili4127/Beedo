using UnityEngine;

public class TopWall : MonoBehaviour
{
    private BoxCollider2D topWall;

    private void Awake()
    {
        topWall = GetComponent<BoxCollider2D>();
        topWall.size = new Vector2(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 2f, 0f, 0f)).x, 1f);
        topWall.offset = new Vector2(0f, Camera.main.ScreenToWorldPoint(new Vector3(0f, Screen.height, 0f)).y + 0.5f);
    }
}
