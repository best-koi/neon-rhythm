using UnityEngine;



public class ArrowSpawner : MonoBehaviour
{
    public ArrowType delegateArrow;
    public GameObject arrowToSpawn;
    private RectTransform rectTransform;
    private Vector3 spawnLocation;

    void Start()
    {
        rectTransform = this.GetComponent<RectTransform>();
        spawnLocation = new Vector3(rectTransform.localPosition.x, rectTransform.localPosition.y, rectTransform.localPosition.z);
        JSONRead.onSpawnNote += SpawnArrowAbsolute;
    }

    void SpawnArrowAbsolute(ArrowType arrow)
    {
        if(arrow == delegateArrow)
        {
            GameObject spawnedArrow = Instantiate(arrowToSpawn, rectTransform);
            spawnedArrow.transform.SetParent(rectTransform, false);
            RectTransform spawnedArrowLTransform = spawnedArrow.GetComponent<RectTransform>();
            spawnedArrowLTransform.localPosition = new Vector3(spawnLocation.x, spawnLocation.y, spawnLocation.z);
            Destroy(spawnedArrow, 6);
        }
    }
}
