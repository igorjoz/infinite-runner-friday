using System.Data;
using UnityEngine;

public class FloatingScript : MonoBehaviour
{
    public float floatingHeight = 0.5f;
    private int count = 0;
    private const int STEPS = 25;
    private bool up = true;

    void FixedUpdate()
    {
        Vector3 move = (up) ? new Vector3(0f, floatingHeight / STEPS, 0f) : new Vector3(0f, -floatingHeight / STEPS, 0f);

        transform.position += move;

        count++;

        if (count == STEPS)
        {
            count = 0;
            up = !up;
        }
    }
}
