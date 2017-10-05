# Beautiful REST API design with ASP.NET Core and Ion

Hello! :wave: This repository contains an example API written in C# and ASP.NET Core 1.1. It uses the [Ion hypermedia specification](https://ionwg.org/) as a starting point to model a consistent, clean REST API that embraces HATEOAS. I use this example in my talk [Building Beautiful REST APIs with ASP.NET Core](https://speakerdeck.com/nbarbettini/building-beautiful-rest-apis-in-asp-dot-net-core).

## Deep dive video course (new! :tada:)
 If you want a **four-hour** deep dive on REST, HATEOAS, Ion, API security, ASP.NET Core, and much more, check out my course [Building and Securing RESTful APIs in ASP.NET Core](https://www.lynda.com/ASP-NET-tutorials/Building-Securing-RESTful-API-Multiple-Clients-ASP-NET-Core/590839-2.html) on Lynda.
 
 It covers everything in this example repository and a lot more. (If you don't have a Lynda subscription, send me an e-mail and I'll give you a coupon!)

## Testing it out

0. Clone this repository
0. Build the solution using Visual Studio, or on the [command line](https://www.microsoft.com/net/core) with `dotnet build`.
0. Run the project. The API will start up on http://localhost:50647, or http://localhost:5000 with `dotnet run`.
0. Use an HTTP client like [Postman](https://www.getpostman.com/) or [Fiddler](https://www.telerik.com/download/fiddler) to `GET http://localhost:50647`.
0. HATEOAS
0. Profit! :moneybag:

## Techniques for building RESTful APIs in ASP.NET Core

This example contains a number of tricks and techniques I've learned while building APIs in ASP.NET Core. If you have any suggestions to make it even better, let me know!

+ [Entity Framework Core in-memory for rapid prototyping](#entity-framework-core-in-memory-for-rapid-prototyping)
+ [Model Ion links, resources, and collections](#model-ion-links-resources-and-collections)
+ [Basic API controllers and routing](#basic-api-controllers-and-routing)
+ [Named routes pattern](#named-routes-pattern)
+ [Async/await best practices](#asyncawait-best-practices)
+ [Keep controllers lean](#keep-controllers-lean)
+ [Validate model binding with an ActionFilter](#validate-model-binding-with-an-actionfilter)
+ [Provide a root route](#provide-a-root-route)
+ [Serialize errors as JSON](#serialize-errors-as-json)
+ [Generate absolute URLs automatically with a filter](#generate-absolute-urls-automatically-with-a-filter)
+ [Map resources using AutoMapper](#map-resources-using-automapper)
+ [Use strongly-typed route parameter classes](#use-strongly-typed-route-parameter-classes)
+ [Consume application configuration in services](#consume-application-configuration-in-services)
+ [Add paging to collections](#add-paging-to-collections)

### Entity Framework Core in-memory for rapid prototyping

The [in-memory provider](https://docs.microsoft.com/en-us/ef/core/miscellaneous/testing/in-memory) in Entity Framework Core makes it easy to rapidly prototype without having to worry about setting up a database. You can build and test against a fast in-memory store, and then swap it out for a real database when you're ready.

With the [Microsoft.EntityFrameworkCore.InMemory](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.InMemory) package installed, create a `DbContext`:

```csharp
public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options)
        : base(options)
    {
    }

    // DbSets...
}
```

The only difference between this and a "normal" `DbContext` is the addition of a constructor that takes a `DbContextOptions<>` parameter. This is required by the in-memory provider.

Then, wire up the in-memory provider in `Startup.ConfigureServices`:

```csharp
services.AddDbContext<ApiDbContext>(options =>
{
    // Use an in-memory database with a randomized database name (for testing)
    options.UseInMemoryDatabase(Guid.NewGuid().ToString());
});
```

The database will be empty when the application starts. To make prototyping and testing easy, you can add test data in `Startup.cs`:

```csharp
// In Configure()
var dbContext = app.ApplicationServices.GetRequiredService<ApiDbContext>();
AddTestData(dbContext);

private static void AddTestData(ApiDbContext context)
{
    context.Conversations.Add(new Models.ConversationEntity
    {
        Id = Guid.Parse("6f1e369b-29ce-4d43-b027-3756f03899a1"),
        CreatedAt = DateTimeOffset.UtcNow,
        Title = "Who is the coolest Avenger?"
    });

    // Make sure you save changes!
    context.SaveChanges();
}
```

### Model Ion links, resources, and collections

[Ion](https://ionwg.org) provides a simple framework for describing REST objects in JSON. These Ion objects can be modeled as POCOs in C#. Here's a Link object:

```csharp
public class Link
{
    public string Href { get; set; }

    // Since ASP.NET Core uses JSON.NET by default, serialization can be
    // fine-tuned with JSON.NET attributes
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
    [DefaultValue(GetMethod)]
    public string Method { get; set; }

    [JsonProperty(PropertyName = "rel", NullValueHandling = NullValueHandling.Ignore)]
    public string[] Relations { get; set; }
}
```

Modeling resources and collections is drop-dead simple:

```csharp
// Resources are also (self-referential) links
public abstract class Resource : Link
{
    // Rewritten using LinkRewritingFilter during the response pipeline
    [JsonIgnore]
    public Link Self { get; set; }
}

// Collections are also resources
public class Collection<T> : Resource
{
    public const string CollectionRelation = "collection";

    public T[] Value { get; set; }
}
```

These base classes make returning responses from the API nice and clean.

### Basic API controllers and routing

API controllers in ASP.NET Core inherit from the `Controller` class and use attributes to define routes. The common pattern is naming the controller `<RouteName>Controller`, and using the `/[controller]` attribute value, which automatically names the route based on the controller name:

```csharp
// Handles all routes under /comments
[Route("/[controller]")]
public class CommentsController : Controller
{
    // Action methods...
}
```

Methods in the controller handle specific HTTP verbs and sub-routes. Returning `IActionResult` gives you the flexibility to return both HTTP status codes and object payloads:

```csharp
// Handles route:
// GET /comments
[HttpGet]
public async Task<IActionResult> GetCommentsAsync(CancellationToken ct)
{
    return NotFound(); // 404
    
    return Ok(data); // 200 with JSON payload
}

// Handles route:
// GET /comments/{commentId}
// and {commentId} is bound to the argument in the method signature
[HttpGet("{commentId}"]
public async Task<IActionResult> GetCommentByIdAsync(Guid commentId, CancellationToken ct)
{
    // ...
}
```

### Named routes pattern

If you need to refer to specific routes later in code, you can use the `Name` property in the route attribute to provide a unique name. I like using `nameof` to name the routes with the same descriptive name as the method itself:

```csharp
[HttpGet(Name = nameof(GetCommentsAsync))]
public async Task<IActionResult> GetCommentsAsync(CancellationToken ct)
{
    // ...
}
```

This way, the compiler will make sure route names are always correct.

### Async/await best practices

ASP.NET Core supports async/await all the way down the stack. Any controllers or services that make network or database calls should be `async`. Entity Framework Core provides async versions of database methods like `SingleAsync` and `ToListAsync`.

Adding a `CancellationToken` parameter to your route methods allows ASP.NET Core to notify your asynchronous tasks of a cancellation (if the browser closes a connection, for example).

### Keep controllers lean

I like keeping controllers as lean as possible, by only concerning them with:

* Validating model binding (or not, see below!)
* Checking for null, returning early
* Orchestrating requests to services
* Returning nice results

Notice the lack of business logic! Keeping controllers lean makes them easier to test and maintain. Lean controllers fit nicely into more complex patterns like CQRS or Mediator as well.

### Validate model binding with an ActionFilter

Most routes need to make sure the input values are valid before proceeding. This can be done in one line:

```csharp
if (!ModelState.IsValid) return BadRequest(ModelState);
```

Instead of having this line at the top of every route method, you can factor it out to [an ActionFilter](src/Infrastructure/ValidateModelAttribute.cs) which can be applied as an attribute:

```csharp
[HttpGet(Name = nameof(GetCommentsAsync))]
[ValidateModel]
public async Task<IActionResult> GetCommentsAsync(...)
```

The `ModelState` dictionary contains descriptive error messages (especially if the models are annotated with [validation attributes](https://docs.microsoft.com/en-us/aspnet/core/mvc/models/validation#validation-attributes)). You could return all of the errors to the user, or traverse the dictionary to pull out the first error:

```csharp
var firstErrorIfAny = modelState
    .FirstOrDefault(x => x.Value.Errors.Any())
    .Value?.Errors?.FirstOrDefault()?.ErrorMessage
```

### Provide a root route

It's not HATEOAS unless the API has a clearly-defined entry point. The root document can be defined as a simple resource of links:

```csharp
public class RootResource : Resource
{
    public Link Conversations { get; set; }

    public Link Comments { get; set; }
}
```

And returned from a controller bound to the `/` route:

```csharp
[Route("/")]
public class RootController : Controller
{
    [HttpGet(Name = nameof(GetRoot))]
    public IActionResult GetRoot()
    {
        // return Ok(new RootResponse...)
    }
}
```

### Serialize errors as JSON

A JSON API is expected to return exceptions or API errors as JSON. It's possible to write an [exception filter](https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/filters#exception-filters) that will serialize all MVC errors to JSON, but some errors occur outside of the MVC pipeline and won't be caught by an exception filter.

Instead, exception-handling middleware can be added in `Startup.Configure`:

```csharp
var jsonExceptionMiddleware = new JsonExceptionMiddleware(
    app.ApplicationServices.GetRequiredService<IHostingEnvironment>());
app.UseExceptionHandler(new ExceptionHandlerOptions { ExceptionHandler = jsonExceptionMiddleware.Invoke });
```

Passing `IHostingEnvironment` to the middleware makes it possible to send more detailed information during development, but send a generic error message in production. The middleware is implemented in [JsonExceptionMiddleware.cs](src/Infrastructure/JsonExceptionMiddleware.cs).

### Generate absolute URLs automatically with a filter

The `Controller` base class provides an easy way to generate protocol- and server-aware absolute URLs with `Url.Link()`. However, if you need to generate these links outside of a controller (such as in service code), you either need to pass around the `IUrlHelper` or find another way.

In this project, the [`Link`](src/Models/Link.cs) class represents an absolute link to another resource or collection. The derived [`RouteLink`](src/Models/RouteLink.cs) class can stand in (temporarily) as a placeholder that contains just a route name, and at the very end of the response pipeline the [`LinkRewritingFilter`](src/Infrastructure/LinkRewritingFilter.cs) enerates the absolute URL. (Filters have access to `IUrlHelper`, just like controllers do!)

### Map resources using AutoMapper

It's a good idea to keep the classes that represent database entities separate from the classes that model what is returned to the client. (In this project, for example, the [`CommentEntity`](src/Models/CommentEntity.cs) class contains an `Id` and other properties that aren't directly exposed to the client in [`CommentResource`](src/Models/CommentResource.cs).)

A object mapping library like [AutoMapper](http://automapper.org/) saves you from manually mapping properties from entity classes to resource classes. AutoMapper integrates with ASP.NET Core easily with the [AutoMapper.Extensions.Microsoft.DependencyInjection](https://www.nuget.org/packages/AutoMapper.Extensions.Microsoft.DependencyInjection/) package and a `services.AddAutoMapper()` line in `ConfigureServices`. Most properties are mapped automatically, and you can define a [mapping profile](src/Infrastructure/DefaultAutomapperProfile.cs) for custom cases.

AutoMapper plays nice with Entity Framework Core and async LINQ, too:

```csharp
var items = await query // of CommentEntity
    .Skip(pagingOptions.Offset.Value)
    .Take(pagingOptions.Limit.Value)
    .ProjectTo<CommentResource>() // lazy mapping!
    .ToArrayAsync(ct); // of CommentResource
```

### Use strongly-typed route parameter classes

If you look at the AutoMapper [mapping profile](src/Infrastructure/DefaultAutomapperProfile.cs) from above, you'll notice that strongly-typed route values are saved in each Link:

```csharp
Link.To(
    nameof(ConversationsController.GetConversationByIdAsync),
    new GetConversationByIdParameters { ConversationId = src.Id })
```

(You'll also notice a practical use of the [named routes pattern](#named-routes-pattern)!)

When the URL of the link is generated (later), the provided route values are matched up with the route method definition. The `RouteValues` property is just an `object`, so you could pass an anonymous object instead:

```csharp
Link.To(
    nameof(ConversationsController.GetConversationByIdAsync),
    new { conversationId = src.Id })
```

However, defining a [simple POCO](src/Controllers/GetConversationByIdParameters.cs) makes this type-safe and foolproof:

```csharp
public class GetConversationByIdParameters
{
    [FromRoute]
    public Guid ConversationId { get; set; }
}
```

Instead of defining the parameters directly in the method signature, like this:

```csharp
[HttpGet("{conversationId}", Name = nameof(GetConversationByIdAsync))]
public async Task<IActionResult> GetConversationByIdAsync(
    Guid conversationId,
    CancellationToken ct)
```

The method signature contains the POCO itself:

```csharp
[HttpGet("{conversationId}", Name = nameof(GetConversationByIdAsync))]
public async Task<IActionResult> GetConversationByIdAsync(
    GetConversationByIdParameters parameters,
    CancellationToken ct)
```

### Consume application configuration in services

ASP.NET Core comes with a powerful [configuration system](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration) that you can use to provide dynamic configuration data to your API. You can define a group of properties in `appsettings.json`:

```json
"DefaultPagingOptions": {
  "limit": 25,
  "offset": 0
}
```

And bind that group to a POCO in `ConfigureServices`:

```csharp
services.Configure<Models.PagingOptions>(Configuration.GetSection("DefaultPagingOptions"));
```

This places the POCO in the services (DI) container as a singleton wrapped in `IOptions<>`, which can be injected in controllers and services:

```csharp
private readonly PagingOptions _defaultPagingOptions;

public CommentsController(
    ICommentService commentService,
    IOptions<PagingOptions> defaultPagingOptionsAccessor)
{
    // ...
    _defaultPagingOptions = defaultPagingOptionsAccessor.Value;
}
```

### Add paging to collections

Collections with more than a few dozen items start to become heavy to send over the wire. By enriching the [`Collection<T>` class](src/Models/Collection{T}.cs) with paging metadata, the client can get a paged collection experience complete with HATEOAS navigation links:

```json
{
    "href": "http://api.foo.bar/comments",
    "rel": [ "collection" ],
    "offset": 0,
    "limit": 25,
    "size": 200,
    "first": { "href": "http://api.foo.bar/comments", "rel": [ "collection" ] },
    "next": { "href": "http://api.foo.bar/comments?limit=25&offset=25", "rel": [ "collection" ] },
    "last": { "href": "http://api.foo.bar/comments?limit=25&offset=175", "rel": [ "collection"] },
    "value": [
      "items..."
    ]
}
```

In this project, the [`CollectionWithPaging{T}` class](src/Models/CollectionWithPaging{T}.cs) handles the logic and math behind the scenes. Controllers that return collections accept a `[FromQuery] PagingOptions` parameter that binds to the `limit` and `offset` parameters needed for paging.

Paging using a limit and offset is a stateless approach to paging. You could implement paging statefully (using a cursor) instead by saving a cursor position in the database.

### More to come...
