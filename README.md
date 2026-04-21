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
## Ejercicio 5 Branch
Ejercicio 5: .NET MAUI	
Objetivo: Desarrollo de aplicaciones móviles nativas en el ecosistema .NET utilizando .NET MAUI
Se desea obtener detalles de la entidad en la que se está trabajando. Para esto, se desarrollará una aplicación móvil nativa utilizando .NET MAUI, la cual tendrá un campo de búsqueda (de tipo texto), desde donde se llamará al método de búsqueda de la web API REST desarrollada anteriormente, y se mostrarán los resultados en forma de grilla.



---

## ⚙️ Funcionalidades

✅CRUD completo para todas las entidades
✅Búsqueda por texto
✅Validaciones de negocio
✅ Paginación de datos
✅Persistencia en base de datos
✅ API REST 
✅ Front ReactJS
✅ Aplicacion Mobile con NET MAUI

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

## FrontEnd
- Vista de usuario realizada con ReactJS
- CSS Tailwinds

## Mobile
- Aplicacion para entorno Mobile

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

Ejecutar la aplicación de webapi
Ejecutar aplicacion mobile

---

## 👨‍ Autor

Ezequiel Medina
Desarrollador .NET

---

## 📚 Notas

* El desarrollo es individual
* Se permite colaboración conceptual
* Uso obligatorio de C#
