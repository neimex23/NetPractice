# 馃實 TSI.NET 2026 - Trabajo Pr谩ctico

Aplicaci贸n desarrollada en ASP.NET Core MVC como parte del Taller de Sistemas de Informaci贸n.

---

## 馃搶 Descripci贸n

Este proyecto consiste en un sistema de gesti贸n de:

* Pa铆ses
* Confederaciones
* Deportes

Incluye funcionalidades CRUD completas, b煤squedas y persistencia de datos utilizando SQL Server.

---

## 馃П Tecnolog铆as utilizadas

* .NET 10
* ASP.NET Core MVC
* Entity Framework Core
* SQL Server
* LINQ
* Razor Views
* Swagger (para API)

---

## 馃З Arquitectura

El proyecto sigue el patr贸n MVC:

### Modelo (Model)

* Entidades: Pa铆s, Confederaci贸n, Deporte
* L贸gica de negocio y validaciones

### Vista (View)

* Razor Pages
* Formularios CRUD
* B煤squedas

### Controlador (Controller)

* Manejo de requests
* Conexi贸n entre vista y modelo

---

## 鈿欙笍 Funcionalidades

鉁?CRUD completo para todas las entidades
鉁?B煤squeda por texto
鉁?Validaciones de negocio
鉁?Paginaci贸n de datos
鉁?Persistencia en base de datos
鉁?API REST (Ejercicio 3)

---

## 馃梽锔?Base de Datos

* Motor: SQL Server
* ORM: Entity Framework Core
* Enfoque: Code First / Model First
* Uso de Migrations

---

## 馃寪 API REST

Se implementa una API con:

* M茅todos HTTP: GET, POST, PUT, DELETE
* C贸digos de estado HTTP correctos
* Documentaci贸n con Swagger

---

## 馃捇 Frontend (Ejercicio 4)

* Framework JS (React / Vue / Angular)
* Dise帽o responsive con Bootstrap o similar
* B煤squeda full-text

---

## 馃摫 Mobile (Ejercicio 5)

Aplicaci贸n en .NET MAUI que:

* Consume la API REST
* Permite b煤squeda
* Muestra resultados en grilla

---

## 馃殌 C贸mo ejecutar

1. Clonar el repositorio
2. Configurar conexi贸n a SQL Server en `appsettings.json` de MVC

Ejecutar migraciones:
```
dotnet ef migrations add InitialCreate --project .\NetPracticeCore\NetPracticeCore.csproj --startup-project .\NetMVC\NetMVC.csproj
```
```
dotnet ef database update
```

Ejecutar la aplicaci贸n:

---

## 馃懆鈥嶐煉?Autor

Ezequiel Medina
Desarrollador .NET

---

## 馃摎 Notas

* El desarrollo es individual
* Se permite colaboraci贸n conceptual
* Uso obligatorio de C#
