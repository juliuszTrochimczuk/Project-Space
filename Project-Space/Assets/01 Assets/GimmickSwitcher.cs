using UnityEngine;

public class GimmickSwitcher : MonoBehaviour
{
    [SerializeField]
    private GameObject componentsPref;

    private GameObject components;

    private void OnTriggerEnter(Collider other)
    {
        components = Instantiate(componentsPref, other.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        Destroy(components);
    }
}
