using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text;


namespace SignaturePad
{
    public partial class SignaturePad
    {
        [Parameter]
        public byte[] Value { get; set; } = [];
        
        [Parameter]
        public EventCallback<byte[]> ValueChanged { get; set; }
        [Parameter]
        public SignaturePadOptions Options { get; set; } = new SignaturePadOptions();

        [Parameter]
        public bool ShowClearButton { get; set; } = true;
        [Parameter]
        public string ClearButtonClass { get; set; } = "btn btn-default";
        [Parameter]
        public string Class { get; set; } = "signature";
        [Parameter]
        public string ClearButtonText { get; set; } = "Clear Signature";
        [Parameter]
        public bool Disabled { get; set; }



        /// <summary>
        /// Captures all the custom attributes that are not part of BlazorBootstrap component.
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> Attributes { get; set; } = [];

        private readonly string _id = Guid.NewGuid().ToString();
        private readonly DotNetObjectReference<SignaturePad> _reference;
        private IJSObjectReference? _jsModule;
      
        
        public SignaturePad()
        {
            _reference = DotNetObjectReference.Create(this);
        }

        [JSInvokable]
        public async Task SignatureDataChangedAsync()
        {
            using MemoryStream memoryStream = new();
            var dataReference = await _jsModule!.InvokeAsync<IJSStreamReference>("getBase64", _id);
            using var dataReferenceStream = await dataReference.OpenReadStreamAsync(maxAllowedSize: 10_000_000);
            await dataReferenceStream.CopyToAsync(memoryStream);

            string base64 = Encoding.UTF8.GetString(memoryStream.ToArray());

            try
            {
                Value = Convert.FromBase64String(base64);
            }
            catch (Exception)
            {
                Value = [];
            }


            await ValueChanged.InvokeAsync(Value);

        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _jsModule = await jsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/Blazor.SignaturePad/sigpad.interop.js?ver=8.1.3");
                await Setup();
                await Update();
                await UpdateImage();
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        protected override async Task OnParametersSetAsync()
        {
            await Update();
            await UpdateImage();
        }

        private static string ByteToData(byte[] data)
        {
            return $"{Encoding.UTF8.GetString(data)}";
        }

        private async Task Setup()
        {
            if (_jsModule is not null)
            {
                await _jsModule.InvokeVoidAsync("setup", [_id, _reference, Options.ToJSON(), Value is null ? string.Empty : ByteToData(Value)]);
            }
        }

        private async Task Update()
        {
            if (_jsModule is not null)
            {
                await _jsModule.InvokeVoidAsync("update", [_id, Options.ToJSON()]);
            }
        }

        [JSInvokable]
        public async Task UpdateImage()
        {
            if (_jsModule is not null)
            {
                await _jsModule.InvokeVoidAsync("updateImage", [_id, Value is null ? string.Empty : ByteToData(Value)]);
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_jsModule is not null)
            {
                try
                {
                    await _jsModule.InvokeVoidAsync("destroy", [_id]);
                    await _jsModule.DisposeAsync();
                }
                catch (TaskCanceledException)
                {
                }
                catch (JSDisconnectedException)
                {
                }
            }

        }

        public async Task Clear()
        {
            if (_jsModule is not null)
            {
                await _jsModule.InvokeVoidAsync("clear", [_id, Value is null ? String.Empty : ByteToData(Value)]);
                Value = [];
                await ValueChanged.InvokeAsync(Value);
                await UpdateImage();
            }
        }
    }
}