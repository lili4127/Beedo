using UnityEngine;

public class BottomWall : MonoBehaviour
{
    private BoxCollider2D bottomWall;

    private void Awake()
    {
        bottomWall = GetComponent<BoxCollider2D>();
        bottomWall.size = new Vector2(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 2f, 0f, 0f)).x, 1f);
        bottomWall.offset = new Vector2(0f, Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)).y - 0.5f);
    }
}
