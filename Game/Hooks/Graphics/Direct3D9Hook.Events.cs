using System;
using System.Runtime.InteropServices;
using SharpDX;
using SharpDX.Direct3D9;

namespace Game.Hooks.Graphics
{
    public partial class Direct3D9Hook
    {
        /* Creation callbacks */

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result ReportCreateVertexShader(ref IntPtr devicePointer, ref byte[] shaderBytecode, ref IntPtr vertexShader);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        private delegate Result ReportCreatePixelShader(ref IntPtr devicePointer, ref IntPtr shaderBytecode, ref IntPtr pixelShader);

        /* Allocation update callbacks */
public delegate bool ReportUnlockTexture(ref SurfaceDescription Desc, ref Surface Bmp, ref IntPtr Handle);
public delegate void ReportLockVertexBuffer( &Data, D3DVERTEXBUFFER_DESC &Desc);
public delegate void ReportLockIndexBuffer(BufferLockData &Data, D3DINDEXBUFFER_DESC &Desc);

public delegate void ReportDestroy(ref IntPtr Handle);

        public delegate void ResetEventDelegate(ref IntPtr devicePointer, ref PresentParameters[] presentParameters);
        public event ResetEventDelegate OnReset;

        public delegate void EndSceneEventDelegate(ref IntPtr devicePointer);
        public event EndSceneEventDelegate OnEndScene;

        public delegate void SetTextureEventDelegate(ref IntPtr devicePointer, ref int stage, ref IntPtr texturePointer);
        public event SetTextureEventDelegate OnSetTexture;

        public delegate void CreateTextureEventDelegate(ref IntPtr devicePointer, ref int width, ref int height, ref int levels, ref Usage usage, ref Format format, ref Pool pool, ref IntPtr texture, ref IntPtr sharedHandle);
        public event CreateTextureEventDelegate OnCreateTexture;

        public delegate void DrawIndexedPrimitveEventDelegate(ref IntPtr devicePointer, ref PrimitiveType primitiveType, ref int baseVertexIndex, ref int minVertexIndex, ref int numVertices, ref int startIndex, ref int primitiveCount);
        public event DrawIndexedPrimitveEventDelegate OnDrawIndexedPrimitve;

        public delegate void SetStreamSourceEventDelegate(ref IntPtr devicePointer, ref int streamNumber, ref IntPtr streamVertexBufferData, ref int offsetInBytes, ref int stride);
        public event SetStreamSourceEventDelegate OnSetStreamSource;
    }
}
