# 🌍 TSI.NET 2026 - Trabajo Práctico

Aplicación desarrollada en ASP.NET Core MVC como parte del Taller de Sistemas de Información.

---

## 📌 Descripción

Este proyecto consiste en un sistema de gestión de:

* Países
* Confederaciones
* Deportes

Incluye funcionalidades CRUD completas, búsquedas y persistencia de datos utilizando SQL Server.

---

## 🧱 Tecnologías utilizadas

* .NET 10
* ASP.NET Core MVC
* Entity Framework Core
* SQL Server
* LINQ
* Razor Views
* Swagger (para API)

---

## 🧩 Arquitectura

El proyecto sigue el patrón MVC:

### Modelo (Model en DLL PracticeCore)

* Entidades: País, Confederación, Deporte
* Lógica de negocio y validaciones

### Vista (View)

* Razor Pages
* Formularios CRUD
* Búsquedas

### Controlador (Controller)

* Manejo de requests
* Conexión entre vista y modelo

---
## Ejercicio 3 Branch
ASP.NET Core Web API
Objetivo: Tener un primer acercamiento a la utilización de servicios web en la plataforma .NET, en este caso mediante la utilización de ASP.NET Core Web API  .
En este ejercicio se debe agregar una nueva capa de servicios REST al prototipo del ejercicio 1. Esta capa de servicios debe proveer las mismas funcionalidades de CRUD y búsqueda sobre las entidades del Ejercicio 1.
El resultado del ejercicio debe contemplar el correcto manejo de los códigos de estado de respuesta HTTP  , así como también de los métodos HTTP  .
Se sugiere como buena práctica documentar la Web API con Swagger  .



---

## ⚙️ Funcionalidades

✅CRUD completo para todas las entidades
✅Búsqueda por texto
✅Validaciones de negocio
✅ Paginación de datos
✅Persistencia en base de datos
✅ API REST 

---

## 🗄Base de Datos

* Motor: SQL Server
* ORM: Entity Framework Core
* Enfoque: Code First / Model First
* Uso de Migrations

---

## 🌐 API REST

Se implementa una API con:

* Métodos HTTP: GET, POST, PUT, DELETE
* Códigos de estado HTTP correctos
* Documentación con Swagger

---
## Separacion de Logica de Negocio
* DLL Libreria de clases para modelos, DbContext,etc

---

## 🚀 Cómo ejecutar

1. Clonar el repositorio
2. Configurar conexión a SQL Server en `appsettings.json` de MVC y WebApi

Ejecutar migraciones:
```
dotnet ef migrations add InitialCreate --project .\NetPracticeCore\NetPracticeCore.csproj --startup-project .\NetMVC\NetMVC.csproj
```
```
dotnet ef database update
```

- Ejecutar WebApi
- Ejecutar React Web en visual studio code con 

---

## 👨‍ Autor

Ezequiel Medina
Desarrollador .NET

---

## 📚 Notas

* El desarrollo es individual
* Se permite colaboración conceptual
* Uso obligatorio de C#
