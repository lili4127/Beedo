using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPool : MonoBehaviour
{
    [SerializeField] private Ball ballPrefab;
    private Queue<Ball> ballQueue;

    private void Awake()
    {
        ballQueue = new Queue<Ball>();
        AddBall(10);
    }

    public Ball Get()
    {
        if (ballQueue.Count == 0)
        {
            AddBall(1);
        }

        return ballQueue.Dequeue();
    }

    private void AddBall(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Ball b = Instantiate(ballPrefab, transform);
            b.gameObject.SetActive(false);
            ballQueue.Enqueue(b);
        }
    }

    public void ReturnToPool(Ball b)
    {
        if (!ballQueue.Contains(b))
        {
            ballQueue.Enqueue(b);
        }
    }
}
