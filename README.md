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
## Ejercicio 4 Branch
Ejercicio 4: Frameworks JS 
Objetivo: Trabajar con otras opciones existentes al momento de desarrollar interfaces de usuario, por fuera del ecosistema .NET.
Se quiere tener la posibilidad de realizar búsquedas full text sobre el atributo de texto de la entidad Categoría (probablemente la descripción), y poder visualizarla de manera correcta independientemente del dispositivo y resolución utilizado.
Para esto se optó por la utilización de un framework JS (Vue, Angular, React, etc.) de manera conjunta con un framework de diseño responsive (Bootstrap, Material, etc.).
r .


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

## FrontEnd
- Vista de usuario realizada con ReactJS
- CSS Tailwinds

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
Ejecutar front con ```npm run dev```

---

## 👨‍ Autor

Ezequiel Medina
Desarrollador .NET

---

## 📚 Notas

* El desarrollo es individual
* Se permite colaboración conceptual
* Uso obligatorio de C#
