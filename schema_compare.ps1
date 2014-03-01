$databaseName = "Twitta"
$base_dir = resolve-path .
$source_dir = "$base_dir\source"
$databaseScripts = "$source_dir\Database.Twitta"
$SqlPackage = "C:\Program Files (x86)\Microsoft SQL Server\110\DAC\bin\SqlPackage.exe"
$AliaSQL = "$base_dir\lib\AliaSQL\AliaSQL.exe"
$databaseName_Original = "$databaseName" + "_Original"
$databaseScriptsUpdate = "$databaseScripts\Scripts\Update"
write-host $databaseScriptsUpdate
#$newScriptName = ((Get-ChildItem $databaseScriptsUpdate -filter "*.sql" | ForEach-Object {[int]$_.Name.Substring(0, 4)} | Sort-Object)[-1] + 1).ToString("0000-") + "$databaseName" + ".sql.temp"

  $maxfile = Get-ChildItem $databaseScriptsUpdate -filter "*.sql" | ?{$_.BaseName -like "[0-9][0-9][0-9][0-9]"} | Sort BaseName -Descending | Select -First 1 -Expand BaseName;
  if (!$maxfile) { $maxfile = "0000" }
  [int32]$filenumberint     = $maxfile.substring(1); $filenumberint++
  [string]$filenumberstring = ($filenumberint).ToString("0000"); 
  [string]$newName          = ($filenumberstring + "-$databaseName.sql.temp");

write-host $databaseScriptsUpdate\$newName