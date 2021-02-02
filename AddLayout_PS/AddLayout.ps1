add-type -Path 'VideoOS.Platform.dll'
add-type -Path 'VideoOS.Platform.SDK.dll'

# modify these parameters, or place them on command line
$uri = "http://localhost"
$user = ""
$password = ""
$auth = "Negotiate"
$definitionXml = Get-Content "LayoutNEW.xml"
$layoutName = "LayoutPS"
$layoutDescription = "The best"

# method for logging in

function InitAndLogin {

    $cc = [VideoOS.Platform.Login.Util]::BuildCredentialCache($uri,$user,$password,$auth)

    [VideoOS.Platform.SDK.Environment]::Initialize()
    [VideoOS.Platform.SDK.Environment]::AddServer($uri, $cc, $true)
    [VideoOS.Platform.SDK.Environment]::Login($uri);

    if (![VideoOS.Platform.SDK.Environment]::IsLoggedIn($uri))
    {
        Write-error "Unable to login"
        return
    }
}

InitAndLogin

# the ServerId is used for all ConfigurationItem constructions
$serverId = [VideoOS.Platform.EnvironmentManager]::Instance.MasterSite.ServerId

# Add one layout as defined in the layout.xml text file
$mgtServer = New-Object -TypeName "VideoOS.Platform.ConfigurationItems.ManagementServer" -ArgumentList $serverId

#we get hold of the first group:  "4:3"
$layoutFolder = $mgtServer.LayoutGroupFolder.LayoutGroups[0].LayoutFolder  
$layoutFolder.AddLayout($layoutName, $layoutDescription, $definitionXml)