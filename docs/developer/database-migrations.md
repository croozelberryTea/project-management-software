# Overview
For database migrations we use EF Core Migrations. This is a code first approach. You can see the Microsoft Learn 
article for this feature [here](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli).

For this approach you will need to install the .NET CLI tools EF Core tools. You can learn more about these tools and how to install 
these tools with this Microsoft Learn article [here](https://learn.microsoft.com/en-us/ef/core/cli/dotnet). 
As of writing These tools are installed as such using the .NET CLI which should be included with a working install of the .NET SDK.
To verify that the .NET SDK is working run the following command in your terminal.

## Installation of EF Core tool
```shell
  dotnet --version
```
This should output the version number of the .NET SDK as of writing and with the configuration on my machine it outputs the following:
```text
10.0.101
```

Now that you have confirmed that you have the .NET CLI tools properly installed you can now install the tool that we will need.
```shell
  dotnet tool install --global dotnet-ef
```

To verify installation of the tool you can use the input the following command
```shell
  dotnet ef
```

This should output look something similar to the following:
```text

                     _/\__                                                                                                                                                                                                                                                                       
               ---==/    \\
         ___  ___   |.    \|\                                                                                                                                                                                                                                                                    
        | __|| __|  |  )   \\\                                                                                                                                                                                                                                                                   
        | _| | _|   \_/ |  //|\\                                                                                                                                                                                                                                                                 
        |___||_|       /   \\\/\\                                                                                                                                                                                                                                                                

Entity Framework Core .NET Command-line Tools 10.0.1
```

If you already have the tool installed you may also wish to update it to ensure you are using the most recent version of the tool
```shell
  dotnet tool update --global dotnet-ef
```


