using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    public float rotationSpeed = 2f;

    void Start()
    {
        // od 50% do 150% domy�lnej warto�ci
        rotationSpeed = Random.Range(0.5f * rotationSpeed, 1.5f * rotationSpeed);
    }

    void FixedUpdate()
    {
        if (!GameManager.instance.isInGame)
        {
            return;
        }

        transform.Rotate(new Vector3(0f, 0f, rotationSpeed));
    }
}
