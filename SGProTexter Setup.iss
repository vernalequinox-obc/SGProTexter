; SEE THE DOCUMENTATION FOR DETAILS ON CREATING .ISS SCRIPT FILES!

[Setup]
AppID={{FABAC6E9-3EE2-4C55-B069-98C1A7CEE4AB}
AppName=SGProTexter End Of Script for Sequence Generator Pro
AppVerName=SGProTexter Version 2.0
AppVersion=2.0
AppPublisher=Grandadcast Jess
DefaultDirName={commonpf32}\SGProTexter
DefaultGroupName=SGProTexter
; Since no icons will be created in "{group}", we don't need the wizard
; to ask for a Start Menu folder name:
DisableProgramGroupPage=yes
OutputDir="C:\CodeProjects\SGProTexter"
OutputBaseFilename="SGProTexter Setup"
SetupIconFile=C:\CodeProjects\SGProTexter\SGProTexter.ico
Compression=lzma2
SolidCompression=yes
UninstallDisplayIcon="{commonpf32}\SGProTexter"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Dirs]
Name: "{autoprograms}\SGProTexter"

[Files]
Source: "C:\CodeProjects\SGProTexter\bin\Release\net8.0\*.*"; DestDir: "{commonpf32}\SGProTexter"
Source: "C:\CodeProjects\SGProTexter\SGProTexter.ico"; DestDir: "{commonpf32}\SGProTexter"

[Icons]
Name: "{autoprograms}\SGProTexter\SGProTexter"; Filename: "{commonpf32}\SGProTexter\SGProTexter.exe"; IconFilename: "{commonpf32}\SGProTexter\SGProTexter.ico";  WorkingDir: "{commonpf32}\SGProTexter"
Name: "{autoprograms}\SGProTexter\Uninstall SGProTexter"; Filename: "{commonpf32}\SGProTexter\unins000.exe"
Name: "{commondesktop}\SGProTexter"; Filename:"{commonpf32}\SGProTexter\SGProTexter.exe"; Tasks: desktopicon; IconFilename: "{commonpf32}\\SGProTexter\SGProTexter.ico"