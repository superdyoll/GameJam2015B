using UnityEngine;
using System.Collections;
public class cameraShake : MonoBehaviour
{
    private Vector3 originPosition;
    private Quaternion originRotation;
    public float shake_decay;
    public float shake_intensity;

    void Update()
    {
        if (shake_intensity > 0)
        {
            transform.position = originPosition + Random.insideUnitSphere * shake_intensity;
            transform.rotation = new Quaternion(
            originRotation.x + Random.Range(-shake_intensity, shake_intensity) * .1f,
            originRotation.y + Random.Range(-shake_intensity, shake_intensity) * .1f,
            originRotation.z,
            originRotation.w);
            shake_intensity -= shake_decay;
        }
    }

	void Start(){
		Shake ();
	}

    public void Shake()
    {
        originPosition = transform.position;
        originRotation = transform.rotation;
        shake_intensity = .1f;
        shake_decay = 0.002f;
    }
}
