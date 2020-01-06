using UnityEngine;

public class ConstantVelocity : MonoBehaviour
{

    [SerializeField]
    private Vector3 velocity;

    [SerializeField]
    private Space space;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(velocity * Time.deltaTime, space);
    }
}
