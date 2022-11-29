using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;
using BlazorWebassemblyTests;
using BlazorWebassemblyTests.Shared;

namespace BlazorWebassemblyTests.Pages
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