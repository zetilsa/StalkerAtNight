using UnityEngine;

public class FreelookCam : MonoBehaviour
{
    public float sensitivity = 2f;
    public Vector2 YawRange = new Vector2(-180, 180); // Contoh default range
    public Vector2 PitchRange = new Vector2(-80, 80);

    private float rotationX;
    private float rotationY;

    void Start()
    {
        // 1. Ambil rotasi awal dari transform yang sudah diatur di Inspector
        Vector3 currentRotation = transform.localEulerAngles;

        // 2. Masukkan ke variabel akumulasi agar sinkron
        // Catatan: Unity menyimpan sudut 0-360. 
        // Untuk Pitch, kita perlu mengubahnya menjadi rentang negatif jika > 180
        rotationX = (currentRotation.x > 180) ? currentRotation.x - 360 : currentRotation.x;
        rotationY = (currentRotation.y > 180) ? currentRotation.y - 360 : currentRotation.y;
    }

    void OnEnable()
    {
        // Tips: Mengunci kursor bisa ditaruh di sini jika diinginkan
        // Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X") * sensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

            rotationY += mouseX;
            rotationX -= mouseY;

            rotationX = Mathf.Clamp(rotationX, PitchRange.x, PitchRange.y);
            rotationY = Mathf.Clamp(rotationY, YawRange.x, YawRange.y);

            transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0f);
        }
    }
}