#define MyAppName "Mobile Game Control"
#define MyAppVersion "1"
#define MyAppPublisher "Sharkbyteprojects"
#define MyAppURL "https://discord.gg/z8nVJ4yXZj"
#define MyAppExeName "Game.exe"
#define ApkName "com.Sharkbyteprojects.controllerclient.apk"

[Setup]
AppId={{8BE8FB0B-2405-45DF-B24A-1B419A4ECD9C}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={autopf}\{#MyAppName}
DisableProgramGroupPage=yes
LicenseFile=.\LICENSE.txt
PrivilegesRequired=lowest
PrivilegesRequiredOverridesAllowed=commandline
OutputDir=M:\apps\test_cli\mobile\Game\bin
OutputBaseFilename=serversetup
SetupIconFile=.\Game\iconimg.ico
Compression=lzma
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"
Name: "german"; MessagesFile: "compiler:Languages\German.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
Name: "instdirr"; Description: "Startmenu Link to Instdir"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
Name: "includeapk"; Description: "Store Apk file in InstDir"; GroupDescription: "Install:"; Flags: unchecked

[Files]
Source: ".\Game\bin\Release\netcoreapp3.1\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: ".\Game\bin\Release\netcoreapp3.1\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: ".\bin\{#ApkName}"; DestDir: "{app}\apkfiles"; Flags: ignoreversion recursesubdirs createallsubdirs; Tasks: includeapk

[Icons]
Name: "{autoprograms}\{#MyAppName}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autoprograms}\{#MyAppName}\Dir"; Filename: "{app}"; Tasks: instdirr
Name: "{autoprograms}\{#MyAppName}\ApkDir"; Filename: "{app}"; Tasks: includeapk
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

