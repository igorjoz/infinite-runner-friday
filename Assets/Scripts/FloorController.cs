using UnityEngine;

public class FloorController : MonoBehaviour
{
    public GameObject floorTiles1, floorTiles2;
    public GameObject[] floorTilesArray;

    void FixedUpdate()
    {
        floorTiles1.transform.position -= new Vector3(GameManager.instance.worldScrollingSpeed, 0f, 0f);
        floorTiles2.transform.position -= new Vector3(GameManager.instance.worldScrollingSpeed, 0f, 0f);

        if (floorTiles2.transform.position.x < 0f)
        {
            //floorTiles1.transform.position += new Vector3(40f, 0f, 0f);
            var newFloorTiles = Instantiate(floorTilesArray[Random.Range(0, floorTilesArray.Length - 1)], floorTiles2.transform.position + new Vector3(20f, 0f, 0f), Quaternion.identity);

            Destroy(floorTiles1);

            //var temp = floorTiles1;
            floorTiles1 = floorTiles2;
            floorTiles2 = newFloorTiles;
        }
    }
}
