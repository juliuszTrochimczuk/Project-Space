using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders;

public class PortalNode : MonoBehaviour
{
    public SceneInstance connectsTo;
    [SerializeField] private PlayerDetector entranceTrig;

    private void Awake()
    {
        if (entranceTrig is not null)
        {
            entranceTrig.onPlayerDetect.AddListener(x => connectsTo.ActivateAsync());
        }
    }
}
