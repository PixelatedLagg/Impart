<img src="Images/main.png"></img>
## How to Use:
### For Dotnet CLI:
`dotnet add package Impart`
#### To use a specific version:
`dotnet add package Impart --version (version number)`
### For .csproj Files:
```
<ItemGroup>
    <PackageReference Include="Impart" />
</ItemGroup>
```
#### To use a specific version:
```
<ItemGroup>
    <PackageReference Include="Impart" Version="(version number)" />
</ItemGroup>
```
#### *For .csproj files, it is recommended to specify the version, considering different versions of Nuget either use the latest or earliest version of the package when unspecified.*
### You don't use Dotnet CLI or .csproj files? (I highly recommend them)
#### Find the instructions for use with your package manager on the [Impart Nuget page](https://www.nuget.org/packages/Impart/).
