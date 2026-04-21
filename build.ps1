
 
try {

    # Use convention to the project name
    $projectName = dir .. -Directory | select -Unique parent | %{$_.Parent.Name}

    mkdir package -ErrorAction SilentlyContinue | Out-Null
    Remove-Item package\* -Recurse -Force -ErrorAction SilentlyContinue
    $packageFolder = "./package/$projectName"
    mkdir $packageFolder | Out-Null

    # ---- Build the projects.  The projects will automatically deploy to the Steam Workshop folder.

    "Bootstrap"
    dotnet clean "./src\$($projectName)_Bootstrap.csproj"
    dotnet build -c Release "./src\$($projectName)_Bootstrap.csproj" -o $packageFolder

    "Stable"
    dotnet clean "..\main-repo\src\$($projectName).csproj"
    dotnet build -c Release "..\main-repo\src\$($projectName).csproj" -o "$packageFolder\Stable"
    # dotnet build has a bug where it always copies any project references.
    del "$packageFolder/Stable/$($projectName)_Bootstrap.*"

    "Beta"
    dotnet clean "..\beta\src\$($projectName).csproj"
    dotnet build -c Release "..\beta\src\$($projectName).csproj" -o "$packageFolder\Beta"
    # dotnet build has a bug where it always copies any project references.
    del "$packageFolder/Beta/$($projectName)_Bootstrap.*"

    # ---- Create the package zip file.
    Copy-Item ../main-repo/media/thumbnail.png $packageFolder
    Copy-Item ../main-repo/README.md $packageFolder
    Copy-Item version-info.json $packageFolder
    Copy-Item modmanifest.json $packageFolder

    Compress-Archive -Path "$packageFolder\*" -DestinationPath "./$($projectName).zip" -Force

    # Add the beta text if beta is not disabled.
    # Otherwise remove?

    "Build completed"
} catch {
    Write-Error "Build Failure: $_"
    exit 1
}



