using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using static Unity.Burst.Intrinsics.X86.Avx;

[System.Serializable]
public class VHSEffectPostProcessPass : ScriptableRenderPass
{
    // Used to render from camera to post processings
    // back and forth, until we render the final image to
    // the camera
    RTHandle source;
    RTHandle destinationA;
    RTHandle destinationB;
    RTHandle latestDest;

    readonly int temporaryRTIdA = Shader.PropertyToID("_TempRT");
    readonly int temporaryRTIdB = Shader.PropertyToID("_TempRTB");

    public VHSEffectPostProcessPass()
    {
        // Set the render pass event
        renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
    }

    public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
    {
        var desc = renderingData.cameraData.cameraTargetDescriptor;
        desc.depthBufferBits = 0;

        // Assign source as the main camera color target
        source = renderingData.cameraData.renderer.cameraColorTargetHandle;

        // Allocate temporary RTHandles for A and B
        RenderingUtils.ReAllocateHandleIfNeeded(ref destinationA, desc, name: "_TempRT_A");
        RenderingUtils.ReAllocateHandleIfNeeded(ref destinationB, desc, name: "_TempRT_B");
    }
    void DrawFullScreen(CommandBuffer cmd, RTHandle source, RTHandle destination, Material material, int passIndex = 0)
    {
        cmd.SetRenderTarget(destination, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store);
        cmd.SetGlobalTexture("_MainTex", source); // Ensure shader uses this name
        cmd.DrawMesh(RenderingUtils.fullscreenMesh, Matrix4x4.identity, material, 0, passIndex);
    }

    // The actual execution of the pass. This is where custom rendering occurs.
    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        // Skipping post processing rendering inside the scene View
        //if (renderingData.cameraData.isSceneViewCamera)
        //    return;

        // Here you get your materials from your custom class
        // (It's up to you! But here is how I did it)
        var materials = VHSEffectMaterialPointer.Instance;
        if (materials == null)
        {
            Debug.LogError("Custom Post Processing Materials instance is null");
            return;
        }

        CommandBuffer cmd = CommandBufferPool.Get("VHS Effect Post Processing");
        cmd.Clear();

        // This holds all the current Volumes information
        // which we will need later
        var stack = VolumeManager.instance.stack;

        #region Local Methods

        // Swaps render destinations back and forth, so that
        // we can have multiple passes and similar with only a few textures
        void BlitTo(Material mat, int pass = 0)
        {
            var first = latestDest;
            var last = first == destinationA ? destinationB : destinationA;
            Blit(cmd, first, last, mat, pass);

            latestDest = last;
        }

        #endregion

        // Starts with the camera source
        latestDest = source;

        //---Custom effect here---
        var customEffect = stack.GetComponent<VHSEffectComponent>();
        // Only process if the effect is active
        if (customEffect.IsActive())
        {
            var material = materials.VHSEffectMaterial;

            // P.s. optimize by caching the property ID somewhere else
            material.SetFloat(Shader.PropertyToID("_Intensity"), customEffect.intensity.value);
            material.SetColor(Shader.PropertyToID("StaticColor"), customEffect.noiseColor.value);
            material.SetFloat(Shader.PropertyToID("ScanLinesHeight"), customEffect.scanlinesHeight.value);

            BlitTo(material);
        }

        // Add any other custom effect/component you want, in your preferred order
        // Custom effect 2, 3 , ...


        // DONE! Now that we have processed all our custom effects, applies the final result to camera
        DrawFullScreen(cmd, latestDest, source, materials.VHSEffectMaterial);

        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
    }

    //Cleans the temporary RTs when we don't need them anymore
    public override void OnCameraCleanup(CommandBuffer cmd)
    {
        destinationA?.Release();
        destinationB?.Release();
    }
}