using System.Collections;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private MeshRenderer portalDoor;
    [SerializeField] private Material portalMaterial;
    [SerializeField] private Camera pov;
    private bool playerOverlapping = false;
    private static bool canTeleport = true; 

    [SerializeField] private Camera playerCam;
    public GameObject player;
    private CharacterController playerController;

    [SerializeField] private Portal connection;

    private void Awake()
    {
        portalDoor.material = new Material(portalMaterial);
        RenderTexture texture = new RenderTexture(Screen.width, Screen.height, 24);
        portalDoor.material.mainTexture = texture;
        connection.pov.targetTexture = texture;
        playerController = player.GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector3 distToPlayer = player.transform.position - connection.transform.position;
        pov.transform.position = transform.position + distToPlayer;

        float portalsRotDelta = Quaternion.Angle(transform.rotation, pov.transform.rotation);
        Quaternion toRotate = Quaternion.AngleAxis(portalsRotDelta, Vector3.up);
        pov.transform.rotation = Quaternion.LookRotation(toRotate * playerCam.transform.forward, Vector3.up);
        
        if (playerOverlapping && canTeleport)
        {
            float dot = Vector3.Dot(transform.up, transform.position - player.transform.position);

            if (dot < 0)
            {
                StartCoroutine(Teleportation(distToPlayer));
            }
        }
    }
    
    private IEnumerator Teleportation(Vector3 distToPlayer)
    {
        playerController.enabled = false;

        canTeleport = false;
        float rotDiff = -Quaternion.Angle(transform.rotation, connection.transform.rotation);
        rotDiff += 180;
        player.transform.Rotate(Vector3.up, rotDiff);

        Vector3 playerOffset = Quaternion.Euler(0f, rotDiff, 0f) * distToPlayer;
        player.transform.position = connection.transform.position;
        playerOverlapping = false;

        yield return new WaitForEndOfFrame();

        playerController.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        playerOverlapping = true;
    }

    private void OnTriggerExit(Collider other)
    {
        playerOverlapping = false;
        canTeleport = true;
    }
}
