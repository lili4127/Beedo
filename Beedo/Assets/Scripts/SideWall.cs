using UnityEngine;

public class SideWall : MonoBehaviour
{
    private BoxCollider2D sideWall;

    private void Awake()
    {
        sideWall = GetComponent<BoxCollider2D>();

        if (sideWall.gameObject.name.Equals("Left"))
        {
            sideWall.size = new Vector2(1f, Camera.main.ScreenToWorldPoint(new Vector3(0f, Screen.height * 2f, 0f)).y);
            sideWall.offset = new Vector2(Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)).x + 0.05f, 0f);
        }

        else
        {
            sideWall.size = new Vector2(1f, Camera.main.ScreenToWorldPoint(new Vector3(0f, Screen.height * 2f, 0f)).y);
            sideWall.offset = new Vector2(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x - 0.05f, 0f);
        }
    }
}
