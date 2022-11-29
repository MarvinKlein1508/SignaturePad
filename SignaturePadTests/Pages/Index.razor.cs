namespace SignaturePadTests.Pages
{
    public partial class Index
    {
        public MyInput Input { get; set; } = new();
    }

    public class MyInput
    {
        public byte[] Unterschrift { get; set; } = Array.Empty<byte>();
    }
}