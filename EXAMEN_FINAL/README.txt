# Administrador de Personajes de One Piece

Este proyecto es una aplicación de escritorio desarrollada en C# que permite administrar información sobre personajes del anime "One Piece" utilizando una base de datos MySQL.

## Descripción

El proyecto consta de dos partes principales: una clase de acceso a datos (`PERSONAJEOP`) que se encarga de la comunicación con la base de datos, y un formulario de interfaz de usuario (`Form1`) que permite al usuario interactuar con los datos de los personajes.

## Funcionalidades Principales

- **Agregar Personaje**: Permite al usuario agregar un nuevo personaje con su nombre, grupo, cargo, nivel de poder, raza, recompensa, fruta del diablo y fecha de creación.
- **Actualizar Personaje**: Permite al usuario actualizar la información de un personaje existente.
- **Eliminar Personaje**: Permite al usuario eliminar un personaje de la base de datos.
- **Mostrar Lista de Personajes**: Muestra una lista de todos los personajes almacenados en la base de datos.
- **Ordenar por Fecha de Creación**: Permite al usuario ordenar la lista de personajes por fecha de creación en orden descendente.
- **Buscar Personajes Recientes**: Permite al usuario encontrar los personajes cuya fecha de creación está dentro de los últimos 5 días.

## Requisitos

- Sistema operativo Windows
- MySQL Server
- Biblioteca MySql.Data.MySqlClient para C#

## Instalación y Uso

1. Clona este repositorio: `git clone https://github.com/Nayeli-Urrutia/EXAMEN_FINAL.git
2. Abre la solución en Visual Studio.
3. Asegúrate de tener la cadena de conexión correcta en la clase `PERSONAJEOP`.
4. Compila y ejecuta el proyecto.

## Contribuir

¡Si quieres contribuir al proyecto, serás bienvenido! Sigue los pasos habituales para hacer un fork, crear una nueva rama, hacer tus cambios y enviar un pull request.

## Créditos

- Desarrollado por [Nayeli Urrutia]

## Licencia

Este proyecto está bajo la [Licencia MIT](https://opensource.org/licenses/MIT).
