using UnityEngine;

public class ShurikenTrap : MonoBehaviour
{
    public float speed = 3f;
    private int index = 0;
    public Transform[] points;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, points[index].position) < .1f)
        {
            index += 1;
        }

        if (index == points.Length)
        {
            index = 0;
        }

        transform.position = Vector2.MoveTowards(transform.position, points[index].position, speed * Time.deltaTime);
    }
}
