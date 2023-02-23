using Microsoft.AspNetCore.Components;
using System.Text;

namespace BlazorServerTests.Pages
{
    public partial class Index
    {
        public MyInput Input { get; set; } = new();

       
        private void SaveSignature()
        {
            memoryService.Signature = Input.Signature;
        }

        private void OpenSignature()
        {
            navigationManager.NavigateTo(Input.SignatureAsBase64);
        }

        private void ReadSignature()
        {
            Input.Signature = memoryService.Signature;
            StateHasChanged();
        }
    }

    public class MyInput
    {
        public byte[] Signature { get; set; } = Array.Empty<byte>();
        public string SignatureAsBase64 => Encoding.UTF8.GetString(Signature);
    }


}