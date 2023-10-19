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

## Especificacion de todos los Endpoints requeridos en metodo POSt⌨️

  ###Tabla Breed :

    **Endpoint**: `http://localhost:5021/api/Breed`
    {
    	"Name":"Esponja"
    }
  ###Tabla TypeMovement :

    **Endpoint**: `http://localhost:5021/api/typemovement`
    {
    	"Name":"Fianza"
    }
  ###Tabla Specie :

    **Endpoint**: `http://localhost:5021/api/Specie`
    {
    	"Name":"Raro"
    }
  ###Tabla Speciality :

    **Endpoint**: `http://localhost:5021/api/SpecialityControllers`
    {
    	"Name":"Reanimador"
    }
  ###Tabla Gender :

    **Endpoint**: `http://localhost:5021/api/Gender`
    {
    	"Name":"No Definido"
    }
  ###Tabla Laboratory :

    **Endpoint**: `http://localhost:5021/api/Laboratory`
    {
    	"Name":"Genfares",
    	"Address":"calle aa con bbb",
    	"Phone":"123456"
    }
  ###Tabla Medicine :

    **Endpoint**: `http://localhost:5021/api/Medicine`
    {
    	"Name":"Terramicina",
    	"QuantityAvalible":50,
    	"Price":10.99,
    	"LaboratoryIdFk":1
    }
  ###Tabla Partner :

    **Endpoint**: `http://localhost:5021/api/Partner`
    {	
      "Name":"Carlos camilo",
    	"Email":"calos@a.com",
    	"Phone":"3005669",
    	"Address":"calle re falsa 123",
    	"SpecialtyIdFk":11,
    	"GenderIdFk":1,
    	"PartnerTypeIdFk":2
    }
  ###Tabla Pet :

    **Endpoint**: `http://localhost:5021/api/Pet`
    {
      "Name":"Carlos camilo pet",
      "DateBirth":"2022-01-15",
      "UserOwnerId":14,
      "SpeciesIdFk":2,
      "BreedIdFk":1
    }
  ###Tabla Quote :

    **Endpoint**: `http://localhost:5021/api/Quote`
    {
    	"Hour":"15:30:00",
    	"Date":"2022-01-15",
    	"Reason":"asmndfbksafdlkusahdf",
    	"PetIdFk":2,
    	"VeterinarianIdFk":1
    }
  ###Tabla MedicinePartner :

    **Endpoint**: `http://localhost:5021/api/MedicinePartner`
    {
    	"MedicineIdFk":18,
    	"PartnerIdFk":4
    }
  ###Tabla TypeMovement :

    **Endpoint**: `http://localhost:5021/api/typemovement`
    {
    	"Name":"Fianza"
    }
  ###Tabla TypeMovement :

    **Endpoint**: `http://localhost:5021/api/typemovement`
    {
    	"Name":"Fianza"
    }
  ###Tabla TypeMovement :

    **Endpoint**: `http://localhost:5021/api/typemovement`
    {
    	"Name":"Fianza"
    }

## Desarrollo de los Endpoints requeridos⌨️

Cada Endpoint tiene su versión 1.0 y 1.1, al igual que están con y sin paginación.

Para consultar la versión 1.0 de todos se ingresa únicamente el Endpoint; para consultar la versión 1.1 se deben seguir los siguientes pasos: 

En el Thunder Client se va al apartado de "Headers" y se ingresa lo siguiente:

![image](https://github.com/SilviaJaimes/Proyecto-Veterinaria/assets/132016483/8044ee3d-76d9-4437-9f08-da8e5d7cff9a)

Para realizar la paginación se va al apartado de "Query" y se ingresa lo siguiente:

![image](https://github.com/SilviaJaimes/Proyecto-Veterinaria/assets/132016483/22683e46-037e-4f30-96b8-161df8622b40)


## 1. Visualizar los veterinarios cuya especialidad sea Cirujano vascular:

  **Endpoint**: `http://localhost:5021/api/Partner/consulta1a`
  
  **Método**: `GET` 
<br>--------  Version 1.0  -----------------------------  version 1.1 con paginacion<br>
![image](https://github.com/AndresAngarita10/AdminVeterinary/assets/106509898/58af3abe-b12a-4756-b824-aa03bd96c0d3)
![image](https://github.com/AndresAngarita10/AdminVeterinary/assets/106509898/1e8204b1-6908-4fe4-b223-ed7e62569d14)





## 2. Listar los medicamentos que pertenezcan a el laboratorio Genfar:

**Endpoint**: `http://localhost:5021/api/Medicine/consulta2a`

**Método**: `GET` <br>
--------  Version 1.0  -----------------------------  version 1.1 con paginacion<br>
![image](https://github.com/AndresAngarita10/AdminVeterinary/assets/106509898/5dc34578-5f9d-44e6-8189-853349a6039a)
![image](https://github.com/AndresAngarita10/AdminVeterinary/assets/106509898/b17eaf6c-2930-4550-b5c4-a79c11790d26)




## 3. Mostrar las mascotas que se encuentren registradas cuya especie sea felina:

**Endpoint**: `http://localhost:5021/api/Pet/consulta3a`

**Método**: `GET` <br>
--------  Version 1.0  -----------------------------  version 1.1 con paginacion<br>
![image](https://github.com/AndresAngarita10/AdminVeterinary/assets/106509898/53ae08cc-ac11-42f0-aeda-d2eeea32c903)
![image](https://github.com/AndresAngarita10/AdminVeterinary/assets/106509898/35c22e83-a0d8-4011-8cc1-715cc1782022)


## 4. Listar los propietarios y sus mascotas:

**Endpoint**: `http://localhost:5021/api/Partner/consulta4a`

**Método**: `GET`<br>
--------  Version 1.0  -----------------------------  version 1.1 con paginacion<br>
![image](https://github.com/AndresAngarita10/AdminVeterinary/assets/106509898/cdee3ef6-6a88-4dcb-ab87-59d18cccc7aa)
![image](https://github.com/AndresAngarita10/AdminVeterinary/assets/106509898/0a04d082-d60c-42ab-b7ab-45c8b801d985)


## 5. Listar los medicamentos que tenga un precio de venta mayor a 10 Usd:

**Endpoint**: `http://localhost:5021/api/Medicine/consulta5a`

**Método**: `GET`<br>
--------  Version 1.0  -----------------------------  version 1.1 con paginacion<br>
![image](https://github.com/AndresAngarita10/AdminVeterinary/assets/106509898/d250d5a6-f994-4a3e-93ca-b576ee2c40d8)
![image](https://github.com/AndresAngarita10/AdminVeterinary/assets/106509898/5e6d3aeb-d542-48ab-9843-ae5d39a19768)


## 6. Listar las mascotas que fueron atendidas por motivo de vacunacion en el primer trimestre del 2023:

**Endpoint**: `http://localhost:5021/api/Pet/consulta6a`

**Método**: `GET`<br>
![image](https://github.com/AndresAngarita10/AdminVeterinary/assets/106509898/b55ccb55-c67c-4725-bf47-9f34152dcad5)
![image](https://github.com/AndresAngarita10/AdminVeterinary/assets/106509898/b15747ce-2226-4008-8e3d-e8614656a53b)


## 7. Listar todas las mascotas agrupadas por especie:

**Endpoint**: `http://localhost:5021/api/Pet/consulta1b`

**Método**: `GET`<br>
--------  Version 1.0  -----------------------------  version 1.1 con paginacion<br>
![image](https://github.com/AndresAngarita10/AdminVeterinary/assets/106509898/d78758d7-e67b-4bd9-afd2-a7d2c2f8275c)
![image](https://github.com/AndresAngarita10/AdminVeterinary/assets/106509898/6eb4298e-6e74-428c-88ba-54219897205a)



## 8. Listar todos los movimientos de medicamentos y el valor total de cada movimiento:

**Endpoint**: `http://localhost:5021/api/MedicineMovement/consulta2b`

**Método**: `GET`<br>
--------  Version 1.0  -----------------------------  version 1.1 con paginacion<br>
![image](https://github.com/AndresAngarita10/AdminVeterinary/assets/106509898/754fd469-49c3-42cc-a540-06fbfed231df)
![image](https://github.com/AndresAngarita10/AdminVeterinary/assets/106509898/598b5d47-d137-49f8-a47f-93cfc8bbea44)


## 9. Listar las mascotas que fueron atendidas por un determinado veterinario: 
En esta consulta se busca por el nombre del veterinario, en el ejemplo se especifica uno en concreto que ya deberia existir

**Endpoint**: `http://localhost:5021/api/Partner/consulta3b/veterinario 2`

**Método**: `GET`<br>
--------  Version 1.0  -----------------------------  version 1.1 con paginacion<br>
![image](https://github.com/AndresAngarita10/AdminVeterinary/assets/106509898/80c42b77-641f-4f1b-9931-42e132a319e9)
![image](https://github.com/AndresAngarita10/AdminVeterinary/assets/106509898/32b087fd-18d4-440e-9358-9ed9ce312880)


## 10. Listar los proveedores que me venden un determinado medicamento:
En esta consulta se busca por el nombre de la medicina la cual se da una en el ejemplo llanmada medicina 5
**Endpoint**: `http://localhost:5021/api/Partner/consulta4b/medicina 5`

**Método**: `GET`<br>
--------  Version 1.0  -----------------------------  version 1.1 con paginacion<br>
![image](https://github.com/AndresAngarita10/AdminVeterinary/assets/106509898/c5e41b4b-6ced-4dea-9aed-6536ccb76cef)
![image](https://github.com/AndresAngarita10/AdminVeterinary/assets/106509898/0858603d-de2d-417c-9bc3-94dc9f0ce65a)


## 11. Listar las mascotas y sus propietarios cuya raza sea Golden Retriver:

**Endpoint**: `http://localhost:5021/api/Pet/Consulta5b`

**Método**: `GET`<br>
--------  Version 1.0  -----------------------------  version 1.1 con paginacion<br>
![image](https://github.com/AndresAngarita10/AdminVeterinary/assets/106509898/c10a3fc5-6c71-4de3-aff3-7bf8493fc564)
![image](https://github.com/AndresAngarita10/AdminVeterinary/assets/106509898/f2b50b2d-c085-4881-bfe7-d1ec0a5ee59f)



## 12. Listar la cantidad de mascotas que pertenecen a una raza:

**Endpoint**: `http://localhost:5021/api/veterinaria/mascota/consulta6B`

**Método**: `GET`<br>
--------  Version 1.0  -----------------------------  version 1.1 con paginacion<br>
![image](https://github.com/AndresAngarita10/AdminVeterinary/assets/106509898/320fa722-60b1-4505-a6a9-5c8f8c80a994)
![image](https://github.com/AndresAngarita10/AdminVeterinary/assets/106509898/b366422a-6684-4f91-be23-57d35cf53663)


## Desarrollo ⌨️
Este proyecto utiliza varias tecnologías y patrones, incluidos:

Entity Framework Core para la ORM.
Patrón Repository y Unit of Work para la gestión de datos.
AutoMapper para el mapeo entre entidades y DTOs.

## Agradecimientos 🎁

A todas las librerías y herramientas utilizadas en este proyecto.

Estamos encantados de que hayas decidido utilizar este sistema.

