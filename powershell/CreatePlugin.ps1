# Variables
$clientName = "ClientTest"
$basePath = "C:\Temp\Plugins\$clientName"

# Crée le dossier s'il n'existe pas
if (-Not (Test-Path -Path $basePath)) {
    New-Item -ItemType Directory -Path $basePath | Out-Null
    Write-Output "Dossier créé : $basePath"
} else {
    Write-Output "Le dossier existe déjà : $basePath"
}

# Crée un fichier Plugin.cs s'il n'existe pas
$filePath = "$basePath\Plugin.cs"
if (-Not (Test-Path -Path $filePath)) {
    New-Item -ItemType File -Path $filePath | Out-Null
    Write-Output "Fichier créé : $filePath"
} else {
    Write-Output "Le fichier existe déjà : $filePath"
}

Write-Output "Projet plugin prêt pour $clientName à l'emplacement : $basePath"
