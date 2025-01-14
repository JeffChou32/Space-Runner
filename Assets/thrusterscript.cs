using UnityEngine;

public class thrusterscript : MonoBehaviour
{
    private Vector3 originalPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (shipscript.shipIsAlive == false)
        {
            gameObject.SetActive(false);
        }
        transform.localPosition = new Vector3(
                originalPosition.x,
                originalPosition.y * shipscript.multiplier,
                originalPosition.z
            );
    }
}
