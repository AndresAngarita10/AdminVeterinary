# AdminVeterinary

## Introduccion
Este proyecto proporciona una API en la cual permite el manejo de una veterinaria. Se cuenta con un sistema cerrado en el cual existe uno o varios administradores, empleados.
cada uno de los roles de usario podra manejar la informacion de los clientes, mascotas incluyendo sus citas medicas y tratamientos. 
Compras de diferentes proveedores y las ventas diarias que puedan realizarse.

❗- Al momento de realizar el update de la base de datos y ejecutar por primera vez se insertaran los datos con los cuales nos daran la facilida de 
hacer las pruebas de funcionamiento mas rapido

## Características 🌟

- Registro de usuarios.
- Autenticación con usuario y contraseña.
- Generación y utilización del token.
- CRUD completo para cada entidad. 
- Vista de las consultas requeridas.
- Para cada controlador GET se estan manejando dos versiones :fire: - 
  - 1.0 -> Esta version NO incluye paginacion en los metodos GET 
  - 1.1 -> Esta Version SI incluye paginacion en los metodos GET  :white_check_mark:
## Uso 🕹

Una vez que el proyecto esté en marcha, puedes acceder a los diferentes endpoints que se describiran acontinuacion:<br>
  - Tener en cuenta que es muy probable que el puerto del localhost pueda cambiar :warning: - 

## 1. Registro de Usuarios
  :warning: - El genero del usuario en este caso estara representado en los siguientes valores:<br>
            :white_check_mark: - 1 Para Hombre<br>
            :white_check_mark: - 2 Para Mujer<br>
  ⚠️ - Solo una persona con rol de administrador puede realizar esta accion, para hacer el primer login de prueba debe hacerlo con
        el siguiente usuario en el siguiente EndPoint
  
        
        **Endpoint**: `http://localhost:5021/api/User/token`

        **Método**: `POST`
        
        **Payload**:
        
        json
        `{
            "Username": "Andres",
            "Password": "1234"
        }`
        
    - Nos data el siguiente resultado
          ```{
            "message": null,
            "isAuthenticated": true,
            "userName": "Andres",
            "email": "Andy@gmail.com",
            "roles": ["Administrator"],
            "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJBbmRyZXMiLCJqdGkiOiIwNjZmNmEwMi1iNjY3LTQ3ZmItOTBlZS00ZDYyY2E5NTBlNGYiLCJlbWFpbCI6IkFuZHlAZ21haWwuY29tIiwidWlkIjoiMSIsInJvbGVzIjoiQWRtaW5pc3RyYXRvciIsImV4cCI6MTY5NzczMjI0MCwiaXNzIjoiRW1pc29yVG9rZW4iLCJhdWQiOiJSZWNlcHRvclRva2VuIn0.EsIhcJT1PhlNI_CbSQ4vo0IyTsaUlsmUxmVb9wh0CrM",
            "refreshTokenExpiration": "2023-10-29T16:13:37.169476"
          }```
  - Luego Deberemos pegar el Token en la seccion de Auth de Insomnia seleccionando que es un Bearer Token y asi podremos continuar con la inserccion del nuevo
    usuario 
          **Endpoint**: `http://localhost:5021/api/User/register`
          
          **Método**: `POST`
          
          **Payload**:
          
          json
          `{
              "Name": "<nombre_del_usuario>",
              "Password": "<Password>",
              "Email": "<Email>",
              "Username": "<Nombre_usuario>",
              "Address": "<Direccion_del_usuario>",
              "Gender": "<Genero_del_usuario>"
          }`

Este endpoint permite a los usuarios registrarse en el sistema.

## 2. Generación del token:

    **Endpoint**: `http://localhost:5021/api/token`
    
    **Método**: `POST`
    
    **Payload**:
    
    `{
        "Nombre": "<nombre_de_usuario>",
        "password": "<password>"
    }`

Una vez registrado el usuario tendrá que ingresar para recibir un token, este será ingresado al siguiente Endpoint que es el de Refresh Token.
❗Recordar que debemos insertar el token en la seccion de Auth y solo debe estar vigente el token

## 3. Refresh Token:

    **Endpoint**: `http://localhost:5021/api/refresh-token`


Se dejan los mismos datos en el Body y luego se ingresa al "Auth", "Bearer", allí se ingresa el token obtenido en el anterior Endpoint.

**Otros Endpoints**

    Obtener Todos los Usuarios: GET `http://localhost:5021/api/user`
    
    Obtener Usuario por ID: GET `http://localhost:5021/api/user/{id}`
    
    Actualizar Usuario: PUT `http://localhost:5021/api/user/{id}`
    
    Eliminar Usuario: DELETE `http://localhost:5021/api/user/{id}`


## Desarrollo de los Endpoints requeridos⌨️

Cada Endpoint tiene su versión 1.0 y 1.1, al igual que están con y sin paginación.

Para consultar la versión 1.0 de todos se ingresa únicamente el Endpoint; para consultar la versión 1.1 se deben seguir los siguientes pasos: 

En el Thunder Client se va al apartado de "Headers" y se ingresa lo siguiente:

![image](https://github.com/SilviaJaimes/Proyecto-Veterinaria/assets/132016483/8044ee3d-76d9-4437-9f08-da8e5d7cff9a)

Para realizar la paginación se va al apartado de "Query" y se ingresa lo siguiente:

![image](https://github.com/SilviaJaimes/Proyecto-Veterinaria/assets/132016483/22683e46-037e-4f30-96b8-161df8622b40)


## 1. Visualizar los veterinarios cuya especialidad sea Cirujano vascular:

    **Endpoint**: `http://localhost:5021/api/veterinaria/veterinario/consulta1a`
    
    **Método**: `GET`


## 2. Listar los medicamentos que pertenezcan a el laboratorio Genfar:

**Endpoint**: `http://localhost:5021/api/veterinaria/laboratorio/consulta2A`

**Método**: `GET`


## 3. Mostrar las mascotas que se encuentren registradas cuya especie sea felina:

**Endpoint**: `http://localhost:5021/api/veterinaria/mascota/consulta3A`

**Método**: `GET`


## 4. Listar los propietarios y sus mascotas:

**Endpoint**: `http://localhost:5021/api/veterinaria/propietario/consulta4A`

**Método**: `GET`


## 5. Listar los medicamentos que tenga un precio de venta mayor a 50000:

**Endpoint**: `http://localhost:5021/api/veterinaria/medicamento/consulta5A`

**Método**: `GET`


## 6. Listar las mascotas que fueron atendidas por motivo de vacunacion en el primer trimestre del 2023:

**Endpoint**: `http://localhost:5021/api/veterinaria/mascota/consulta6A`

**Método**: `GET`


## 7. Listar todas las mascotas agrupadas por especie:

**Endpoint**: `http://localhost:5021/api/veterinaria/mascota/consulta1B`

**Método**: `GET`


## 8. Listar todos los movimientos de medicamentos y el valor total de cada movimiento:

**Endpoint**: `http://localhost:5021/api/veterinaria/movimientoMedicamento/consulta2B`

**Método**: `GET`


## 9. Listar las mascotas que fueron atendidas por un determinado veterinario:

**Endpoint**: `http://localhost:5021/api/veterinaria/mascota/consulta3B`

**Método**: `GET`

## 10. Listar los proveedores que me venden un determinado medicamento:

**Endpoint**: `http://localhost:5021/api/veterinaria/proveedor/consulta4B`

**Método**: `GET`

## 11. Listar las mascotas y sus propietarios cuya raza sea Golden Retriver:

**Endpoint**: `http://localhost:5021/api/veterinaria/propietario/consulta5B`

**Método**: `GET`

## 12. Listar la cantidad de mascotas que pertenecen a una raza:

**Endpoint**: `http://localhost:5021/api/veterinaria/mascota/consulta6B`

**Método**: `GET`

## Desarrollo ⌨️
Este proyecto utiliza varias tecnologías y patrones, incluidos:

Entity Framework Core para la ORM.
Patrón Repository y Unit of Work para la gestión de datos.
AutoMapper para el mapeo entre entidades y DTOs.

## Agradecimientos 🎁

A todas las librerías y herramientas utilizadas en este proyecto.

A ti, por considerar el uso de este sistema.

por Owen 🦝
