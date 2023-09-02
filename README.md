# BorysExperimental

## Description

This is a collection of experimental scripts and tools that I have created for some games. They are not production ready, neither are guaranteed to work perfectly, nor safe to use.


## Features

### Partial Methods

Partial methods are a feature allowing you to split the body of a function into multiple parts in the same class and inheritors, but without the need of partial classes.

```csharp

public class MyPartialBehaviour : PartialMonoBehaviour
{
    [PartialMethodDefinition]
    private void MyPartialMethod()
    {
        CallPartialMethod("MyPartialMethod");
    }
    
    [PartialMethod]
    private void MyPartialMethodPart1()
    {
        // Do something
    }
    
    [PartialMethod]
    private void MyPartialMethodPart2()
    {
        // Do something else
    }
    
    [PartialMethod]
    private void MyPartialMethodPart3()
    {
        // Do something else
    }
}

```

It is also useful when you want to have a function that is called from multiple places but you want to have different functionality in each place.

```csharp



```


## Systems

Coming soon...

## Tools

Coming soon...