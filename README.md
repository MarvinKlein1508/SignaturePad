# SignaturePad
A simple to use blazor component to draw a signature. It supports both mouse and touch inputs and works on Blazor Server and Blazor WebAssembly.

See a live [demo](https://marvinklein1508.github.io/SignaturePad/) right here on github.

![SignaturePad Demo](images/demo.png)

## Installation

You can install from Nuget using the following command:

`Install-Package Blazor.SignaturePad`

Or via the Visual Studio package manger.

## Basic usage
Start by adding the following using statement to your root `_Imports.razor`.
```
@using SignaturePad
```

Next you should define a property in your class. For example:
```csharp
public class MyInput
{
    public byte[] Signature { get; set; } = Array.Empty<byte>();
}
```

You can then use it wherever you want.
```
    <SignaturePad @bind-Value="Input.Signature" style="width: 100%" />
```

The control provides you the image data as base64 `byte[]`

To get the image, you'll need to convert to `byte[]` into a string. For example:

```csharp
public class MyInput
{
    public byte[] Signature { get; set; } = Array.Empty<byte>();
    public string SignatureAsBase64 => System.Text.Encoding.UTF8.GetString(Signature);
}
```

## Providing options
You can configure the SignaturePad by providing a `SignaturePadOptions` instance to the component. 

```csharp
<SignaturePad @bind-Value="Input.Signature" Options="_options" style="width: 100%" />

@code {
    public MyInput Input { get; set; } = new();

    private SignaturePadOptions _options = new SignaturePadOptions
    {
        LineCap = LineCap.Round,
        LineJoin = LineJoin.Round,
        LineWidth = 20
    };
}
```

## Custom styling
You can customize the looks of SignaturePad by either overriding the CSS classes or by specifiying your own classes.

For the SignaturePad itself you can use the `Class` parameter and for the button to clear the SignaturePad you can use the `ClearButtonClass` parameter.

