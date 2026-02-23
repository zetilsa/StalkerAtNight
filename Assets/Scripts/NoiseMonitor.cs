using UnityEngine;

public class NoiseMonitor : MonoBehaviour
{
    [SerializeField] float value;
    [SerializeField] float hasil;
    [SerializeField]Material noisematerial;
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] Vector2 VolumeRange;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        noisematerial = meshRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        print(noisematerial.GetFloat("_NoisePower"));
        value = noisematerial.GetFloat("_NoisePower");
        hasil = Mathf.Lerp(VolumeRange.x,VolumeRange.y,(value * 100) / 12);
    }
}
