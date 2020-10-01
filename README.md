# WcfCustomMessageEncoding

<a href="https://www.nuget.org/packages/WcfCustomMessageEncoding/">
<img src="https://img.shields.io/nuget/v/WcfCustomMessageEncoding.svg?style=flat" />
</a>
   
NuGet feeds
- Official releases: https://www.nuget.org/packages/WcfCustomMessageEncoding/


WcfCustomMessageEncoding is a basic custom implementation of MessageEncodingBindingElement for using Windows Communication Foundation (WCF) on ASP.NET Core. It was based on [this post](https://docs.microsoft.com/en-us/dotnet/api/system.servicemodel.channels.messageencodingbindingelement?view=dotnet-plat-ext-3.1#examples) from Microsoft Docs and serves to solve communication problems with external services that work with different encoding than your project.


### Example of common encoding error:
```xml
The content type text/xml; charset=ISO-8859-1 of the response message does not match the content type of the binding (text/xml; charset=utf-8). If using a custom encoder, be sure that the IsContentTypeSupported method is implemented properly. The first x bytes of the response were: <?xml version=\"1.0\" encoding=\"ISO-8859-1\"?> ...
```
  
## Dependencies
.NET Standard 2.0+

You can check supported frameworks here:

https://docs.microsoft.com/pt-br/dotnet/standard/net-standard

## Instalation
This package is available through Nuget Packages: https://www.nuget.org/packages/WcfCustomMessageEncoding

**Nuget**
```
Install-Package WcfCustomMessageEncoding
```

**.NET CLI**
```
dotnet add package WcfCustomMessageEncoding
```

# How to Use

```csharp
// A basic binding sample with custom encoding on Reference.cs 
using WcfCustomMessageEncoding;

namespace Reference
{
    //...

    private static System.ServiceModel.Channels.Binding GetBindingForEndpoint()
    {
        var transport = new HttpsTransportBindingElement { AuthenticationScheme = AuthenticationSchemes.Basic };
        var messageEncoding = new CustomTextMessageEncodingBindingElement("iso-8859-1", "text/xml", MessageVersion.Soap11);
    
        return new CustomBinding(messageEncoding,transport);

    }

```

## Thanks to these links 

* [Microsoft Docs](https://docs.microsoft.com/en-us/dotnet/api/system.servicemodel.channels.messageencodingbindingelement?view=dotnet-plat-ext-3.1#examples)
* [dotnet/wcf issues](https://github.com/dotnet/wcf/issues/2550)
* [stackoverflow questions](https://stackoverflow.com/questions/7033442/using-iso-8859-1-encoding-between-wcf-and-oracle-linux)
* [Pathfinder Tech Blog](https://pathfindertech.net/connecting-to-a-php-web-service-with-wcf-in-c)
* [Gitter.im/dotnet/wcf](https://gitter.im/dotnet/wcf?at=5d112f8b492f010bcab2cc63)

## License

```
MIT License

Copyright (c) 2020 Rafael Viegas

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

```