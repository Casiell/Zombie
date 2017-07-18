using UnityEngine;

public class CameraControler : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;

    // Use this for initialization
    private void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}