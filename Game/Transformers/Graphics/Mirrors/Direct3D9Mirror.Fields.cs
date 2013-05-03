using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Direct3D9;

namespace Game.Transformers.Graphics.Mirrors
{
    public partial class Direct3D9Mirror
    {
        private List<CubeTexture> CubeTextures { get; set; }

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

        private void InitializeFields()
        {
            SwapChains = new List <SwapChain>();
            Textures = new List <Texture>();
            CubeTextures = new List <CubeTexture>();
            VolumeTextures = new List <VolumeTexture>();
            Surfaces = new List <Surface>();
            IndexBuffers = new List <IndexBuffer>();
            PixelShaders = new List <PixelShader>();
            VertexShaders = new List <VertexShader>();
            Queries = new List <Query>();
            StateBlocks = new List <StateBlock>();
            VertexBuffers = new List <VertexBuffer>();
            VertexDeclarations = new List <VertexDeclaration>();
        }
    }
}
