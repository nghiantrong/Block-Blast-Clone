using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockSpawner : MonoBehaviour
{
    public List<GameObject> blockPrefabs;
    public Transform blockHolder;
    public int blocksPerTurn = 3;

    void Start()
    {
        SpawnBlocks();
    }

    private void SpawnBlocks()
    {
        foreach (Transform child in blockHolder)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < blocksPerTurn; i++)
        {
            int randomIndex = Random.Range(0, blockPrefabs.Count);
            GameObject blockInstance = Instantiate(blockPrefabs[randomIndex], blockHolder);

            StartCoroutine(EnableIgnoreLayout());
        }
    }

    IEnumerator EnableIgnoreLayout()
    {
        // Force a layout rebuild
        LayoutRebuilder.ForceRebuildLayoutImmediate(blockHolder.GetComponent<RectTransform>());

        yield return null;

        foreach (Transform child in blockHolder)
        {
            LayoutElement layoutElement = child.GetComponent<LayoutElement>();
            if (layoutElement != null)
            {
                layoutElement.ignoreLayout = true;
            }
        }
    }
}
