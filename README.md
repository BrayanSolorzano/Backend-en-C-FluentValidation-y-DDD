# Prueba Técnica - Programador Backend C#

Esta solución implementa una API REST utilizando **.NET 9**, **Entity Framework Core**, **PostgreSQL** y arquitectura **Domain-Driven Design (DDD)**.

## Arquitectura y Buenas Prácticas
El proyecto está dividido en las siguientes capas:
- **Domain:** Entidades e interfaces de repositorios.
- **Application:** DTOs, Servicios y validaciones con **FluentValidation**.
- **Infrastructure:** DbContext, repositorios y configuraciones de Entity Framework.
- **API:** Controladores REST.

Además, el proyecto incluye **Pruebas Unitarias** con XUnit utilizando una base de datos en memoria (In-Memory).

## Requisitos Previos
- Docker y Docker Compose instalados.
- (Opcional) .NET 9 SDK si se desea correr localmente sin Docker.

## Instrucciones para ejecutar con Docker

1. Abrir una terminal en la raíz del proyecto (donde se encuentra el archivo `docker-compose.yml`).
2. Ejecutar el siguiente comando:
   ```bash
   docker compose up -d --build
   ```
3. El proyecto ejecutará automáticamente las migraciones (Data Seed incluido) al arrancar.

## Endpoints
Una vez que los contenedores estén en ejecución, la API estará disponible en el puerto `5000`:

- **Obtener Marcas de Autos:** 
  `GET http://localhost:5000/api/marcasautos`

- **Documentación OpenAPI (Swagger):** 
  `GET http://localhost:5000/openapi/v1.json`

## Pruebas Unitarias
Para ejecutar las pruebas unitarias localmente, ejecute el siguiente comando en la raíz del proyecto:
```bash
dotnet test
```
