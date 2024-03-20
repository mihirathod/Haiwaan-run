using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotator : MonoBehaviour
{
    float rotateSpeed;
    bool isFollow;
    public float followSpeed;

    private void Start()
    {
        rotateSpeed = Random.Range(1, 2);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.left * rotateSpeed);

        if (isFollow)
        {
            transform.position = Vector3.MoveTowards(transform.position, PlayerController.Instance.coinCollecter.transform.position, followSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "MagneticFeild")
        {
            isFollow = true;
        }
    }
}
