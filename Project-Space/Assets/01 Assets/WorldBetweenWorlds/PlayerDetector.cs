using UnityEngine;
using UnityEngine.Events;

public class PlayerDetector : MonoBehaviour
{
    public UnityEvent<Collider> onPlayerDetect;

    private void OnTriggerEnter(Collider other)
    {
        onPlayerDetect?.Invoke(other);
    }
}
