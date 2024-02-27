Site internet d'express Voiture (avec Home public) et un accès admin pour la gestion des véhicules (ajout/modifications/suppresion véhicules, réparations, annonce, photos).
L'administrateur actuel du site est thomas.collet@axa.fr (se rapprocher de lui pour les droits d'accès).
Le site est actuellement publié sur https://p5expressvoituresidentity20240223184658.azurewebsites.net/.

prérequis:  .NET Core DSK 8.0.102
Lors du lancement de l'IDE Visual Studio, cloner le dépot Git

Pour installer les packages Nuggets : utiliser la commande "dotnet restore". A défaut les packages Nuggets utilisés sont :
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

L'application utilise une base de donnée SSMS le nom de la base de donnée est définit dans le connectionStrings de appsettings.json. A modifier si besoin
Dans la console du gestionnaire de package Nuggets il convient de :
