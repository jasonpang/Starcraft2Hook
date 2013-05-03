using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Game.Extensions.SharpDX;
using Game.Extensions.SharpDX.Direct3D;
using Logging.Extensions;
using SharpDX;
using SharpDX.Direct3D9;
using Rectangle = SharpDX.Rectangle;

namespace Game.Hooks.Graphics
{
    public partial class Direct3D9Hook
    {
        private Result BeginScene(IntPtr devicePointer)
        {
            try
            {
                this.Log.LogMethodSignatureTypesAndValues(devicePointer);
                this.GetOrCreateDevice(devicePointer);
                this.Device.BeginScene();
            }
            catch (SharpDXException ex)
            {
                this.Log.Warn(ex);
            }
            catch (Exception ex)
            {
                this.Log.Fatal(ex);
            }

            return Result.Ok;
        }

        private Result BeginStateBlock(IntPtr devicePointer)
        {
            try
            {
                this.Log.LogMethodSignatureTypesAndValues(devicePointer);
                this.GetOrCreateDevice(devicePointer);
                this.Device.BeginStateBlock();
            }
            catch (SharpDXException ex)
            {
                Log.Warn(ex);
            }
            catch (Exception ex)
            {
                this.Log.Fatal(ex);
            }

            return Result.Ok;
        }

        private Result Clear(IntPtr devicePointer, int count, IntPtr rects, ClearFlags flags, ColorBGRA color, float z, int stencil)
        {
            try
            {
                var structSize = Marshal.SizeOf(typeof(Rectangle));
                var structs = new SharpDX.Rectangle[count];
                for (int i = 0; i < count; i++)
                {
                    structs[i] = (SharpDX.Rectangle)Marshal.PtrToStructure(rects, typeof(SharpDX.Rectangle));
                }

                var rectangles = structs;
                this.Log.LogMethodSignatureTypesAndValues(devicePointer, count, rectangles.PrintTypesNamesValues(), flags,
                                                     color, z, stencil);
                this.GetOrCreateDevice(devicePointer);
                if (rectangles.Length == 0)
                    this.Device.Clear(flags, color, z, stencil);
                else
                    this.Device.Clear(flags, color, z, stencil, rectangles);
            }
            catch (SharpDXException ex)
            {
                Log.Warn(ex);
            }
            catch (Exception ex)
            {
                this.Log.Fatal(ex);
            }

            return Result.Ok;
        }

        private Result ColorFill (IntPtr devicePointer, IntPtr surface, IntPtr rect, ColorBGRA color)
        {
            try
            {
                this.Log.LogMethodSignatureTypesAndValues (devicePointer, surface, rect, color);

                Rectangle? rectangle = ( rect == IntPtr.Zero )
                                    ? null // A null rectangle means the entire backbuffer is to be color filled
                                    : (Rectangle?) Marshal.PtrToStructure (rect, typeof (Rectangle));

                this.GetOrCreateDevice (devicePointer);
                this.Device.ColorFill (Surface.FromPointer <Surface> (surface), rectangle, color);
            }
            catch (SharpDXException ex)
            {
                Log.Warn (ex);
            }
            catch (Exception ex)
            {
                this.Log.Fatal (ex);
                this.Log.LogExceptionLineNumber (ex);
            }

            return Result.Ok;
        }

        private Result CreateAdditionalSwapChain(IntPtr devicePointer, PresentParameters presentParameters, out SwapChain swapChain)
        {
            try
            {
                this.Log.LogMethodSignatureTypesAndValues(devicePointer, presentParameters.PrintTypesNamesValues());
                this.GetOrCreateDevice(devicePointer);
                swapChain = new SwapChain(this.Device, presentParameters);
                this.SwapChains.Add(swapChain);
                return Result.Ok;
            }
            catch (SharpDXException ex)
            {
                Log.Warn(ex);
                swapChain = null;
                return ex.ResultCode;
            }
            catch (Exception ex)
            {
                this.Log.Fatal(ex);
                swapChain = null;
                return Result.UnexpectedFailure;
            }
        }

        private Result CreateCubeTexture(IntPtr devicePointer, int edgeLength, int levels, Usage usage, Format format, Pool pool, out IntPtr cubeTexture, IntPtr sharedHandle)
        {
            try
            {
                this.Log.LogMethodSignatureTypesAndValues(devicePointer, edgeLength, levels, usage, format, pool, "out", sharedHandle);
                this.GetOrCreateDevice(devicePointer);
                this.CubeTextures.Add(sharedHandle == IntPtr.Zero
                    ? new CubeTexture(this.Device, edgeLength, levels, usage, format, pool)
                    : new CubeTexture(this.Device, edgeLength, levels, usage, format, pool, ref sharedHandle));
                cubeTexture = this.CubeTextures.Last().NativePointer;
                return Result.Ok;
            }
            catch (SharpDXException ex)
            {
                Log.Warn(ex);
                cubeTexture = IntPtr.Zero;
                return ex.ResultCode;
            }
            catch (Exception ex)
            {
                this.Log.Fatal(ex);
                cubeTexture = IntPtr.Zero;
                return Result.UnexpectedFailure;
            }
        }

        private Result CreateDepthStencilSurface(IntPtr devicePointer, int width, int height, Format format, MultisampleType multiSampleType, int multiSampleQuality, bool discard, out IntPtr surface, IntPtr sharedHandle)
        {
            try
            {
                this.Log.LogMethodSignatureTypesAndValues(devicePointer, width, height, format, multiSampleType, multiSampleQuality, discard, "out", sharedHandle);
                this.GetOrCreateDevice(devicePointer);
                this.Surfaces.Add(sharedHandle == IntPtr.Zero
                    ? Surface.CreateDepthStencil(this.Device, width, height, format, multiSampleType, multiSampleQuality, discard)
                    : Surface.CreateDepthStencil(this.Device, width, height, format, multiSampleType, multiSampleQuality, discard, ref sharedHandle));
                surface = this.Surfaces.Last().NativePointer;
                return Result.Ok;
            }
            catch (SharpDXException ex)
            {
                Log.Warn(ex);
                surface = IntPtr.Zero;
                return ex.ResultCode;
            }
            catch (Exception ex)
            {
                this.Log.Fatal(ex);
                surface = IntPtr.Zero;
                return Result.UnexpectedFailure;
            }
        }

        /// <summary>
        /// Creates the index buffer.
        /// </summary>
        /// <param name="devicePointer">The device pointer.</param>
        /// <param name="length">The length.</param>
        /// <param name="usage">The usage.</param>
        /// <param name="format">The format.</param>
        /// <param name="pool">The pool.</param>
        /// <param name="indexBuffer">The index buffer.</param>
        /// <param name="sharedHandle">The shared handle.</param>
        /// <returns></returns>
        private Result CreateIndexBuffer(IntPtr devicePointer, int length, Usage usage, Format format, Pool pool, out IntPtr indexBuffer, IntPtr sharedHandle)
        {
            try
            {
                this.Log.LogMethodSignatureTypesAndValues(devicePointer, length, usage, format, pool, "out", sharedHandle);
                this.GetOrCreateDevice(devicePointer);
                this.IndexBuffers.Add(sharedHandle == IntPtr.Zero
                                     ? new IndexBuffer(this.Device, length, usage, pool, format == Format.Index16)
                                     : new IndexBuffer(this.Device, length, usage, pool, format == Format.Index16, ref sharedHandle));
                indexBuffer = this.IndexBuffers.Last().NativePointer;
                return Result.Ok;
            }
            catch (SharpDXException ex)
            {
                Log.Warn(ex);
                indexBuffer = IntPtr.Zero;
                return ex.ResultCode;
            }
            catch (Exception ex)
            {
                this.Log.Fatal(ex);
                indexBuffer = IntPtr.Zero;
                return Result.UnexpectedFailure;
            }
        }

        private Result CreateOffscreenPlainSurface(IntPtr devicePointer, int width, int height, Format format, Pool pool, out IntPtr surface, IntPtr sharedHandle)
        {
            try
            {
                this.Log.LogMethodSignatureTypesAndValues(devicePointer, width, height, format, pool, "out", sharedHandle);
                this.GetOrCreateDevice(devicePointer);
                this.Surfaces.Add(sharedHandle == IntPtr.Zero
                                     ? Surface.CreateOffscreenPlain(this.Device, width, height, format, pool)
                                     : Surface.CreateOffscreenPlain(this.Device, width, height, format, pool, ref sharedHandle));
                surface = this.Surfaces.Last().NativePointer;
                return Result.Ok;
            }
            catch (SharpDXException ex)
            {
                Log.Warn(ex);
                surface = IntPtr.Zero;
                return ex.ResultCode;
            }
            catch (Exception ex)
            {
                this.Log.Fatal(ex);
                surface = IntPtr.Zero;
                return Result.UnexpectedFailure;
            }
        }

        /* Todo later, length is not supposed to be found, use d3d9Wrapper */
        private Result CreatePixelShader(IntPtr devicePointer, IntPtr shaderBytecode, out IntPtr pixelShader)
        {
            throw new NotImplementedException("Dissassembler needed. Do last.");
#pragma warning disable 162
            try
            {
                this.Log.LogMethodSignatureTypesAndValues(devicePointer, shaderBytecode, "out");
                this.GetOrCreateDevice(devicePointer);
                this.PixelShaders.Add(new PixelShader(this.Device, new ShaderBytecode(shaderBytecode, 1)));
                pixelShader = this.PixelShaders.Last().NativePointer;
            }
            catch (Exception ex)
            {
                Log.Warn(ex);
                pixelShader = IntPtr.Zero;
            }

            return Result.Ok;
#pragma warning restore 162
        }

        private unsafe Result CreateQuery (IntPtr devicePointer, QueryType queryType, IntPtr** queryInterface)
        {
            throw new NotSupportedException("Can't write interop, and this function doesn't seem necessary.");
            try
            {
                this.Log.LogMethodSignatureTypesAndValues (devicePointer, queryType, "ref");
                this.GetOrCreateDevice(devicePointer);
                if (**queryInterface == IntPtr.Zero)
                {
                    return Result.Ok;
                }
                else
                {
                    this.Queries.Add(new Query(**queryInterface));
                    //queryInterface = this.Queries.Last().NativePointer;
                    return Result.Ok;
                }
            }
            catch (SharpDXException ex)
            {
                Log.Warn (ex);
                return ex.ResultCode;
            }
            catch (Exception ex)
            {
                this.Log.Fatal (ex);
                return Result.UnexpectedFailure;
            }
        }

        private Result CreateRenderTarget(IntPtr devicePointer, int width, int height, Format format, MultisampleType multiSampleType, int multiSampleQuality, bool lockable, out IntPtr surface, IntPtr sharedHandle)
        {
            try
            {
                this.Log.LogMethodSignatureTypesAndValues(devicePointer, width, height, format, multiSampleType, multiSampleQuality, lockable, "out", sharedHandle);
                this.GetOrCreateDevice(devicePointer);
                this.Surfaces.Add(sharedHandle == IntPtr.Zero
                                     ? Surface.CreateRenderTarget(this.Device, width, height, format, multiSampleType, multiSampleQuality, lockable)
                                     : Surface.CreateRenderTarget(this.Device, width, height, format, multiSampleType, multiSampleQuality, lockable, ref sharedHandle));
                surface = this.Surfaces.Last().NativePointer;
                return Result.Ok;
            }
            catch (SharpDXException ex)
            {
                Log.Warn(ex);
                surface = IntPtr.Zero;
                return ex.ResultCode;
            }
            catch (Exception ex)
            {
                this.Log.Fatal(ex);
                surface = IntPtr.Zero;
                return Result.UnexpectedFailure;
            }
        }

        private Result CreateStateBlock(IntPtr devicePointer, StateBlockType type, out IntPtr stateBlock)
        {
            try
            {
                this.Log.LogMethodSignatureTypesAndValues(devicePointer, type, "out");
                this.GetOrCreateDevice(devicePointer);
                this.StateBlocks.Add(new StateBlock(this.Device, type));
                stateBlock = this.StateBlocks.Last().NativePointer;
                return Result.Ok;
            }
            catch (SharpDXException ex)
            {
                Log.Warn(ex);
                stateBlock = IntPtr.Zero;
                return ex.ResultCode;
            }
            catch (Exception ex)
            {
                this.Log.Fatal(ex);
                stateBlock = IntPtr.Zero;
                return Result.UnexpectedFailure;
            }
        }

        private Result CreateTexture(IntPtr devicePointer, int width, int height, int levels, Usage usage, Format format, Pool pool, out IntPtr texture, IntPtr sharedHandle)
        {
            try
            {
                this.Log.LogMethodSignatureTypesAndValues(devicePointer, width, height, levels, usage, format, pool, "out", sharedHandle);
                this.GetOrCreateDevice(devicePointer);
                this.Textures.Add(sharedHandle == IntPtr.Zero
                                     ? new Texture(this.Device, width, height, levels, usage, format, pool)
                                     : new Texture(this.Device, width, height, levels, usage, format, pool, ref sharedHandle));
                texture = this.Textures.Last().NativePointer;
                return Result.Ok;
            }
            catch (SharpDXException ex)
            {
                Log.Warn(ex);
                texture = IntPtr.Zero;
                return ex.ResultCode;
            }
            catch (Exception ex)
            {
                this.Log.Fatal(ex);
                texture = IntPtr.Zero;
                return Result.UnexpectedFailure;
            }
        }

        private Result CreateVertexBuffer(IntPtr devicePointer, int length, Usage usage, VertexFormat vertexFormat, Pool pool, out IntPtr vertexBuffer, IntPtr sharedHandle)
        {
            try
            {
                this.Log.LogMethodSignatureTypesAndValues(devicePointer, length, usage, vertexFormat, pool, "out", sharedHandle);
                this.GetOrCreateDevice(devicePointer);
                this.VertexBuffers.Add(sharedHandle == IntPtr.Zero
                                     ? new VertexBuffer(this.Device, length, usage, vertexFormat, pool)
                                     : new VertexBuffer(this.Device, length, usage, vertexFormat, pool, ref sharedHandle));
                vertexBuffer = this.VertexBuffers.Last().NativePointer;
                return Result.Ok;
            }
            catch (SharpDXException ex)
            {
                Log.Warn(ex);
                vertexBuffer = IntPtr.Zero;
                return ex.ResultCode;
            }
            catch (Exception ex)
            {
                this.Log.Fatal(ex);
                vertexBuffer = IntPtr.Zero;
                return Result.UnexpectedFailure;
            }
        }

        private Result CreateVertexDeclaration(IntPtr devicePointer, IntPtr elementsPointer, out IntPtr vertexDeclaration)
        {
            try
            {
                this.Log.LogMethodSignatureTypesAndValues(devicePointer, elementsPointer, "out");
                this.GetOrCreateDevice(devicePointer);

                // We don't know the size of the array at first; it's terminated by a D3DDECL_END()
                var elements = new List<VertexElement>();

                do
                {
                    elements.Add((VertexElement)Marshal.PtrToStructure(elementsPointer, typeof(VertexElement)));
                    // Offset the pointer by the struct size to read the next struct
                    elementsPointer = IntPtr.Add(elementsPointer, Marshal.SizeOf(typeof(VertexElement)));
                }
                while (!elements.Last().IsEndOfDeclaration());

                this.VertexDeclarations.Add(new VertexDeclaration(this.Device, elements.ToArray()));
                vertexDeclaration = this.VertexDeclarations.Last().NativePointer;
                return Result.Ok;
            }
            catch (SharpDXException ex)
            {
                Log.Warn(ex);
                vertexDeclaration = IntPtr.Zero;
                return ex.ResultCode;
            }
            catch (Exception ex)
            {
                this.Log.Fatal(ex);
                vertexDeclaration = IntPtr.Zero;
                return Result.UnexpectedFailure;
            }
        }

        /* todo later, length is not supposed to be passed */
        private Result CreateVertexShader(IntPtr devicePointer, byte[] shaderBytecode, out IntPtr vertexShader)
        {
            throw new NotImplementedException("Dissassembler needed. Do last.");
#pragma warning disable 162
            try
            {
                this.Log.LogMethodSignatureTypesAndValues(devicePointer, shaderBytecode, "out");
                this.GetOrCreateDevice(devicePointer);
                this.VertexShaders.Add(new VertexShader(this.Device, new ShaderBytecode(shaderBytecode)));
                vertexShader = this.VertexShaders.Last().NativePointer;
            }
            catch (Exception ex)
            {
                Log.Warn(ex);
                vertexShader = IntPtr.Zero;
            }

            return Result.Ok;
#pragma warning restore 162
        }

        private Result CreateVolumeTexture(IntPtr devicePointer, int width, int height, int depth, int levels,
                                           Usage usage, Format format, Pool pool, out IntPtr volumeTexture,
                                           IntPtr sharedHandle)
        {
            try
            {
                this.Log.LogMethodSignatureTypesAndValues(devicePointer, width, height, depth, levels, usage, format, pool,
                                                     "out", sharedHandle);
                this.GetOrCreateDevice(devicePointer);
                this.VolumeTextures.Add(sharedHandle == IntPtr.Zero
                                       ? new VolumeTexture(this.Device, width, height, depth, levels, usage, format, pool)
                                       : new VolumeTexture(this.Device, width, height, depth, levels, usage, format, pool,
                                                           ref sharedHandle));
                volumeTexture = this.VolumeTextures.Last().NativePointer;
                return Result.Ok;
            }
            catch (SharpDXException ex)
            {
                Log.Warn(ex);
                volumeTexture = IntPtr.Zero;
                return ex.ResultCode;
            }
            catch (Exception ex)
            {
                this.Log.Fatal(ex);
                volumeTexture = IntPtr.Zero;
                return Result.UnexpectedFailure;
            }
        }

        private Result DeletePatch(IntPtr devicePointer, int handle)
        {
            try
            {
                this.Log.LogMethodSignatureTypesAndValues(devicePointer, handle);
                this.GetOrCreateDevice(devicePointer);
                this.Device.DeletePatch(handle);
                return Result.Ok;
            }
            catch (SharpDXException ex)
            {
                Log.Warn(ex);
                return ex.ResultCode;
            }
            catch (Exception ex)
            {
                this.Log.Fatal(ex);
                return Result.UnexpectedFailure;
            }
        }

        private Texture ChamsTextureRed;
        private Texture ChamsTextureGreen;

        private Result DrawIndexedPrimitive(IntPtr devicePointer, PrimitiveType primitiveType, int baseVertexIndex, int minVertexIndex, int numVertices, int startIndex, int primitiveCount)
        {
            try
            {
                this.Log.LogMethodSignatureTypesAndValues(devicePointer, primitiveType, baseVertexIndex, minVertexIndex, numVertices, startIndex, primitiveCount);
                this.GetOrCreateDevice(devicePointer);

                if (this.OnDrawIndexedPrimitve != null)
                    this.OnDrawIndexedPrimitve(ref devicePointer, ref primitiveType, ref baseVertexIndex, ref minVertexIndex, ref numVertices, ref startIndex, ref primitiveCount);

                this.Device.DrawIndexedPrimitive(primitiveType, baseVertexIndex, minVertexIndex, numVertices, startIndex, primitiveCount);

                return Result.Ok;
            }
            catch (SharpDXException ex)
            {
                Log.Warn(ex);
                return ex.ResultCode;
            }
            catch (Exception ex)
            {
                this.Log.Fatal(ex);
                return Result.UnexpectedFailure;
            }
        }

        /* todo later */
        private Result DrawIndexedPrimitiveUP(IntPtr devicePointer, PrimitiveType primitiveType, int minVertexIndex, int numVertices, int primitiveCount, IntPtr IndexData, Format indexDataFormat, IntPtr vertexStreamZeroData, int vertexStreamZeroStride)
        {
            throw new NotImplementedException();
#pragma warning disable 162
            try
            {
                this.Log.LogMethodSignatureTypesAndValues(devicePointer, primitiveType, minVertexIndex, numVertices, primitiveCount, IndexData, indexDataFormat, vertexStreamZeroData, vertexStreamZeroStride);
                this.GetOrCreateDevice(devicePointer);
                //Device.DrawIndexedUserPrimitives();
                return Result.Ok;
            }
            catch (SharpDXException ex)
            {
                Log.Warn(ex);
                return ex.ResultCode;
            }
            catch (Exception ex)
            {
                this.Log.Fatal(ex);
                return Result.UnexpectedFailure;
            }
#pragma warning restore 162
        }

        private Result DrawPrimitive(IntPtr devicePointer, PrimitiveType primitiveType, int startVertex, int primitiveCount)
        {
            try
            {
                this.Log.LogMethodSignatureTypesAndValues(devicePointer, primitiveType, startVertex, primitiveCount);
                this.GetOrCreateDevice(devicePointer);
                this.Device.DrawPrimitives(primitiveType, startVertex, primitiveCount);
                return Result.Ok;
            }
            catch (SharpDXException ex)
            {
                Log.Warn(ex);
                return ex.ResultCode;
            }
            catch (Exception ex)
            {
                this.Log.Fatal(ex);
                return Result.UnexpectedFailure;
            }
        }

        /* todo later */
        private Result DrawPrimitiveUP(IntPtr devicePointer, PrimitiveType primitiveType, int primitiveCount, IntPtr vertexStreamZeroData, int vertexStreamZeroStride)
        {
            throw new NotImplementedException();
        }

        private Result DrawRectPatch(IntPtr devicePointer, int handle, float[] numSegments, IntPtr rectPatchInfo)
        {
            try
            {
                this.Log.LogMethodSignatureTypesAndValues(devicePointer, handle, numSegments, rectPatchInfo);
                this.GetOrCreateDevice(devicePointer);
                if (rectPatchInfo == IntPtr.Zero)
                    this.Device.DrawRectanglePatch(handle, numSegments);
                else this.Device.DrawRectanglePatch(handle, numSegments, (RectanglePatchInfo) Marshal.PtrToStructure(rectPatchInfo, typeof(RectanglePatchInfo)));
                return Result.Ok;
            }
            catch (SharpDXException ex)
            {
                Log.Warn(ex);
                return ex.ResultCode;
            }
            catch (Exception ex)
            {
                this.Log.Fatal(ex);
                return Result.UnexpectedFailure;
            }
        }

        private Result DrawTriPatch(IntPtr devicePointer, int handle, float[] numSegments, IntPtr triPatchInfo)
        {
            try
            {
                this.Log.LogMethodSignatureTypesAndValues(devicePointer, handle, numSegments, triPatchInfo);
                this.GetOrCreateDevice(devicePointer);
                if (triPatchInfo == IntPtr.Zero)
                    this.Device.DrawTrianglePatch(handle, numSegments);
                else this.Device.DrawTrianglePatch(handle, numSegments, (TrianglePatchInfo)Marshal.PtrToStructure(triPatchInfo, typeof(TrianglePatchInfo)));
                return Result.Ok;
            }
            catch (SharpDXException ex)
            {
                Log.Warn(ex);
                return ex.ResultCode;
            }
            catch (Exception ex)
            {
                this.Log.Fatal(ex);
                return Result.UnexpectedFailure;
            }
        }

        private ColorBGRA black = new ColorBGRA(0, 0, 0, 255);
        private Result EndScene(IntPtr devicePointer)
        {
            try
            {
                this.Log.LogMethodSignatureTypesAndValues(devicePointer);
                this.GetOrCreateDevice(devicePointer);
                if (this.OnEndScene != null)
                    this.OnEndScene(ref devicePointer);
                this.Device.EndScene();
                return Result.Ok;
            }
            catch (SharpDXException ex)
            {
                Log.Warn(ex);
                return ex.ResultCode;
            }
            catch (Exception ex)
            {
                this.Log.Fatal(ex);
                return Result.UnexpectedFailure;
            }
        }

        private Result EndStateBlock(IntPtr devicePointer, [In, Out] StateBlock stateBlock)
        {
            try
            {
                this.Log.LogMethodSignatureTypesAndValues(devicePointer, stateBlock);
                this.GetOrCreateDevice(devicePointer);
                stateBlock = this.Device.EndStateBlock();
                return Result.Ok;
            }
            catch (SharpDXException ex)
            {
                Log.Warn(ex);
                return ex.ResultCode;
            }
            catch (Exception ex)
            {
                this.Log.Fatal(ex);
                return Result.UnexpectedFailure;
            }
        }

        private Result EvictManagedResources(IntPtr devicePointer)
        {
            try
            {
                this.Log.LogMethodSignatureTypesAndValues(devicePointer);
                this.GetOrCreateDevice(devicePointer);
                this.Device.EvictManagedResources();
                return Result.Ok;
            }
            catch (SharpDXException ex)
            {
                Log.Warn(ex);
                return ex.ResultCode;
            }
            catch (Exception ex)
            {
                this.Log.Fatal(ex);
                return Result.UnexpectedFailure;
            }
        }

        private long GetAvailableTextureMem(IntPtr devicePointer)
        {
            try
            {
                this.Log.LogMethodSignatureTypesAndValues(devicePointer);
                this.GetOrCreateDevice(devicePointer);
                return this.Device.AvailableTextureMemory;
            }
            catch (SharpDXException ex)
            {
                Log.Warn(ex);
                return 0;
            }
            catch (Exception ex)
            {
                this.Log.Fatal(ex);
                return 0;
            }
        }

        private Result GetBackBuffer(IntPtr devicePointer, int swapChainIndex, int backBufferIndex, BackBufferType backBufferType, out Surface backBuffer)
        {
            try
            {
                this.Log.LogMethodSignatureTypesAndValues(devicePointer, swapChainIndex, backBufferIndex, backBufferType, "out");
                this.GetOrCreateDevice(devicePointer);
                backBuffer = this.Device.GetBackBuffer(swapChainIndex, backBufferIndex);
                return Result.Ok;
            }
            catch (SharpDXException ex)
            {
                Log.Warn(ex);
                backBuffer = null;
                return ex.ResultCode;
            }
            catch (Exception ex)
            {
                this.Log.Fatal(ex);
                backBuffer = null;
                return Result.UnexpectedFailure;
            }
        }

        private Result GetClipPlane(IntPtr devicePointer, int index, out float[] plane)
        {
            try
            {
                this.Log.LogMethodSignatureTypesAndValues(devicePointer, index, "out");
                this.GetOrCreateDevice(devicePointer);
                plane = new[] { this.Device.GetClipPlane(index) };
                return Result.Ok;
            }
            catch (SharpDXException ex)
            {
                Log.Warn(ex);
                plane = null;
                return ex.ResultCode;
            }
            catch (Exception ex)
            {
                this.Log.Fatal(ex);
                plane = null;
                return Result.UnexpectedFailure;
            }
        }

        private Result GetClipStatus(IntPtr devicePointer, out ClipStatus clipStatus)
        {
            try
            {
                this.Log.LogMethodSignatureTypesAndValues(devicePointer, "out");
                this.GetOrCreateDevice(devicePointer);
                clipStatus = this.Device.ClipStatus;
                return Result.Ok;
            }
            catch (SharpDXException ex)
            {
                Log.Warn(ex);
                clipStatus = default(ClipStatus);
                return ex.ResultCode;
            }
            catch (Exception ex)
            {
                this.Log.Fatal(ex);
                clipStatus = default(ClipStatus);
                return Result.UnexpectedFailure;
            }
        }

        private Result GetCreationParameters(IntPtr devicePointer, out CreationParameters parameters)
        {
            try
            {
                this.Log.LogMethodSignatureTypesAndValues(devicePointer, "out");
                this.GetOrCreateDevice(devicePointer);
                parameters = this.Device.CreationParameters;
                return Result.Ok;
            }
            catch (SharpDXException ex)
            {
                Log.Warn(ex);
                parameters = default(CreationParameters);
                return ex.ResultCode;
            }
            catch (Exception ex)
            {
                this.Log.Fatal(ex);
                parameters = default(CreationParameters);
                return Result.UnexpectedFailure;
            }
        }

        private Result GetCurrentTexturePalette(IntPtr devicePointer, IntPtr paletteNumber)
        {
            throw new NotImplementedException();
        }

        private Result GetDepthStencilSurface(IntPtr devicePointer, out IntPtr stencilSurface)
        {
            throw new NotImplementedException();
        }

        private Result GetDeviceCaps(IntPtr devicePointer, Capabilities caps)
        {
            throw new NotImplementedException();
        }

        private Result GetDirect3D(IntPtr devicePointer, out IntPtr direct3D)
        {
            throw new NotImplementedException();
        }

        private Result GetDisplayMode(IntPtr devicePointer, uint swapChainIndex, DisplayMode mode)
        {
            throw new NotImplementedException();
        }

        private Result GetFrontBufferData(IntPtr devicePointer, int swapChainIndex, IntPtr destSurface)
        {
            throw new NotImplementedException();
        }

        private Result GetFVF(IntPtr devicePointer, out int fvf)
        {
            throw new NotImplementedException();
        }

        private Result GetGammaRamp(IntPtr devicePointer, int swapChainIndex, out IntPtr gammaRamp)
        {
            throw new NotImplementedException();
        }

        private Result GetIndices(IntPtr devicePointer, out IntPtr indexBuffer)
        {
            throw new NotImplementedException();
        }

        private Result GetLight(IntPtr devicePointer, int index, out IntPtr light)
        {
            throw new NotImplementedException();
        }

        private Result GetLightEnable(IntPtr devicePointer, int index, out int enabled)
        {
            throw new NotImplementedException();
        }

        private Result GetMaterial(IntPtr devicePointer, int index, out IntPtr material)
        {
            throw new NotImplementedException();
        }

        private double GetNPatchMode(IntPtr devicePointer)
        {
            throw new NotImplementedException();
        }

        private Result GetNumberOfSwapChains(IntPtr devicePointer)
        {
            throw new NotImplementedException();
        }

        private Result GetPaletteEntries(IntPtr devicePointer, int paletteNumber, IntPtr entries)
        {
            throw new NotImplementedException();
        }

        private Result GetPixelShader(IntPtr devicePointer, out IntPtr pixelShader)
        {
            throw new NotImplementedException();
        }

        private Result GetPixelShaderConstantB(IntPtr devicePointer, int startRegister, int[] data, int vector4BoolsCount)
        {
            throw new NotImplementedException();
        }

        private Result GetPixelShaderConstantF(IntPtr devicePointer, int startRegister, double[] data, int vector4FloatsCount)
        {
            throw new NotImplementedException();
        }

        private Result GetPixelShaderConstantI(IntPtr devicePointer, int startRegister, double[] data, int vector4IntsCount)
        {
            throw new NotImplementedException();
        }

        private Result GetRasterStatus(IntPtr devicePointer, int swapChainIndex, IntPtr rasterStatus)
        {
            throw new NotImplementedException();
        }

        private Result GetRenderState(IntPtr devicePointer, RenderState state, out int value)
        {
            throw new NotImplementedException();
        }

        private Result GetRenderTarget(IntPtr devicePointer, int renderTargetIndex, out IntPtr renderTarget)
        {
            throw new NotImplementedException();
        }

        private Result GetRenderTargetData(IntPtr devicePointer, out IntPtr destSurface)
        {
            throw new NotImplementedException();
        }

        private Result GetSamplerState(IntPtr devicePointer, int sampler, SamplerState type, IntPtr value)
        {
            throw new NotImplementedException();
        }

        private Result GetScissorRect(IntPtr devicePointer, out IntPtr rect)
        {
            throw new NotImplementedException();
        }

        private int GetSoftwareVertexProcessing(IntPtr devicePointer, int software)
        {
            throw new NotImplementedException();
        }

        private Result GetStreamSource(IntPtr devicePointer, int streamNumber, out IntPtr streamVertexBufferData, out IntPtr OffsetInBytes, out IntPtr stride)
        {
            throw new NotImplementedException();
        }

        private Result GetStreamSourceFreq(IntPtr devicePointer, int streamNumber, out int freqDivider)
        {
            throw new NotImplementedException();
        }

        private Result GetSwapChain(IntPtr devicePointer, uint swapChainIndex, out IntPtr swapChain)
        {
            throw new NotImplementedException();
        }

        private BaseTexture GetTexture(IntPtr devicePointer, int stage)
        {
            throw new NotImplementedException();
        }

        private Result GetTextureStageState(IntPtr devicePointer, int stage, TextureStage type, IntPtr value)
        {
            throw new NotImplementedException();
        }

        private Result GetTransform(IntPtr devicePointer, TransformState transformState, out IntPtr matrix)
        {
            throw new NotImplementedException();
        }

        private Result GetVertexDeclaration(IntPtr devicePointer, out IntPtr vertexDeclaration)
        {
            throw new NotImplementedException();
        }

        private Result GetVertexShader(IntPtr devicePointer, out IntPtr vertexShader)
        {
            throw new NotImplementedException();
        }

        private Result GetVertexShaderConstantB(IntPtr devicePointer, int startRegister, int[] bools, int vector4BoolCounts)
        {
            throw new NotImplementedException();
        }

        private Result GetVertexShaderConstantF(IntPtr devicePointer, int startRegister, double[] floats, int vector4FloatCount)
        {
            throw new NotImplementedException();
        }

        private Result GetVertexShaderConstantI(IntPtr devicePointer, int startRegister, double[] ints, int vector4IntCount)
        {
            throw new NotImplementedException();
        }

        private Result GetViewport(IntPtr devicePointer, int index, out IntPtr viewPort)
        {
            throw new NotImplementedException();
        }

        private Result LightEnable(IntPtr devicePointer, int index, int enabled)
        {
            throw new NotImplementedException();
        }

        private Result MultiplyTransform(IntPtr devicePointer, TransformState transformState, IntPtr matrix)
        {
            throw new NotImplementedException();
        }

        private Result Present(IntPtr devicePointer, Rectangle sourceRect, Rectangle destRect, IntPtr destWindowOverride, ref Region dirtyRegion)
        {
            throw new NotImplementedException();
        }

        private Result ProcessVertices(IntPtr devicePointer, int srcStartIndex, int destIndex, int vertexCount, IntPtr destVertexBuffer, IntPtr vertexDeclaration, int flags)
        {
            throw new NotImplementedException();
        }

        private Result Reset(IntPtr devicePointer, params PresentParameters[] presentParameters)
        {
            try
            {
                this.Log.LogMethodSignatureTypesAndValues(devicePointer, presentParameters.PrintTypesNamesValues());
                this.GetOrCreateDevice(devicePointer);
                if (this.OnReset != null) this.OnReset(ref devicePointer, ref presentParameters);
                this.Device.Reset(presentParameters);
                return Result.Ok;
            }
            catch (Exception ex)
            {
                this.Log.Fatal(ex);
                return Result.Ok;
            }
        }

        private Result SetClipPlane(IntPtr devicePointer, int index, double[] plane)
        {
            throw new NotImplementedException();
        }

        private Result SetCurrentTexturePalette(IntPtr devicePointer, int paletteNumber)
        {
            throw new NotImplementedException();
        }

        private void SetCursorPosition(IntPtr devicePointer, int x, int y, bool cursorBitmap)
        {
            throw new NotImplementedException();
        }

        private Result SetCursorProperties(IntPtr devicePointer, uint xHotSpot, uint yHotSpot, IntPtr cursorBitmap)
        {
            throw new NotImplementedException();
        }

        private Result SetDepthStencilSurface(IntPtr devicePointer, out IntPtr stencilSurface)
        {
            throw new NotImplementedException();
        }

        private Result SetDialogBoxMode(IntPtr devicePointer, int enableDialogs)
        {
            throw new NotImplementedException();
        }

        private Result SetFVF(IntPtr devicePointer, int fvf)
        {
            throw new NotImplementedException();
        }

        private Result SetGammaRamp(IntPtr devicePointer, int swapChainIndex, int flags, IntPtr gammaRamp)
        {
            throw new NotImplementedException();
        }

        private Result SetIndices(IntPtr devicePointer, IntPtr indexBuffer)
        {
            throw new NotImplementedException();
        }

        private Result SetLight(IntPtr devicePointer, int index, IntPtr light)
        {
            throw new NotImplementedException();
        }

        private Result SetMaterial(IntPtr devicePointer, int index, IntPtr material)
        {
            throw new NotImplementedException();
        }

        private int SetNPatchMode(IntPtr devicePointer, double nSegments)
        {
            throw new NotImplementedException();
        }

        private Result SetPaletteEntries(IntPtr devicePointer, int paletteNumber, IntPtr entries)
        {
            throw new NotImplementedException();
        }

        private Result SetPixelShader(IntPtr devicePointer, IntPtr pixelShader)
        {
            throw new NotImplementedException();
        }

        private Result SetPixelShaderConstantB(IntPtr devicePointer, int startRegister, int[] data, int vector4BoolsCount)
        {
            throw new NotImplementedException();
        }

        private Result SetPixelShaderConstantF(IntPtr devicePointer, int startRegister, double[] data, int vector4FloatsCount)
        {
            throw new NotImplementedException();
        }

        private Result SetPixelShaderConstantI(IntPtr devicePointer, int startRegister, double[] data, int vector4IntsCount)
        {
            throw new NotImplementedException();
        }

        private Result SetRenderState(IntPtr devicePointer, RenderState state, int value)
        {
            throw new NotImplementedException();
        }

        private Result SetRenderTarget(IntPtr devicePointer, int renderTargetIndex, IntPtr renderTarget)
        {
            throw new NotImplementedException();
        }

        private Result SetSamplerState(IntPtr devicePointer, int sampler, SamplerState type, int value)
        {
            throw new NotImplementedException();
        }

        private Result SetScissorRect(IntPtr devicePointer, IntPtr rect)
        {
            throw new NotImplementedException();
        }

        private Result SetSoftwareVertexProcessing(IntPtr devicePointer, int software)
        {
            throw new NotImplementedException();
        }

        private Result SetStreamSource(IntPtr devicePointer, int streamNumber, IntPtr streamVertexBufferData, int offsetInBytes, int stride)
        {
            try
            {
                this.Log.LogMethodSignatureTypesAndValues(devicePointer, streamNumber, streamVertexBufferData, offsetInBytes, stride);
                this.GetOrCreateDevice(devicePointer);
                if (this.OnSetStreamSource != null) this.OnSetStreamSource(ref devicePointer, ref streamNumber, ref streamVertexBufferData, ref offsetInBytes, ref stride);
                var vertexBuffer = new VertexBuffer (streamVertexBufferData);
                this.Device.SetStreamSource (streamNumber, vertexBuffer, offsetInBytes, stride);
                return Result.Ok;
            }
            catch (Exception ex)
            {
                this.Log.Fatal(ex);
                return Result.Ok;
            }
        }

        private Result SetStreamSourceFreq(IntPtr devicePointer, int streamNumber, int setting)
        {
            throw new NotImplementedException();
        }

        private Result SetTexture(IntPtr devicePointer, int stage, IntPtr texturePointer)
        {
            try
            {
                this.Log.LogMethodSignatureTypesAndValues(devicePointer, stage, texturePointer);
                this.GetOrCreateDevice(devicePointer);
                if (OnSetTexture != null)
                    OnSetTexture(ref devicePointer, ref stage, ref texturePointer);
                this.Device.SetTexture(stage, new BaseTexture (texturePointer));

                return Result.Ok;
            }
            catch (SharpDXException ex)
            {
                this.Log.Warn(ex);
                return ex.ResultCode;
            }
            catch (Exception ex)
            {
                this.Log.Fatal(ex);
                return Result.UnexpectedFailure;
            }
        }

        private Result SetTextureStageState(IntPtr devicePointer, int stage, TextureStage type, int value)
        {
            throw new NotImplementedException();
        }

        private Result SetTransform(IntPtr devicePointer, TransformState transformState, IntPtr matrix)
        {
            throw new NotImplementedException();
        }

        private Result SetVertexDeclaration(IntPtr devicePointer, IntPtr vertexDeclaration)
        {
            throw new NotImplementedException();
        }

        private Result SetVertexShader(IntPtr devicePointer, IntPtr vertexShader)
        {
            throw new NotImplementedException();
        }

        private Result SetVertexShaderConstantB(IntPtr devicePointer, int startRegister, int[] bools, int vector4BoolCounts)
        {
            throw new NotImplementedException();
        }

        private Result SetVertexShaderConstantF(IntPtr devicePointer, int startRegister, double[] floats, int vector4FloatCount)
        {
            throw new NotImplementedException();
        }

        private Result SetVertexShaderConstantI(IntPtr devicePointer, int startRegister, int[] ints, int vector4IntCount)
        {
            throw new NotImplementedException();
        }

        private Result SetViewport(IntPtr devicePointer, int index, IntPtr viewPort)
        {
            throw new NotImplementedException();
        }

        private Result ShowCursor(IntPtr devicePointer, bool show)
        {
            throw new NotImplementedException();
        }

        private Result StretchRect(IntPtr devicePointer, IntPtr sourceSurface, IntPtr sourceRect, IntPtr destSurface, IntPtr destRect, TextureFilter filterType)
        {
            throw new NotImplementedException();
        }

        private Result TestCooperativeLevel(IntPtr devicePointer)
        {
            throw new NotImplementedException();
        }

        private Result UpdateSurface(IntPtr devicePointer, IntPtr sourceSurface, IntPtr sourceRect, IntPtr destSurface, IntPtr destRect)
        {
            throw new NotImplementedException();
        }

        private Result UpdateTexture(IntPtr devicePointer, IntPtr sourceTexture, IntPtr destTexture)
        {
            throw new NotImplementedException();
        }

        private Result ValidateDevice(IntPtr devicePointer, IntPtr numPasses)
        {
            throw new NotImplementedException();
        }
    }
}
