using System.Text;

namespace Demos.Core.Models
{
    public class MyInput
    {
        public byte[] Signature { get; set; } = [];
        public string SignatureAsBase64 => Encoding.UTF8.GetString(Signature);
    }


}