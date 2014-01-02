pushd %~dp0
pushd ..\src\WPCharting\
msbuild WPCharting.csproj /p:Configuration=Release
popd
nuget pack WPCharting.nuspec