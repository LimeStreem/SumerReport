using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SlimDX;
using SlimDX.D3DCompiler;
using SlimDX.Direct3D11;
using SlimDX.DXGI;
using SummerReport.Camera;
using SummerReport.Projection;
using Buffer = SlimDX.Direct3D11.Buffer;
using Device = SlimDX.Direct3D11.Device;
using Resource = SlimDX.DXGI.Resource;

namespace SummerReport
{
    public partial class Form1 : Form
    {
        private Device device;

        private SwapChain swapChain;

        private InputLayout inputLayout;

        private Effect effect;

        private Buffer vertexBuffer;

        private RenderTargetView renderTarget;

        private RenderTargetView posOutTarget;

        private Form2 form2;

        private IMatrixBinder matrixBinder;
        private Vector4[] verticies;
        private Texture2D posOuttexture;

        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.matrixBinder=new SummerReportMatrixBinder(this);
            form2=new Form2();
            form2.savePosMap.Click += savePosMap_Click;
            form2.Show();
            if (
    Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.None,
        new[] { FeatureLevel.Level_11_0 }, new SwapChainDescription()
        {
            BufferCount = 2,
            Flags = SwapChainFlags.AllowModeSwitch,
            IsWindowed = true,
            ModeDescription = new ModeDescription()
            {
                Format = Format.R8G8B8A8_UNorm,
                Height = Height,
                RefreshRate = new Rational(1, 60),
                Scaling = DisplayModeScaling.Stretched,
                ScanlineOrdering = DisplayModeScanlineOrdering.Progressive,
                Width = Width
            },
            OutputHandle = Handle,
            SampleDescription = new SampleDescription(1, 0),
            SwapEffect = SwapEffect.Discard,
            Usage = Usage.RenderTargetOutput
        }, out device, out swapChain).IsSuccess)
            {
                Vector4[] verticies=new Vector4[]
            {
                new Vector4(-1,1,0.5f,1),
                new Vector4(1,1,0.5f,1),
                new Vector4(-1,-1,0.5f,1),
                new Vector4(1,1,0.5f,1),
                new Vector4(1,-1,0.5f,1),
                new Vector4(-1,-1,0.5f,1), 
            };
                List<Vector4> translated=new List<Vector4>();
                int itr = 200;
                float edgeLength = 2.0f/itr;
                Matrix scaling = Matrix.Scaling(new Vector3(edgeLength));
                Matrix toVertex = Matrix.Translation(edgeLength/2f - 1.0f, edgeLength/2f - 1.0f, 0);
                for (int ix = 0; ix <itr; ix++)
                {
                    for (int iy = 0; iy < itr; iy++)
                    {
                        foreach (var vertex in verticies)
                        {
                            translated.Add(Vector4.Transform(vertex,scaling*toVertex*Matrix.Translation(ix*edgeLength,iy*edgeLength,0)));
                        }
                    }
                }
                this.verticies = verticies = translated.ToArray();
                using (DataStream ds=new DataStream(verticies,true,true))
            {
                vertexBuffer = new Buffer(device,ds,
                    new BufferDescription((int) ds.Length, ResourceUsage.Default, BindFlags.VertexBuffer,
                        CpuAccessFlags.None, ResourceOptionFlags.None, 16));
            }
                using (ShaderBytecode sb=ShaderBytecode.CompileFromFile("shader.fx","fx_5_0"))
                {
                    effect=new Effect(device,sb);
                    inputLayout=new InputLayout(device,effect.GetTechniqueByIndex(0).GetPassByIndex(0).Description.Signature,new InputElement[]{new InputElement()
                    {
                        SemanticName = "POSITION",
                        Format = Format.R32G32B32A32_Float
                    }, });
                }
                using (Texture2D tex=SlimDX.Direct3D11.Resource.FromSwapChain<Texture2D>(swapChain,0))
                {
                    renderTarget=new RenderTargetView(device,tex);
                }
                posOuttexture = new Texture2D(device, new Texture2DDescription()
                {
                    ArraySize = 1,
                    BindFlags = BindFlags.RenderTarget,
                    CpuAccessFlags = CpuAccessFlags.None,
                    Format = Format.R8G8B8A8_UNorm,
                    Height = Height,
                    Width = Width,
                    MipLevels = 1,
                    OptionFlags = ResourceOptionFlags.None,
                    SampleDescription = new SampleDescription(1, 0),
                    Usage = ResourceUsage.Default
                });
                    this.posOutTarget = new RenderTargetView(device, posOuttexture);
                device.ImmediateContext.InputAssembler.SetVertexBuffers(0,new VertexBufferBinding[]{new VertexBufferBinding(vertexBuffer,16,0)});
                device.ImmediateContext.OutputMerger.SetTargets(renderTarget,posOutTarget);
                device.ImmediateContext.InputAssembler.InputLayout = inputLayout;
                device.ImmediateContext.InputAssembler.PrimitiveTopology=PrimitiveTopology.TriangleList;
                device.ImmediateContext.Rasterizer.SetViewports(new Viewport(0,0,Width,Height,0,1));
                device.ImmediateContext.Rasterizer.State = RasterizerState.FromDescription(device,
                    new RasterizerStateDescription()
                    {
                        CullMode = CullMode.None,
                        FillMode = FillMode.Solid
                    });

            }
            else
            {
                throw new Direct3D11Exception("Failed to initialize device and swapchain");
            }    
        }

        void savePosMap_Click(object sender, EventArgs e)
        {
            SlimDX.Direct3D11.Resource.SaveTextureToFile(device.ImmediateContext, posOuttexture, ImageFileFormat.Png,
                string.Format("posMap.bmp"));
        }

        public void Render()
        {
            float max = 1f;
            float min = 0f;
            int itr = 0;
            float xsc = 1f;
            float ysc = 1f;
            min = float.TryParse(form2.minimum.Text, out min) ? min : 0f;
            max = float.TryParse(form2.maximum.Text, out max) ? max : 1f;
            itr = int.TryParse(form2.itr.Text, out itr) ? itr : 0;
            xsc = float.TryParse(form2.xsc.Text, out xsc) ? xsc : 1;
            ysc = float.TryParse(form2.ysc.Text, out ysc) ? ysc : 1;
            device.ImmediateContext.ClearRenderTargetView(renderTarget,new Color4(1,0,1,1));
            device.ImmediateContext.ClearRenderTargetView(posOutTarget, new Color4(1, 1, 1, 1));
            effect.GetVariableBySemantic("WVP").AsMatrix().SetMatrix(Matrix.Identity);
            effect.GetVariableBySemantic("MAX").AsScalar().Set(max);
            effect.GetVariableBySemantic("MIN").AsScalar().Set(min);
            effect.GetVariableBySemantic("IT").AsScalar().Set(itr);
            effect.GetVariableBySemantic("XSC").AsScalar().Set(xsc);
            effect.GetVariableBySemantic("YSC").AsScalar().Set(ysc);
            effect.GetTechniqueByIndex(0).GetPassByIndex(0).Apply(device.ImmediateContext);
            device.ImmediateContext.Draw(verticies.Length,0);
            swapChain.Present(0, PresentFlags.None);
        }
    }
}