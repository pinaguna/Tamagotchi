using UnityEngine;

using UnityEngine;

public class PetMovement : MonoBehaviour
{
    [Header("Sýnýrlar")]
    public Vector2 minPosition = new Vector2(-13.39f, -1.44f); // sol-alt
    public Vector2 maxPosition = new Vector2(-11.9f, -1.94f);  // sað-üst


    [Header("Hareket Ayarlarý")]
    public float moveSpeed = 1.5f;
    public float waitTime = 5f;

    private Vector3 targetPosition;
    private bool isMoving = false;
    private float waitTimer = 0f;

    void Start()
    {
        transform.position = new Vector3(-13.97f, -1.36f, 0f);
        ChooseNewTarget();
    }

    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
            {
                isMoving = false;
                waitTimer = waitTime;
            }
        }
        else
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0f)
            {
                ChooseNewTarget();
            }
        }
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minPosition.x, maxPosition.x),
            Mathf.Clamp(transform.position.y, minPosition.y, maxPosition.y),
            transform.position.z
);
    }

    void ChooseNewTarget()
    {
        float randomX = Random.Range(minPosition.x, maxPosition.x);
        float randomY = Random.Range(minPosition.y, maxPosition.y);
        targetPosition = new Vector3(randomX, randomY, transform.position.z);
        isMoving = true;
    }
}
