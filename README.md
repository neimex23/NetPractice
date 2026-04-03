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

### Modelo (Model)

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

## ⚙️ Funcionalidades

✔ CRUD completo para todas las entidades
✔ Búsqueda por texto
✔ Validaciones de negocio
✔ Paginación de datos
✔ Persistencia en base de datos
✔ API REST (Ejercicio 3)

---

## 🗄️ Base de Datos

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

## 💻 Frontend (Ejercicio 4)

* Framework JS (React / Vue / Angular)
* Diseño responsive con Bootstrap o similar
* Búsqueda full-text

---

## 📱 Mobile (Ejercicio 5)

Aplicación en .NET MAUI que:

* Consume la API REST
* Permite búsqueda
* Muestra resultados en grilla

---

## 🚀 Cómo ejecutar

1. Clonar el repositorio
2. Configurar conexión a SQL Server en `appsettings.json`

Ejecutar migraciones:

```
dotnet ef database update
```

Ejecutar la aplicación:

```
dotnet run
```

---

## 👨‍💻 Autor

Ezequiel Medina
Desarrollador .NET

---

## 📚 Notas

* El desarrollo es individual
* Se permite colaboración conceptual
* Uso obligatorio de C#
