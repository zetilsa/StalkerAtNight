using UnityEngine;

public class FreelookCam : MonoBehaviour
{
    public float sensitivity = 2f;
    public Vector2 YawRange;
    public Vector2 PitchRange;

    private float rotationX;
    private float rotationY;

    void OnEnable()
    {
        // Mengunci kursor di tengah layar agar tidak mengganggu saat rotasi

    }

    void Update()
    {
        //if (Input.GetMouseButtonDown(1))
        //{
        //    rotationX = transform.localRotation.x;
        //    rotationY = transform.localRotation.y;
        //}

        if (Input.GetMouseButton(1))
        {
            // 1. Ambil input dari Mouse menggunakan Input.GetAxis
            float mouseX = Input.GetAxis("Mouse X") * sensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

            // 2. Akumulasi rotasi
            rotationY += mouseX; // Horizontal (Yaw)
            rotationX -= mouseY; // Vertikal (Pitch) - dikurangi agar gerakan mouse ke atas membuat kamera mendongak

            // 3. Batasi rotasi vertikal menggunakan Mathf.Clamp agar tidak jungkir balik
            rotationX = Mathf.Clamp(rotationX, PitchRange.x, PitchRange.y);
            rotationY = Mathf.Clamp(rotationY, YawRange.x, YawRange.y);
            // 4. Terapkan ke transform.localRotation menggunakan Quaternion.Euler
            transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0f);
        }
    }
}
