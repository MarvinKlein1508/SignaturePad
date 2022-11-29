# SignaturePad
Stellt eine Implementierung eines Feldes zum Unterschreiben innerhalb von Blazor-Anwendungen bereit. 

## Installation
Zur Verwendung dieses Packages muss es lediglich über NuGet installiert werden. 

In __Imports.razor_ muss folgende Using-Anweisung hinzugefügt werden

    @using SignaturePad


## Verwendung
Ein Signaturepad kann relativ leicht eingebunden werden

    <SignaturePad @bind-Value="Input.MyValue" />

Der Wert von `MyValue` entspricht hierbei ein `byte[]`

Die maximale Größe der Bilddatei beträgt 10MB.