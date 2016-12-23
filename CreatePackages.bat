ECHO Creating packages!
cd A.Core.Interfaces.Nuget
nuget pack
cd ..
cd A.Core.Model.Nuget
nuget pack
cd ..
cd A.Core.Nuget
nuget pack
cd ..
cd A.Core.Services.Nuget
nuget pack
cd ..
cd A.Core.WebAPI.Nuget
nuget pack
cd ..