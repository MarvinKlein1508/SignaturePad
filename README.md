# SignaturePad
A simple to use blazor component to draw a signature.

![SignaturePad Demo](images/demo.png)

## Installation

You can install from Nuget using the following command:

`Install-Package Blazor.SignaturePad`

Or via the Visual Studio package manger.

## Basic usage
Start by add the following using statement to your root `_Imports.razor`.

    @using SignaturePad

You can then use it wherever you want. 

    <SignaturePad @bind-Value="Input.MyValue" />

The control provides you the image data as `byte[]`