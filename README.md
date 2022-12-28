# Redis
The open source, in-memory data store used by millions of developers as a database, cache, streaming engine, and message broker.

## In-memory data structures
Well-known as a "data structure server", with support for strings, hashes, lists, sets, sorted sets, streams, and more.
Here I'm demonstrate strings, hashes with .NET

## Redis With .NET 6
In order to use Redis with .NET, you need a .NET Redis client. This article shows how to use StackExchange.Redis, a general purpose Redis client. 

## Install StackExchange.Redis 
There are several ways to install this package including:

- With the .NET CLI:
```
dotnet add package StackExchange.Redis
```
- With the package manager console:
```
PM> Install-Package StackExchange.Redis
```
- With the NuGet GUI in Visual Studio

### Connection pooling 
StackExchange.Redis does not support conventional connection pooling. As an alternative solution, you can share and reuse the ConnectionMultiplexer object.

Do not create a separate ConnectionMultiplexer for each operation. Instead, create an instance at the beginning and then reuse the object throughout your process.

ConnectionMultiplexer is thread-safe, so it can be safely shared between threads. For more information, see the Basic Usage.

### Dependency injection of the ConnectionMultiplexer 
As the ConnectionMultiplexer must be shared and reused within a runtime, it’s recommended that you use dependency injection to pass it where it’s needed. There’s a few flavors of dependency injection depending on what you’re using.

A single ConnectionMultiplexer instance should be shared throughout the runtime.

Use the AddSingleton method of IServiceCollection to inject your instance as a dependency in ASP.NET Core when configuring your app’s services in Program.cs

```C#
    public static class RedisIocInstaller
    {
        public static void Install(IServiceCollection services, ConfigurationManager configurationManager)
        {
            var connectionStr = configurationManager.GetConnectionString("DefaultConnection");

            var multiplexer = ConnectionMultiplexer.Connect(connectionStr);
            services.AddSingleton<IConnectionMultiplexer>(multiplexer);

        }
    }
    
    Program.cs:
      RedisIocInstaller.Install(builder.Services, builder.Configuration);
  ```
