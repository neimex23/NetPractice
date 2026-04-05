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
##  Branch: ejercicio1

Objetivo: El objetivo de este ejercicio es poder comenzar con la utilización de la plataforma .NET en tareas de desarrollo e implementación.
Utilizando la plataforma .NET, se desea implementar un prototipo que permita administrar los países junto a la confederación y el deporte. Son 3 entidades distintas relacionadas, y los atributos a definir son parte del problema, tomando en cuenta de utilizar al menos 3 tipos distintos (numérico, fecha, string, etc.). Se deben agregar las restricciones SQL correspondientes

Desarrollar una aplicación ASP.NET Core MVC siguiendo los siguientes lineamientos:
Capa de presentación: debe brindar las funcionalidades CRUD de las entidades, así como también al menos una funcionalidad de búsqueda por un atributo de texto de alguna de ellas. Las vistas deben implementarse utilizando Razor 
Capa de negocios: debe brindar al menos las mismas funcionalidades que la capa de presentación, así como también realizar las validaciones necesarias. Debe controlarse que se cumpla al menos una regla de negocio al momento de agregar, modificar o borrar una entidad, la cual dependerá de la entidad elegida. Importante: la capa de negocios es parte del modelo en una arquitectura MVC. 
Capa de acceso a datos: esta capa debe proveer las funcionalidades necesarias para el mantenimiento de los datos por parte de las capas superiores (de momento la capa de negocios). A esta altura del trabajo práctico solamente se mantendrán datos en memoria.
Notas:
Los datos deben paginarse desde el servidor
Pueden consultarse y definir cosas en conjunto con sus compañeros de grupo, pero el desarrollo es individual.
Debe utilizarse, en todos los casos, C# como lenguaje de desarrollo.


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

✔ Integrada en Memoria

---

## 🚀 Cómo ejecutar

1. Clonar el repositorio

2. Ejecutar la aplicación:

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
