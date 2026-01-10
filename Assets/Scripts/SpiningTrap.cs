using Unity.Cinemachine;
using UnityEngine;

public class SpiningTrap : MonoBehaviour
{
    public float rotationSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(new Vector3(0, 0, rotationSpeed));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FindAnyObjectByType<PlayerMovement>().TakeDamage();
        }
    }
}
