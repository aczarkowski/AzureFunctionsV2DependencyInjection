# Azure Functions V2 Dependency Injection

Sample demonstrating dependency injection in Azure Functions V2.

It's based on how ILogger is injected into the default function signature.

# Startup project

By using IWebJobsStartup we get access to the IServiceCollection instance where we register our IWelcomeService.

By following how Host binds function parameters as per https://github.com/Azure/azure-webjobs-sdk-extensions/wiki/The-Binding-Process we register IBindingProvider to our GenericBindingProvider which does the whole job.
 