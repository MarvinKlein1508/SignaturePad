using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text;


namespace SignaturePad
{
    public partial class SignaturePad
    {
        [Parameter]
        public byte[] Value
        {
            get => _value;
            set
            {
                if(value == _value) return;

                _value = value;
                UpdateImage();
            }
        }
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
        public Dictionary<string, object> Attributes { get; set; } = new();

        private string _id = Guid.NewGuid().ToString();
        private DotNetObjectReference<SignaturePad> _reference;
        private IJSObjectReference? jsModule;
        private bool _rendered = false;
        private byte[] _value = Array.Empty<byte>();

        public SignaturePad()
        {
            _reference = DotNetObjectReference.Create(this);
        }

        [JSInvokable]
        public async Task SignatureDataChangedAsync()
        {
            using MemoryStream memoryStream = new MemoryStream();
            var dataReference = await jsModule.InvokeAsync<IJSStreamReference>("getBase64", _id);
            using var dataReferenceStream = await dataReference.OpenReadStreamAsync(maxAllowedSize: 10_000_000);
            await dataReferenceStream.CopyToAsync(memoryStream);

            string base64 = Encoding.UTF8.GetString(memoryStream.ToArray());

            try
            {
                Value = Convert.FromBase64String(base64);
            }
            catch (Exception)
            {
                Value = Array.Empty<byte>();
            }


            await ValueChanged.InvokeAsync(Value);

        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                jsModule = await jsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/Blazor.SignaturePad/sigpad.interop.js");
                await Setup();
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        protected override async Task OnParametersSetAsync()
        {
            if (_rendered)
            {
                await Update();
                await UpdateImage();
            }
        }

        private string ByteToData(byte[] data)
        {
            return $"{Encoding.UTF8.GetString(data)}";
        }

        private async Task Setup()
        {
            if (jsModule is not null)
            {
                await jsModule.InvokeVoidAsync("setup", new object[] { _id, _reference, Options.ToJSON(), Value is null ? String.Empty : ByteToData(Value) });
            }
        }

        private async Task Update()
        {
            if (jsModule is not null)
            {
                await jsModule.InvokeVoidAsync("update", new object[] { _id, Options.ToJSON() });
            }
        }

        [JSInvokable]
        public async Task UpdateImage()
        {
            if (jsModule is not null)
            {
                await jsModule.InvokeVoidAsync("updateImage", new object[] { _id, Value is null ? String.Empty : ByteToData(Value) });
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (jsModule is not null)
            {
                try
                {
                    await jsModule.InvokeVoidAsync("destroy", new object[] { _id });
                    await jsModule.DisposeAsync();
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
            if (jsModule is not null)
            {
                await jsModule.InvokeVoidAsync("clear", new object[] { _id, Value is null ? String.Empty : ByteToData(Value) });
                Value = Array.Empty<byte>();
                await ValueChanged.InvokeAsync(Value);
                await UpdateImage();
            }
        }
    }
}