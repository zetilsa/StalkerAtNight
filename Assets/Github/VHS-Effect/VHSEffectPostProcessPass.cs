using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Unity.Collections; // Ditambahkan untuk kelengkapan, meskipun mungkin tidak selalu diperlukan

[System.Serializable]
public class VHSEffectPostProcessPass : ScriptableRenderPass
{
    private RTHandle destinationA;
    private RTHandle destinationB;
    private RTHandle latestDest;

    public VHSEffectPostProcessPass()
    {
        renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
    }

    public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
    {
        RenderTextureDescriptor descriptor = renderingData.cameraData.cameraTargetDescriptor;
        descriptor.depthBufferBits = 0;

        // Menggantikan GetTemporaryRT dengan RTHandle.
        RenderingUtils.ReAllocateIfNeeded(ref destinationA, descriptor, FilterMode.Bilinear, TextureWrapMode.Clamp, name: "_TempRT");
        RenderingUtils.ReAllocateIfNeeded(ref destinationB, descriptor, FilterMode.Bilinear, TextureWrapMode.Clamp, name: "_TempRTB");
    }

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        if (renderingData.cameraData.isSceneViewCamera)
            return;

        var materials = VHSEffectMaterialPointer.Instance;
        if (materials == null)
        {
            Debug.LogError("Custom Post Processing Materials instance is null");
            return;
        }

        CommandBuffer cmd = CommandBufferPool.Get("VHS Effect Post Processing");
        cmd.Clear();

        var stack = VolumeManager.instance.stack;

        // **Dihapus:** Menghilangkan definisi RenderTargetIdentifier sourceRTI yang menyebabkan CS0117.
        // RenderTargetIdentifier sourceRTI = renderingData.cameraData.renderer.cameraColorTargetHandle.colorAttachment;

        #region Local Methods

        void BlitTo(Material mat, int pass = 0)
        {
            RTHandle first = latestDest;
            RTHandle last = first == destinationA ? destinationB : destinationA;

            Blitter.BlitCameraTexture(cmd, first, last, mat, pass);

            latestDest = last;
        }

        #endregion

        // Baris ~60: Inisialisasi latestDest dengan RTHandle target kamera.
        latestDest = renderingData.cameraData.renderer.cameraColorTargetHandle;

        //---Custom effect here---
        var customEffect = stack.GetComponent<VHSEffectComponent>();
        if (customEffect != null && customEffect.IsActive())
        {
            var material = materials.VHSEffectMaterial;

            material.SetFloat(Shader.PropertyToID("_Intensity"), customEffect.intensity.value);
            material.SetColor(Shader.PropertyToID("StaticColor"), customEffect.noiseColor.value);
            material.SetFloat(Shader.PropertyToID("ScanLinesHeight"), customEffect.scanlinesHeight.value);

            BlitTo(material);
        }

        // DONE! Applies the final result to camera
        // Dapatkan RTHandle target kamera sebagai tujuan akhir
        RTHandle finalDestinationHandle = renderingData.cameraData.renderer.cameraColorTargetHandle;

        // Baris ~105 (Perbaikan CS1503): Blit dari RTHandle ke RTHandle
        Blitter.BlitCameraTexture(cmd, latestDest, finalDestinationHandle);

        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
    }

    public override void OnCameraCleanup(CommandBuffer cmd)
    {
        // Pembersihan RTHandle
        destinationA?.Release();
        destinationB?.Release();
    }
}