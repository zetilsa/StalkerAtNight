using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Volume))]
public class PostProcessingModifier : MonoBehaviour
{
    public static PostProcessingModifier instance { get; private set; }
    [Header("Depth Of Field")]
    [SerializeField, Tooltip("Jarak fokus lensa (0 - 100 meter/unit)")]
    private float _dofFocusDistance = 10;

    [Header("Vignette")]
    [SerializeField, Range(0,1 ), Tooltip("Intensitas kegelapan di sudut layar (0% - 100%)")]
    private float _vignetteIntensity = 0;

    [Header("Chromatic Aberration")]
    [SerializeField, Range(0, 1), Tooltip("Intensitas distorsi warna lensa (0% - 100%)")]
    private float _chromaticIntensity = 0;

    [Header("Color Adjustment")]
    [SerializeField, Range(-1, 1), Tooltip("Kecerahan eksposur (Contoh: 15 = 1.5 EV)")]
    private float _postExposure = 0;

    [SerializeField, Range(-1, 1), Tooltip("Kontras warna (-100 sampai 100)")]
    private float _contrast = 0;

    [SerializeField, Range(-1, 1), Tooltip("Saturasi warna (-100 = Abu-abu/Hitam Putih)")]
    private float _saturation = 0;


    // --- CACHE KOMPONEN (Untuk optimasi performa) ---
    private VolumeProfile _profile;
    private DepthOfField _depthOfField;
    private Vignette _vignette;
    private ChromaticAberration _chromaticAberration;
    private ColorAdjustments _colorAdjustments;


    // --- PROPERTIES UNTUK DIAKSES SCRIPT LAIN SECARA REALTIME ---
    public float DofFocusDistance
    {
        get => _dofFocusDistance;
        set { if (_dofFocusDistance != value) { _dofFocusDistance = value; UpdateDOF(); } }
    }

    public float VignetteIntensity
    {
        get => _vignetteIntensity;
        set { if (_vignetteIntensity != value) { _vignetteIntensity = value; UpdateVignette(); } }
    }

    public float ChromaticIntensity
    {
        get => _chromaticIntensity;
        set { if (_chromaticIntensity != value) { _chromaticIntensity = value; UpdateChromatic(); } }
    }

    public float PostExposure
    {
        get => _postExposure;
        set { if (_postExposure != value) { _postExposure = value; UpdateColorAdjustments(); } }
    }

    public float Contrast
    {
        get => _contrast;
        set { if (_contrast != value) { _contrast = value; UpdateColorAdjustments(); } }
    }

    public float Saturation
    {
        get => _saturation;
        set { if (_saturation != value) { _saturation = value; UpdateColorAdjustments(); } }
    }


    private void Awake()
    {
        if (PostProcessingModifier.instance != null)
        {
            instance = this;
        }
        else
        {
            Destroy(PostProcessingModifier.instance);
            instance = this;
        }
    
            InitializeVolume();
    }

    private void InitializeVolume()
    {
        // Menduplikasi profil asli ke memori agar asset permanen tidak berubah
        _profile = GetComponent<Volume>().profile;

        // Mengambil referensi komponen & memaksa override aktif
        if (_profile.TryGet(out _depthOfField))
            _depthOfField.focusDistance.overrideState = true;

        if (_profile.TryGet(out _vignette))
            _vignette.intensity.overrideState = true;

        if (_profile.TryGet(out _chromaticAberration))
            _chromaticAberration.intensity.overrideState = true;

        if (_profile.TryGet(out _colorAdjustments))
        {
            _colorAdjustments.postExposure.overrideState = true;
            _colorAdjustments.contrast.overrideState = true;
            _colorAdjustments.saturation.overrideState = true;
        }

        // Terapkan nilai inspector ke volume saat game dimulai
        ApplyAllSettings();
    }

    // --- FUNGSI UPDATE PARAMETER (Hanya berjalan jika nilai diubah) ---
    private void UpdateDOF()
    {
        if (_depthOfField != null)
            _depthOfField.focusDistance.value = Mathf.Max(0.1f, _dofFocusDistance); // Mencegah nilai 0 yang kadang menyebabkan error DOF
    }

    private void UpdateVignette()
    {
        if (_vignette != null)
            _vignette.intensity.value = _vignetteIntensity;
    }

    private void UpdateChromatic()
    {
        if (_chromaticAberration != null)
            _chromaticAberration.intensity.value = _chromaticIntensity;
    }

    private void UpdateColorAdjustments()
    {
        if (_colorAdjustments != null)
        {
            _colorAdjustments.postExposure.value = _postExposure; // Int 15 menjadi 1.5 Float
            _colorAdjustments.contrast.value = _contrast;
            _colorAdjustments.saturation.value = _saturation;
        }
    }

    private void ApplyAllSettings()
    {
        UpdateDOF();
        UpdateVignette();
        UpdateChromatic();
        UpdateColorAdjustments();
    }

    // --- FITUR EDITOR UNTUK TESTING ---
    private void OnValidate()
    {
        if (Application.isPlaying && _profile != null)
        {
            ApplyAllSettings();
        }
    }
}