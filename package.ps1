Remove-Item -Force -ErrorAction SilentlyContinue ./QM-SimpleDataLoader.zip
rd -Force -Recurse -ErrorAction SilentlyContinue package/QM-SimpleDataLoader
md ./package/QM-SimpleDataLoader

dotnet build -c RELEASE .\src\QM-SimpleDataLoader.csproj
copy ./src/bin/Release/net48/QM-SimpleDataLoader.dll ./package/QM-SimpleDataLoader

Compress-Archive ./package/* ./QM-SimpleDataLoader.zip






