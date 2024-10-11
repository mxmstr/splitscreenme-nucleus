using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;
using System;
using System.Windows.Forms;
using System.Windows.Media;
using Device = SharpDX.Direct3D11.Device;
using Factory = SharpDX.Direct2D1.Factory;
using SwapChain = SharpDX.DXGI.SwapChain;

public class Direct2DControl : Control
{
    private Device d3dDevice;
    private SwapChain swapChain;
    private RenderTarget renderTarget;
    private SolidColorBrush rectangleBrush;

    public Direct2DControl()
    {
        // Ensure the control is redrawn
        this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque, true);
        this.Load += Direct2DControl_Load;
    }

    private void Direct2DControl_Load(object sender, EventArgs e)
    {
        InitializeDirect2D();
    }

    private void InitializeDirect2D()
    {
        // Create a Direct3D11 device
        var creationFlags = DeviceCreationFlags.BgraSupport; // Needed for Direct2D support
        d3dDevice = new Device(SharpDX.Direct3D.DriverType.Hardware, creationFlags);

        // Describe the swap chain
        var swapChainDesc = new SwapChainDescription
        {
            BufferCount = 1,
            ModeDescription = new ModeDescription(this.ClientSize.Width, this.ClientSize.Height, new Rational(60, 1), Format.R8G8B8A8_UNorm),
            Usage = Usage.RenderTargetOutput,
            OutputHandle = this.Handle,
            IsWindowed = true,
            SampleDescription = new SampleDescription(1, 0),
            SwapEffect = SwapEffect.Discard,
        };

        // Create the swap chain
        using (var dxgiFactory = new Factory1())
        {
            swapChain = new SwapChain(dxgiFactory, d3dDevice, swapChainDesc);
        }

        // Create Direct2D factory
        var d2dFactory = new Factory();

        // Get the back buffer of the swap chain and create a Direct2D render target
        using (var backBuffer = swapChain.GetBackBuffer<Surface>(0))
        {
            var renderTargetProperties = new RenderTargetProperties(new PixelFormat(Format.Unknown, AlphaMode.Premultiplied));
            renderTarget = new RenderTarget(d2dFactory, backBuffer, renderTargetProperties);
        }

        // Create a brush for the rectangle
        rectangleBrush = new SolidColorBrush(renderTarget, new RawColor4(0.0f, 1.0f, 0.0f, 1.0f)); // Green
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        if (renderTarget != null)
        {
            // Start drawing
            renderTarget.BeginDraw();
            renderTarget.Clear(new RawColor4(0, 0, 0, 0)); // Clear with a black color

            // Draw the rectangle
            var rect = new RawRectangleF(50, 50, 200, 200);
            renderTarget.FillRectangle(rect, rectangleBrush);

            // End drawing
            renderTarget.EndDraw();

            // Present the swap chain (display the frame)
            swapChain.Present(1, PresentFlags.None);
        }
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);

        // Handle resizing of the control
        if (swapChain != null)
        {
            renderTarget.Dispose();
            swapChain.ResizeBuffers(1, this.ClientSize.Width, this.ClientSize.Height, Format.R8G8B8A8_UNorm, SwapChainFlags.None);

            using (var backBuffer = swapChain.GetBackBuffer<Surface>(0))
            {
                var d2dFactory = new Factory();
                var renderTargetProperties = new RenderTargetProperties(new PixelFormat(Format.Unknown, AlphaMode.Premultiplied));
                renderTarget = new RenderTarget(d2dFactory, backBuffer, renderTargetProperties);
            }
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            renderTarget?.Dispose();
            swapChain?.Dispose();
            d3dDevice?.Dispose();
            rectangleBrush?.Dispose();
        }
        base.Dispose(disposing);
    }
}
