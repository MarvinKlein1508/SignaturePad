using Microsoft.JSInterop;

namespace SignaturePad
{
    // This class provides an example of how JavaScript functionality can be wrapped
    // in a .NET class for easy consumption. The associated JavaScript module is
    // loaded on demand when first needed.
    //
    // This class can be registered as scoped DI service and then injected into Blazor
    // components for use.

    public class SignaturePadInterop : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;
        private DotNetObjectReference<SignaturePad> _reference;
        public SignaturePadInterop(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
               "import", "./_content/SignaturePad/signature_pad.min.js").AsTask());
        }

        public async Task InitAsync(DotNetObjectReference<SignaturePad> reference, byte[]? image)
        {
            _reference = reference;
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("setup", new object[] { reference, image is null ? "" : $"data:image/png;base64,{Convert.ToBase64String(image)}" });
        }

        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}