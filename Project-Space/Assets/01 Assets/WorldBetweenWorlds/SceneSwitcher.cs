using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace WorldBetweenWorlds
{
    public class SceneSwitcher : MonoBehaviour
    {
        [SerializeField] private Portal portal;
        [SerializeField] AssetReference nextScene;
        private AsyncOperationHandle<SceneInstance> sceneLoadingHandle;
        [SerializeField] private GameObject corridorNodePref;
        [SerializeField] private GameObject portalNodePref;
        private Transform lastNode;

        private void Awake()
        {
            lastNode = transform;
        }

        private void SpawnNextNode()
        {
            if (sceneLoadingHandle.IsDone)
            {
                lastNode = Instantiate(portalNodePref, lastNode.position + lastNode.forward * 10, transform.rotation).transform;
                lastNode.GetComponent<PortalNode>().connectsTo = sceneLoadingHandle.Result;
            }
            else
            {
                lastNode = Instantiate(corridorNodePref, lastNode.position + lastNode.forward * 10, transform.rotation).transform;
                lastNode.GetComponent<PlayerDetector>().onPlayerDetect.AddListener(x => SpawnNextNode());
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!sceneLoadingHandle.IsValid())
            {
                sceneLoadingHandle = Addressables.LoadSceneAsync(nextScene, LoadSceneMode.Single, false);
                SpawnNextNode();
            }
        }
    }
}