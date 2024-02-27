Site internet d'express Voiture:
Ce site propose une interface publique et un accès administrateur pour la gestion des véhicules (ajout/modifications/suppresion véhicules, réparations, annonce, photos).
L'administrateur actuel du site est thomas.collet@axa.fr.
Le site est actuellement publié sur https://p5expressvoituresidentity20240223184658.azurewebsites.net/.

Prérequis: 
.NET Core DSK 8.0.102

Clonage du dépot:
Lors du lancement de l'IDE Visual Studio, cloner le dépot Git

Installation des packages Nuggets : 
Utilisez la commande "dotnet restore" pour installer les packages NuGet nécessaires. Voici la liste des packages NuGet utilisés :
coverlet.collector, 
Microsoft.AspNetCore.Components.QuickGrid, 
Microsoft.AspNetCore.Components.QuickGrid.EntityFrameworkAdapter, 
Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore, 
Microsoft.AspNetCore.Identity.EntityFrameworkCore
Microsoft.AspNetCore.Identity.UI
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.Design
Microsoft.EntityFrameworkCore.InMemory
Microsoft.EntityFrameworkCore.Sqlite
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
Microsoft.NET.Test.Sdk
Microsoft.VisualStudio.Web.CodeGeneration.Design
Moq
xunit
xunit.runner.visualstudio

Configuration de la base de données:
L'application utilise une base de donnée SQL Server Management Studio (SSMS). Le nom de la base de donnée est défini dans le connectionStrings de appsettings.json. Modifiez-le si nécessaire.
Dans la console du gestionnaire de package NuGet, exécutez la commande Update-Database pour mettre à jour la base de données.
