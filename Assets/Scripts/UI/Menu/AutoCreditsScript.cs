using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class AutoCreditsScript : MonoBehaviour
{
    public float fadeInDuration = 1f;
    public float stayDuration = 3f;
    public float fadeOutDuration = 1f;

    private CanvasGroup parentCanvasGroup;
    [SerializeField] private CanvasGroup[] childCanvasGroups;

    private Sequence mainSequence; // Simpan referensi sequence
    private bool isRunning = false;

    void Awake()
    {
        parentCanvasGroup = GetComponent<CanvasGroup>();
        // Sembunyikan semua child di awal
        foreach (var cg in childCanvasGroups) cg.alpha = 0;
    }

    void Update()
    {
        // Jika sedang berjalan dan klik kiri ditekan
        if (isRunning && Input.GetMouseButtonDown(0))
        {
            SkipToNext();
        }
    }

    public void Aktif()
    {
        if (isRunning) return; // Mencegah double klik aktif

        isRunning = true;
        parentCanvasGroup.alpha = 1;
        parentCanvasGroup.blocksRaycasts = true;

        mainSequence = DOTween.Sequence();

        foreach (CanvasGroup cg in childCanvasGroups)
        {
            // Tambahkan label di tiap awal animasi child untuk navigasi
            mainSequence.Append(cg.DOFade(1, fadeInDuration));
            mainSequence.AppendInterval(stayDuration);
            mainSequence.Append(cg.DOFade(0, fadeOutDuration));
        }

        mainSequence.OnComplete(() =>
        {
            parentCanvasGroup.DOFade(0, fadeOutDuration).OnComplete(() =>
            {
                parentCanvasGroup.blocksRaycasts = false;
                isRunning = false;
            });
        });
    }

    private void SkipToNext()
    {
        // Ambil waktu posisi saat ini dalam sequence
        float currentTime = mainSequence.Elapsed();

        // Cari durasi total satu siklus (fade in + stay + fade out)
        float cycleDuration = fadeInDuration + stayDuration + fadeOutDuration;

        // Hitung sisa waktu di fase 'Fade In' atau 'Stay' untuk langsung lompat ke 'Fade Out'
        float timeInCurrentCycle = currentTime % cycleDuration;

        if (timeInCurrentCycle < (fadeInDuration + stayDuration))
        {
            // Lompat ke titik awal Fade Out pada child yang sedang aktif
            float nextFadeOutTime = (Mathf.Floor(currentTime / cycleDuration) * cycleDuration) + fadeInDuration + stayDuration;
            mainSequence.Goto(nextFadeOutTime, true);
        }
    }
}