using System.Collections;
using UnityEngine;

public class StackCollector : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Inventory inventory = other.GetComponent<Inventory>();
        if (!inventory) return;

        if (inventory.GetRigidbodiesCounts.Count <= 0) return;
        StartCoroutine(RemoveStacks(inventory));
    }
    private void OnTriggerExit(Collider other)
    {
        Inventory inventory = other.GetComponent<Inventory>();
        if (!inventory) return;
        StopAllCoroutines();

    }
    IEnumerator  RemoveStacks(Inventory inventory)
    {

        while (inventory.GetRigidbodiesCounts.Count > 0)
        {
            inventory.RemoveBodyFromStack();
            
            yield return new WaitForSecondsRealtime(0.5f);
        }
        StopAllCoroutines();
    }
}
