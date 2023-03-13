# SignaturePad
A simple to use blazor component to draw a signature. It supports both mouse and touch inputs and works on Blazor Server and Blazor WebAssembly.

See a live [demo](https://marvinklein1508.github.io/SignaturePad/) right here on github.

![SignaturePad Demo](images/demo.png)

## Installation

You can install from Nuget using the following command:

`Install-Package Blazor.SignaturePad`

Or via the Visual Studio package manger.

## Basic usage
Start by add the following using statement to your root `_Imports.razor`.

    @using SignaturePad

Next you should define a property in your class. For example:
```csharp
public class MyInput
{
    public byte[] Signature { get; set; } = Array.Empty<byte>();
}
```

You can then use it wherever you want.
```
    <SignaturePad @bind-Value="Input.Signature" />
```

The control provides you the image data as base64 `byte[]`

To get the image, you'll need to convert to `byte[]` into a string. For example:

```csharp
public class MyInput
{
    public byte[] Signature { get; set; }
    public string SignatureAsBase64 => System.Text.Encoding.UTF8.GetString(Signature);
}
```