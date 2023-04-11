using System.Text;

namespace BlazorWebassemblyTests
{
    public class MyInput
    {
        public byte[] Signature { get; set; } = Array.Empty<byte>();
        public string SignatureAsBase64 => Encoding.UTF8.GetString(Signature);
    }
}