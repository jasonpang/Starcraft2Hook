using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using EasyHook;
using SharpDX;
using SharpDX.Direct3D9;
using Rectangle = SharpDX.Rectangle;

namespace Game.Hooks.Graphics
{
    /*
     * 
            try
            {
                Log.LogMethodSignatureTypesAndValues(devicePointer, presentParameters.PrintNamesValues());
                GetOrCreateDevice(devicePointer);
                Device.Reset(presentParameters);
            }
            catch (Exception ex)
            {
                Log.Warn(ex.ToString());
            }

            return Result.Ok;
     */

    public partial class Direct3D9Hook
    {
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result BeginSceneDelegate(IntPtr devicePointer);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result BeginStateBlocKDelegate(IntPtr devicePointer);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result ClearDelegate(IntPtr devicePointer, int count, IntPtr rects, ClearFlags flags, ColorBGRA color, float z, int stencil);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result ColorFillDelegate(IntPtr devicePointer, IntPtr surface, IntPtr rect, ColorBGRA color);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result CreateAdditionalSwapChainDelegate(IntPtr devicePointer, [In, Out] PresentParameters presentParameters, out SwapChain swapChain);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result CreateCubeTextureDelegate(IntPtr devicePointer, int edgeLength, int levels, Usage usage, Format format, Pool pool, out IntPtr cubeTexture, IntPtr sharedHandle);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result CreateDepthStencilSurfaceDelegate(IntPtr devicePointer, int width, int height, Format format, MultisampleType multiSampleType, int multiSampleQuality, bool discard, out IntPtr surface, IntPtr sharedHandle);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result CreateIndexBufferDelegate(IntPtr devicePointer, int length, Usage usage, Format format, Pool pool, out IntPtr indexBuffer, IntPtr sharedHandle);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result CreateOffscreenPlainSurfaceDelegate(IntPtr devicePointer, int width, int height, Format format, Pool pool, out IntPtr surface, IntPtr sharedHandle);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result CreatePixelShaderDelegate(IntPtr devicePointer, IntPtr shaderBytecode, out IntPtr pixelShader);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private unsafe delegate Result CreateQueryDelegate(IntPtr devicePointer, QueryType queryType, IntPtr** queryInterface);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result CreateRenderTargetDelegate(IntPtr devicePointer, int width, int height, Format format, MultisampleType multiSampleType, int multiSampleQuality, bool lockable, out IntPtr surface, IntPtr sharedHandle);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result CreateStateBlockDelegate(IntPtr devicePointer, StateBlockType type, out IntPtr stateBlock);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result CreateTextureDelegate(IntPtr devicePointer, int width, int height, int levels, Usage usage, Format format, Pool pool, out IntPtr texture, IntPtr sharedHandle);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result CreateVertexBufferDelegate(IntPtr devicePointer, int length, Usage usage, VertexFormat vertexFormat, Pool pool, out IntPtr vertexBuffer, IntPtr sharedHandle);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result CreateVertexDeclarationDelegate(IntPtr devicePointer, IntPtr vertexElements, out IntPtr vertexDeclaration);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result CreateVertexShaderDelegate(IntPtr devicePointer, byte[] shaderBytecode, out IntPtr vertexShader);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result CreateVolumeTextureDelegate(IntPtr devicePointer, int width, int height, int depth, int levels, Usage usage, Format format, Pool pool, out IntPtr volumeTexture, IntPtr sharedHandle);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result DeletePatchDelegate(IntPtr devicePointer, int handle);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result DrawIndexedPrimitiveDelegate(IntPtr devicePointer, PrimitiveType primitiveType, int baseVertexIndex, int minVertexIndex, int numVertices, int startIndex, int primitiveCount);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result DrawIndexedPrimitiveUPDelegate(IntPtr devicePointer, PrimitiveType primitiveType, int minVertexIndex, int numVertices, int primitiveCount, IntPtr IndexData, Format indexDataFormat, IntPtr vertexStreamZeroData, int vertexStreamZeroStride);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result DrawPrimitiveDelegate(IntPtr devicePointer, PrimitiveType primitiveType, int startVertex, int primitiveCount);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result DrawPrimitiveUPDelegate(IntPtr devicePointer, PrimitiveType primitiveType, int primitiveCount, IntPtr vertexStreamZeroData, int vertexStreamZeroStride);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result DrawRectPatchDelegate(IntPtr devicePointer, int handle, float[] numSegments, IntPtr rectPatchInfo);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result DrawTriPatchDelegate(IntPtr devicePointer, int handle, float[] numSegments, IntPtr triPatchInfo);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result EndSceneDelegate(IntPtr devicePointer);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result EndStateBlockDelegate(IntPtr devicePointer, [In, Out] StateBlock stateBlock);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result EvictManagedResourcesDelegate(IntPtr devicePointer);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate long GetAvailableTextureMemDelegate(IntPtr devicePointer);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result GetBackBufferDelegate(IntPtr devicePointer, int swapChainIndex, int backBufferIndex, BackBufferType backBufferType, out Surface backBuffer);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result GetClipPlaneDelegate(IntPtr devicePointer, int index, out float[] plane);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result GetClipStatusDelegate(IntPtr devicePointer, out ClipStatus clipStatus);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result GetCreationParametersDelegate(IntPtr devicePointer, out CreationParameters parameters);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result GetCurrentTexturePaletteDelegate(IntPtr devicePointer, IntPtr paletteNumber);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result GetDepthStencilSurfaceDelegate(IntPtr devicePointer, out IntPtr stencilSurface);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result GetDeviceCapsDelegate(IntPtr devicePointer, Capabilities caps);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result GetDirect3DDelegate(IntPtr devicePointer, out IntPtr direct3D);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result GetDisplayModeDelegate(IntPtr devicePointer, uint swapChainIndex, DisplayMode mode);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result GetFrontBufferDataDelegate(IntPtr devicePointer, int swapChainIndex, IntPtr destSurface);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result GetFVFDelegate(IntPtr devicePointer, out int fvf);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result GetGammaRampDelegate(IntPtr devicePointer, int swapChainIndex, out IntPtr gammaRamp);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result GetIndicesDelegate(IntPtr devicePointer, out IntPtr indexBuffer);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result GetLightDelegate(IntPtr devicePointer, int index, out IntPtr light);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result GetLightEnableDelegate(IntPtr devicePointer, int index, out int enabled);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result GetMaterialDelegate(IntPtr devicePointer, int index, out IntPtr material);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate double GetNPatchModeDelegate(IntPtr devicePointer);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result GetNumberOfSwapChainsDelegate(IntPtr devicePointer);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result GetPaletteEntriesDelegate(IntPtr devicePointer, int paletteNumber, IntPtr entries);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result GetPixelShaderConstantBDelegate(IntPtr devicePointer, int startRegister, [In, Out] int[] data, int vector4BoolsCount);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result GetPixelShaderConstantFDelegate(IntPtr devicePointer, int startRegister, [In, Out] double[] data, int vector4FloatsCount);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result GetPixelShaderConstantIDelegate(IntPtr devicePointer, int startRegister, [In, Out] double[] data, int vector4IntsCount);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result GetPixelShaderDelegate(IntPtr devicePointer, out IntPtr pixelShader);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result GetRasterStatusDelegate(IntPtr devicePointer, int swapChainIndex, IntPtr rasterStatus);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result GetRenderStateDelegate(IntPtr devicePointer, RenderState state, out int value);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result GetRenderTargetDataDelegate(IntPtr devicePointer, out IntPtr destSurface);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result GetRenderTargetDelegate(IntPtr devicePointer, int renderTargetIndex, out IntPtr renderTarget);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result GetSamplerStateDelegate(IntPtr devicePointer, int sampler, SamplerState type, IntPtr value);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result GetScissorRectDelegate(IntPtr devicePointer, out IntPtr rect);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate int GetSoftwareVertexProcessingDelegate(IntPtr devicePointer, int software);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result GetStreamSourceDelegate(IntPtr devicePointer, int streamNumber, out IntPtr streamVertexBufferData, out IntPtr OffsetInBytes, out IntPtr stride);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result GetStreamSourceFreqDelegate(IntPtr devicePointer, int streamNumber, out int freqDivider);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result GetSwapChainDelegate(IntPtr devicePointer, uint swapChainIndex, out IntPtr swapChain);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate BaseTexture GetTextureDelegate(IntPtr devicePointer, int stage);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result GetTextureStageStateDelegate(IntPtr devicePointer, int stage, TextureStage type, IntPtr value);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result GetTransformDelegate(IntPtr devicePointer, TransformState transformState, out IntPtr matrix);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result GetVertexDeclarationDelegate(IntPtr devicePointer, out IntPtr vertexDeclaration);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result GetVertexShaderConstantBDelegate(IntPtr devicePointer, int startRegister, [In, Out] int[] bools, int vector4BoolCounts);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result GetVertexShaderConstantFDelegate(IntPtr devicePointer, int startRegister, [In, Out] double[] floats, int vector4FloatCount);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result GetVertexShaderConstantIDelegate(IntPtr devicePointer, int startRegister, [In, Out] double[] ints, int vector4IntCount);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result GetVertexShaderDelegate(IntPtr devicePointer, out IntPtr vertexShader);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result GetViewportDelegate(IntPtr devicePointer, int index, out IntPtr viewPort);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result LightEnableDelegate(IntPtr devicePointer, int index, int enabled);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result MultiplyTransformDelegate(IntPtr devicePointer, TransformState transformState, IntPtr matrix);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result PresentDelegate(IntPtr devicePointer, Rectangle sourceRect, Rectangle destRect, IntPtr destWindowOverride, ref Region dirtyRegion);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result ProcessVerticesDelegate(IntPtr devicePointer, int srcStartIndex, int destIndex, int vertexCount, IntPtr destVertexBuffer, IntPtr vertexDeclaration, int flags);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result ResetDelegate(IntPtr devicePointer, params PresentParameters[] presentParameters);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result SetClipPlaneDelegate(IntPtr devicePointer, int index, double[] plane);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result SetClipStatusDelegate(IntPtr devicePointer, IntPtr clipStatus);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result SetCurrentTexturePaletteDelegate(IntPtr devicePointer, int paletteNumber);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate void SetCursorPositionDelegate(IntPtr devicePointer, int x, int y, bool cursorBitmap);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result SetCursorPropertiesDelegate(IntPtr devicePointer, uint xHotSpot, uint yHotSpot, IntPtr cursorBitmap);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result SetDepthStencilSurfaceDelegate(IntPtr devicePointer, out IntPtr stencilSurface);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result SetDialogBoxModeDelegate(IntPtr devicePointer, int enableDialogs);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result SetFVFDelegate(IntPtr devicePointer, int fvf);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result SetGammaRampDelegate(IntPtr devicePointer, int swapChainIndex, int flags, IntPtr gammaRamp);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result SetIndicesDelegate(IntPtr devicePointer, IntPtr indexBuffer);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result SetLightDelegate(IntPtr devicePointer, int index, IntPtr light);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result SetMaterialDelegate(IntPtr devicePointer, int index, IntPtr material);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate int SetNPatchModeDelegate(IntPtr devicePointer, double nSegments);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result SetPaletteEntriesDelegate(IntPtr devicePointer, int paletteNumber, IntPtr entries);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result SetPixelShaderConstantBDelegate(IntPtr devicePointer, int startRegister, int[] data, int vector4BoolsCount);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result SetPixelShaderConstantFDelegate(IntPtr devicePointer, int startRegister, double[] data, int vector4FloatsCount);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result SetPixelShaderConstantIDelegate(IntPtr devicePointer, int startRegister, double[] data, int vector4IntsCount);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result SetPixelShaderDelegate(IntPtr devicePointer, IntPtr pixelShader);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result SetRenderStateDelegate(IntPtr devicePointer, RenderState state, int value);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result SetRenderTargetDelegate(IntPtr devicePointer, int renderTargetIndex, IntPtr renderTarget);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result SetSamplerStateDelegate(IntPtr devicePointer, int sampler, SamplerState type, int value);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result SetScissorRectDelegate(IntPtr devicePointer, IntPtr rect);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result SetSoftwareVertexProcessingDelegate(IntPtr devicePointer, int software);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result SetStreamSourceDelegate(IntPtr devicePointer, int streamNumber, IntPtr streamVertexBufferData, int offsetInBytes, int stride);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result SetStreamSourceFreqDelegate(IntPtr devicePointer, int streamNumber, int setting);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result SetTextureDelegate(IntPtr devicePointer, int stage, IntPtr texture);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result SetTextureStageStateDelegate(IntPtr devicePointer, int stage, TextureStage type, int value);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result SetTransformDelegate(IntPtr devicePointer, TransformState transformState, IntPtr matrix);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result SetVertexDeclarationDelegate(IntPtr devicePointer, IntPtr vertexDeclaration);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result SetVertexShaderConstantBDelegate(IntPtr devicePointer, int startRegister, int[] bools, int vector4BoolCounts);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result SetVertexShaderConstantFDelegate(IntPtr devicePointer, int startRegister, double[] floats, int vector4FloatCount);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result SetVertexShaderConstantIDelegate(IntPtr devicePointer, int startRegister, int[] ints, int vector4IntCount);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result SetVertexShaderDelegate(IntPtr devicePointer, IntPtr vertexShader);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result SetViewportDelegate(IntPtr devicePointer, int index, IntPtr viewPort);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result ShowCursorDelegate(IntPtr devicePointer, bool show);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result StretchRectDelegate(IntPtr devicePointer, IntPtr sourceSurface, IntPtr sourceRect, IntPtr destSurface, IntPtr destRect, TextureFilter filterType);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result TestCooperativeLevelDelegate(IntPtr devicePointer);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result UpdateSurfaceDelegate(IntPtr devicePointer, IntPtr sourceSurface, IntPtr sourceRect, IntPtr destSurface, IntPtr destRect);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result UpdateTextureDelegate(IntPtr devicePointer, IntPtr sourceTexture, IntPtr destTexture);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result ValidateDeviceDelegate(IntPtr devicePointer, IntPtr numPasses);

        private List<CubeTexture> CubeTextures { get; set; }

        private Dictionary<Direct3D9DeviceFunctions, LocalHook> Hooks { get; set; }

        private List<Direct3D9DeviceFunctions> ExcludedHooks = new List <Direct3D9DeviceFunctions>(120);

        private List<IndexBuffer> IndexBuffers { get; set; }

        private List<PixelShader> PixelShaders { get; set; }

        private List<Query> Queries { get; set; }

        private List<StateBlock> StateBlocks { get; set; }

        private List<Surface> Surfaces { get; set; }

        private List<SwapChain> SwapChains { get; set; }

        private List<Texture> Textures { get; set; }

        private List<VertexBuffer> VertexBuffers { get; set; }

        private List<VertexDeclaration> VertexDeclarations { get; set; }

        private List<VertexShader> VertexShaders { get; set; }

        private List<VolumeTexture> VolumeTextures { get; set; }

        private void AddHook(Direct3D9DeviceFunctions function, Delegate callback)
        {
            if (!this.ExcludedHooks.Contains (function))
                this.Hooks.Add(function, LocalHook.Create(GetAddressOf(function), callback, this));
        }

        private void InstallDirect3D9Hooks()
        {
            this.Hooks = new Dictionary<Direct3D9DeviceFunctions, LocalHook>(120);
            this.SwapChains = new List<SwapChain>();
            this.Textures = new List<Texture>();
            this.CubeTextures = new List<CubeTexture>();
            this.VolumeTextures = new List<VolumeTexture>();
            this.Surfaces = new List<Surface>();
            this.IndexBuffers = new List<IndexBuffer>();
            this.PixelShaders = new List<PixelShader>();
            this.VertexShaders = new List<VertexShader>();
            this.Queries = new List<Query>();
            this.StateBlocks = new List<StateBlock>();
            this.VertexBuffers = new List<VertexBuffer>();
            this.VertexDeclarations = new List<VertexDeclaration>();

            this.AddHook(Direct3D9DeviceFunctions.BeginScene, new BeginSceneDelegate(this.BeginScene));
            this.AddHook(Direct3D9DeviceFunctions.BeginStateBlock, new BeginSceneDelegate(this.BeginStateBlock));
            this.AddHook(Direct3D9DeviceFunctions.Clear, new ClearDelegate(this.Clear));
            this.AddHook(Direct3D9DeviceFunctions.ColorFill, new ColorFillDelegate(this.ColorFill));
            this.AddHook(Direct3D9DeviceFunctions.CreateAdditionalSwapChain, new CreateAdditionalSwapChainDelegate(this.CreateAdditionalSwapChain));
            this.AddHook(Direct3D9DeviceFunctions.CreateCubeTexture, new CreateCubeTextureDelegate(this.CreateCubeTexture));
            this.AddHook(Direct3D9DeviceFunctions.CreateDepthStencilSurface, new CreateDepthStencilSurfaceDelegate(this.CreateDepthStencilSurface));            
            this.AddHook(Direct3D9DeviceFunctions.CreateIndexBuffer, new CreateIndexBufferDelegate(this.CreateIndexBuffer));
            this.AddHook(Direct3D9DeviceFunctions.CreateOffscreenPlainSurface, new CreateOffscreenPlainSurfaceDelegate(this.CreateOffscreenPlainSurface));
//          AddHook(Direct3D9DeviceFunctions.CreatePixelShader, new CreatePixelShaderDelegate(CreatePixelShader)); /* TODO Last, need to write dissassembler */
            this.AddHook(Direct3D9DeviceFunctions.CreateRenderTarget, new CreateRenderTargetDelegate(this.CreateRenderTarget));
            this.AddHook(Direct3D9DeviceFunctions.CreateStateBlock, new CreateStateBlockDelegate(this.CreateStateBlock));
            this.AddHook(Direct3D9DeviceFunctions.CreateTexture, new CreateTextureDelegate(this.CreateTexture));
            this.AddHook(Direct3D9DeviceFunctions.CreateVertexBuffer, new CreateVertexBufferDelegate(this.CreateVertexBuffer));
            this.AddHook(Direct3D9DeviceFunctions.CreateVertexDeclaration, new CreateVertexDeclarationDelegate(this.CreateVertexDeclaration));
//          AddHook(Direct3D9DeviceFunctions.CreateVertexShader, new CreateVertexShaderDelegate(CreateVertexShader)); /* TODO last, need to write dissassembler */
            this.AddHook(Direct3D9DeviceFunctions.CreateVolumeTexture, new CreateVolumeTextureDelegate(this.CreateVolumeTexture));
            this.AddHook(Direct3D9DeviceFunctions.DeletePatch, new DeletePatchDelegate(this.DeletePatch));
            this.AddHook(Direct3D9DeviceFunctions.DrawIndexedPrimitive, new DrawIndexedPrimitiveDelegate(this.DrawIndexedPrimitive));
            this.AddHook(Direct3D9DeviceFunctions.DrawIndexedPrimitiveUP, new DrawIndexedPrimitiveUPDelegate(this.DrawIndexedPrimitiveUP));
            this.AddHook(Direct3D9DeviceFunctions.DrawPrimitive, new DrawPrimitiveDelegate(this.DrawPrimitive));
            this.AddHook(Direct3D9DeviceFunctions.DrawPrimitiveUP, new DrawPrimitiveUPDelegate(this.DrawPrimitiveUP));
            this.AddHook(Direct3D9DeviceFunctions.DrawRectPatch, new DrawRectPatchDelegate(this.DrawRectPatch));
            this.AddHook(Direct3D9DeviceFunctions.DrawTriPatch, new DrawTriPatchDelegate(this.DrawTriPatch));
            this.AddHook(Direct3D9DeviceFunctions.EndScene, new EndSceneDelegate(this.EndScene));
            this.AddHook(Direct3D9DeviceFunctions.EndStateBlock, new EndStateBlockDelegate(this.EndStateBlock));
            this.AddHook(Direct3D9DeviceFunctions.EvictManagedResources, new EvictManagedResourcesDelegate(this.EvictManagedResources));
            this.AddHook(Direct3D9DeviceFunctions.GetAvailableTextureMem, new GetAvailableTextureMemDelegate(this.GetAvailableTextureMem));
            this.AddHook(Direct3D9DeviceFunctions.GetBackBuffer, new GetBackBufferDelegate(this.GetBackBuffer));
            this.AddHook(Direct3D9DeviceFunctions.GetClipPlane, new GetClipPlaneDelegate(this.GetClipPlane));
            this.AddHook(Direct3D9DeviceFunctions.GetClipStatus, new GetClipStatusDelegate(this.GetClipStatus));
            this.AddHook(Direct3D9DeviceFunctions.GetCreationParameters, new GetCreationParametersDelegate(this.GetCreationParameters));
            /*this.AddHook(Direct3D9DeviceFunctions.GetCurrentTexturePalette, new GetCurrentTexturePaletteDelegate(this.GetCurrentTexturePalette));
            this.AddHook(Direct3D9DeviceFunctions.GetDepthStencilSurface, new GetDepthStencilSurfaceDelegate(this.GetDepthStencilSurface));
            this.AddHook(Direct3D9DeviceFunctions.GetDeviceCaps, new GetDeviceCapsDelegate(this.GetDeviceCaps));
            this.AddHook(Direct3D9DeviceFunctions.GetDirect3D, new GetDirect3DDelegate(this.GetDirect3D));
            this.AddHook(Direct3D9DeviceFunctions.GetDisplayMode, new GetDisplayModeDelegate(this.GetDisplayMode));
            this.AddHook(Direct3D9DeviceFunctions.GetFVF, new GetFVFDelegate(this.GetFVF));
            this.AddHook(Direct3D9DeviceFunctions.GetFrontBufferData, new GetFrontBufferDataDelegate(this.GetFrontBufferData));
            this.AddHook(Direct3D9DeviceFunctions.GetGammaRamp, new GetGammaRampDelegate(this.GetGammaRamp));
            this.AddHook(Direct3D9DeviceFunctions.GetIndices, new GetIndicesDelegate(this.GetIndices));
            this.AddHook(Direct3D9DeviceFunctions.GetLight, new GetLightDelegate(this.GetLight));
            this.AddHook(Direct3D9DeviceFunctions.GetLightEnable, new GetLightEnableDelegate(this.GetLightEnable));
            this.AddHook(Direct3D9DeviceFunctions.GetMaterial, new GetMaterialDelegate(this.GetMaterial));
            this.AddHook(Direct3D9DeviceFunctions.GetNPatchMode, new GetNPatchModeDelegate(this.GetNPatchMode));
            this.AddHook(Direct3D9DeviceFunctions.GetNumberOfSwapChains, new GetNumberOfSwapChainsDelegate(this.GetNumberOfSwapChains));
            this.AddHook(Direct3D9DeviceFunctions.GetPaletteEntries, new GetPaletteEntriesDelegate(this.GetPaletteEntries));
            this.AddHook(Direct3D9DeviceFunctions.GetPixelShader, new GetPixelShaderDelegate(this.GetPixelShader));
            this.AddHook(Direct3D9DeviceFunctions.GetPixelShaderConstantB, new GetPixelShaderConstantBDelegate(this.GetPixelShaderConstantB));
            this.AddHook(Direct3D9DeviceFunctions.GetPixelShaderConstantF, new GetPixelShaderConstantFDelegate(this.GetPixelShaderConstantF));
            this.AddHook(Direct3D9DeviceFunctions.GetPixelShaderConstantI, new GetPixelShaderConstantIDelegate(this.GetPixelShaderConstantI));
            this.AddHook(Direct3D9DeviceFunctions.GetRasterStatus, new GetRasterStatusDelegate(this.GetRasterStatus));
            this.AddHook(Direct3D9DeviceFunctions.GetRenderState, new GetRenderStateDelegate(this.GetRenderState));
            this.AddHook(Direct3D9DeviceFunctions.GetRenderTarget, new GetRenderTargetDelegate(this.GetRenderTarget));
            this.AddHook(Direct3D9DeviceFunctions.GetRenderTargetData, new GetRenderTargetDataDelegate(this.GetRenderTargetData));
            this.AddHook(Direct3D9DeviceFunctions.GetSamplerState, new GetSamplerStateDelegate(this.GetSamplerState));
            this.AddHook(Direct3D9DeviceFunctions.GetScissorRect, new GetScissorRectDelegate(this.GetScissorRect));
            this.AddHook(Direct3D9DeviceFunctions.GetSoftwareVertexProcessing, new GetSoftwareVertexProcessingDelegate(this.GetSoftwareVertexProcessing));
            this.AddHook(Direct3D9DeviceFunctions.GetStreamSource, new GetStreamSourceDelegate(this.GetStreamSource));
            this.AddHook(Direct3D9DeviceFunctions.GetStreamSourceFreq, new GetStreamSourceFreqDelegate(this.GetStreamSourceFreq));
            this.AddHook(Direct3D9DeviceFunctions.GetSwapChain, new GetSwapChainDelegate(this.GetSwapChain));
            this.AddHook(Direct3D9DeviceFunctions.GetTexture, new GetTextureDelegate(this.GetTexture));
            this.AddHook(Direct3D9DeviceFunctions.GetTextureStageState, new GetTextureStageStateDelegate(this.GetTextureStageState));
            this.AddHook(Direct3D9DeviceFunctions.GetTransform, new GetTransformDelegate(this.GetTransform));
            this.AddHook(Direct3D9DeviceFunctions.GetVertexDeclaration, new GetVertexDeclarationDelegate(this.GetVertexDeclaration));
            this.AddHook(Direct3D9DeviceFunctions.GetVertexShader, new GetVertexShaderDelegate(this.GetVertexShader));
            this.AddHook(Direct3D9DeviceFunctions.GetVertexShaderConstantB, new GetVertexShaderConstantBDelegate(this.GetVertexShaderConstantB));
            this.AddHook(Direct3D9DeviceFunctions.GetVertexShaderConstantF, new GetVertexShaderConstantFDelegate(this.GetVertexShaderConstantF));
            this.AddHook(Direct3D9DeviceFunctions.GetVertexShaderConstantI, new GetVertexShaderConstantIDelegate(this.GetVertexShaderConstantI));
            this.AddHook(Direct3D9DeviceFunctions.GetViewport, new GetViewportDelegate(this.GetViewport));
            this.AddHook(Direct3D9DeviceFunctions.LightEnable, new LightEnableDelegate(this.LightEnable));
            this.AddHook(Direct3D9DeviceFunctions.MultiplyTransform, new MultiplyTransformDelegate(this.MultiplyTransform));
            this.AddHook(Direct3D9DeviceFunctions.Present, new PresentDelegate(this.Present));
            this.AddHook(Direct3D9DeviceFunctions.ProcessVertices, new ProcessVerticesDelegate(this.ProcessVertices));
            this.AddHook(Direct3D9DeviceFunctions.Reset, new ResetDelegate(this.Reset));
            this.AddHook(Direct3D9DeviceFunctions.SetClipPlane, new SetClipPlaneDelegate(this.SetClipPlane));
            this.AddHook(Direct3D9DeviceFunctions.SetCurrentTexturePalette, new SetCurrentTexturePaletteDelegate(this.SetCurrentTexturePalette));
            this.AddHook(Direct3D9DeviceFunctions.SetCursorPosition, new SetCursorPositionDelegate(this.SetCursorPosition));
            this.AddHook(Direct3D9DeviceFunctions.SetCursorProperties, new SetCursorPropertiesDelegate(this.SetCursorProperties));
            this.AddHook(Direct3D9DeviceFunctions.SetDepthStencilSurface, new SetDepthStencilSurfaceDelegate(this.SetDepthStencilSurface));
            this.AddHook(Direct3D9DeviceFunctions.SetDialogBoxMode, new SetDialogBoxModeDelegate(this.SetDialogBoxMode));
            this.AddHook(Direct3D9DeviceFunctions.SetFVF, new SetFVFDelegate(this.SetFVF));
            this.AddHook(Direct3D9DeviceFunctions.SetGammaRamp, new SetGammaRampDelegate(this.SetGammaRamp));
            this.AddHook(Direct3D9DeviceFunctions.SetIndices, new SetIndicesDelegate(this.SetIndices));
            this.AddHook(Direct3D9DeviceFunctions.SetLight, new SetLightDelegate(this.SetLight));
            this.AddHook(Direct3D9DeviceFunctions.SetMaterial, new SetMaterialDelegate(this.SetMaterial));
            this.AddHook(Direct3D9DeviceFunctions.SetNPatchMode, new SetNPatchModeDelegate(this.SetNPatchMode));
            this.AddHook(Direct3D9DeviceFunctions.SetPaletteEntries, new SetPaletteEntriesDelegate(this.SetPaletteEntries));
            this.AddHook(Direct3D9DeviceFunctions.SetPixelShader, new SetPixelShaderDelegate(this.SetPixelShader));
            this.AddHook(Direct3D9DeviceFunctions.SetPixelShaderConstantB, new SetPixelShaderConstantBDelegate(this.SetPixelShaderConstantB));
            this.AddHook(Direct3D9DeviceFunctions.SetPixelShaderConstantF, new SetPixelShaderConstantFDelegate(this.SetPixelShaderConstantF));
            this.AddHook(Direct3D9DeviceFunctions.SetPixelShaderConstantI, new SetPixelShaderConstantIDelegate(this.SetPixelShaderConstantI));
            this.AddHook(Direct3D9DeviceFunctions.SetRenderState, new SetRenderStateDelegate(this.SetRenderState));
            this.AddHook(Direct3D9DeviceFunctions.SetRenderTarget, new SetRenderTargetDelegate(this.SetRenderTarget));
            this.AddHook(Direct3D9DeviceFunctions.SetSamplerState, new SetSamplerStateDelegate(this.SetSamplerState));
            this.AddHook(Direct3D9DeviceFunctions.SetScissorRect, new SetScissorRectDelegate(this.SetScissorRect));
            this.AddHook(Direct3D9DeviceFunctions.SetSoftwareVertexProcessing, new SetSoftwareVertexProcessingDelegate(this.SetSoftwareVertexProcessing));
            this.AddHook(Direct3D9DeviceFunctions.SetStreamSource, new SetStreamSourceDelegate(this.SetStreamSource));
            this.AddHook(Direct3D9DeviceFunctions.SetStreamSourceFreq, new SetStreamSourceFreqDelegate(this.SetStreamSourceFreq));
            this.AddHook(Direct3D9DeviceFunctions.SetTexture, new SetTextureDelegate(this.SetTexture));
            this.AddHook(Direct3D9DeviceFunctions.SetTextureStageState, new SetTextureStageStateDelegate(this.SetTextureStageState));
            this.AddHook(Direct3D9DeviceFunctions.SetTransform, new SetTransformDelegate(this.SetTransform));
            this.AddHook(Direct3D9DeviceFunctions.SetVertexDeclaration, new SetVertexDeclarationDelegate(this.SetVertexDeclaration));
            this.AddHook(Direct3D9DeviceFunctions.SetVertexShader, new SetVertexShaderDelegate(this.SetVertexShader));
            this.AddHook(Direct3D9DeviceFunctions.SetVertexShaderConstantB, new SetVertexShaderConstantBDelegate(this.SetVertexShaderConstantB));
            this.AddHook(Direct3D9DeviceFunctions.SetVertexShaderConstantF, new SetVertexShaderConstantFDelegate(this.SetVertexShaderConstantF));
            this.AddHook(Direct3D9DeviceFunctions.SetVertexShaderConstantI, new SetVertexShaderConstantIDelegate(this.SetVertexShaderConstantI));
            this.AddHook(Direct3D9DeviceFunctions.SetViewport, new SetViewportDelegate(this.SetViewport));
            this.AddHook(Direct3D9DeviceFunctions.ShowCursor, new ShowCursorDelegate(this.ShowCursor));
            this.AddHook(Direct3D9DeviceFunctions.StretchRect, new StretchRectDelegate(this.StretchRect));
            this.AddHook(Direct3D9DeviceFunctions.TestCooperativeLevel, new TestCooperativeLevelDelegate(this.TestCooperativeLevel));
            this.AddHook(Direct3D9DeviceFunctions.UpdateSurface, new UpdateSurfaceDelegate(this.UpdateSurface));
            this.AddHook(Direct3D9DeviceFunctions.UpdateTexture, new UpdateTextureDelegate(this.UpdateTexture));
            this.AddHook(Direct3D9DeviceFunctions.ValidateDevice, new ValidateDeviceDelegate(this.ValidateDevice));*/
        }
    }
}
