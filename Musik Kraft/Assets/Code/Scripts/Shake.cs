using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public GameObject camera;

    public float duration = 1f;

    public AnimationCurve curve;
    // Update is called once per frame
    public void ShakeCamera()
    {
        StartCoroutine(Shaking());
    }
    
    IEnumerator Shaking()
    {
        Vector3 startPosition = camera.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            
            camera.transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }

        camera.transform.position = startPosition;
    }
}
