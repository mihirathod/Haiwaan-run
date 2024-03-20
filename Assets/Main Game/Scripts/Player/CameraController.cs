using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Var
    public Transform target;
    public Vector3 offset;
    public float smoothTime = 0.25f;
    Vector3 currentVelocity;
    #endregion

    #region Camera Follow Code
    private void LateUpdate()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;


        float positionX = Mathf.SmoothDamp(transform.position.x, target.position.x, ref currentVelocity.x, smoothTime);
        Vector3 newNonJmpFollowPosition = new Vector3(positionX, transform.position.y , target.position.z + offset.z);
        transform.position = newNonJmpFollowPosition;
    }
    #endregion
}
