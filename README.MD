# Swagger api config

This library gives you a basic implementation of swagger integrated with api versioning and oauth authentication services

# How to use it

First of all you need to install it

### dotnet install

``` bash
dotnet add package swagger.api.config
```

### Power shell
``` PowerShell
Install-Package package swagger.api.config

``` 

## Settings

You need to configure how you're going to show things on your swagger document. To do that we recommend using the `AddVersionedSwagger` extension to add all the required services onto the service provider like this.

``` csharp
services.AddVersionedSwagger(
    config => config
        .WithApiName("My api name")
        .WithAssembly(this.GetType().Assembly)
        .WithAuthUrl("http://AuthUrl")
        .WithScope("openid", "openId scope")
        .WithScope("theApiScope", "An scope of my api"));
```

You also need to add the swagger middleware, acknowledging on the swagger ui every version that's defined onto the api. We have simplified this process with one simple extension method `UseVersionedSwagger`, that you can use it like this.

``` csharp

//.. other middleware

app.UseVersionedSwagger();

//.. other middleware

```

We do recommend to execute this middleware before mvc's routing and authentication/authorization, and after exception handling.

## On the controller

On the controller side, to make use of the library potential you need to use Mvc's `ApiVersion` Attribute to set the version used, The `VersionedRoute` attribute on controller level and `VersionedActionRoute` on action level, to let the template take the form of `api/v{V}/{template}`.

As an example

``` csharp

[ApiVersion("1.0"), VersionedRoute("My")]
public class MyController : Controller
{
    // GET ./api/v1/My
    [HttpGet]
    public IActionResult GetMy()
    {
        return Ok("my");
    }

    // GET ./api/v1/My/1
    [HttpGet("{id}")]
    public IActionResult GetSubMy(int id)
    {
        return Ok($"my {id}");
    }

    // GET ./api/v1/My/1/sub
    [HttpGet("{id}/sub")]
    public IActionResult GetSubMy(int id)
    {
        return Ok($"my {id} sub");
    }

    // GET ./api/v1/theOtherMy
    [HttpGet, VersionedActionRoute("theOtherMy")]
    public IActionResult GetTheOtherMy()
    {
        return Ok("theOtherMy");
    }
}

```

You may notice that HttpGet Templates stays true to his normal function, and versioned route just adds the prefix given a version.

To use another version you can use the same attribute like this

``` csharp

[ApiVersion("2.0"), VersionedRoute("My")]
public class MyController : Controller
{
    // GET ./api/v2/My
    [HttpGet]
    public IActionResult GetMy()
    {
        return Ok("my second version");
    }
}

```

# Notes

- Right now it only supports oauth2 Resource owner password credentials for authentication, maybe a future change would be the support of other authentication methods
- Another good change can be the use of diferent prefixes