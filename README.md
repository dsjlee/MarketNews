# MarketNews
Blazor WebAssembly progressive web application using FinnHub news api

Demo of running application: https://dlee.azurewebsites.net/

## Instruction
Sign up to get API key from https://finnhub.io

In Startup.cs of WasmServer.Server project, replace Configuration["FinnHub:ApiKey"] with your own API key.

```c#
public Startup(IConfiguration configuration)
{
    Configuration = configuration;
    Helper.FinnHubApiKey = Configuration["FinnHub:ApiKey"];
}
```
Alternatively, enable user-secrets for WasmServer.Server project and add key-value pair to avoid committing secret-key to repository

```javascript
"FinnHub": {
    "ApiKey": "yourownapikey"
}
```

For deployment, you'll need to arrange how to replace api key. e.g. Use Key-vaults service if deploying to Azure.
