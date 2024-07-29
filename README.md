# EntertainmentHub - Tercer Sprint
Proyecto hecho por Lizeth Consuelo Bañuelos Ruelas.


# Descripción
Entertainment Hub entretenimiento donde se pueden ver películas, programas de televisión, las próximas películas, las películas más valoradas y populares, también tiene un inicio de sesión simulado.

# Objetivos
- Implementar backend con .Net
- Desarrollar la base de datos 
# Dependencias y bibliotecas
.Net v8


# Captura de pantalla del proyecto
![imagen](https://github.com/user-attachments/assets/8bfbde55-cdbb-4f1f-ad92-2cff96742783)


# Instrucciones
Obtener la url del repositorio: Hacer clic en el botón code y copiar la URL del repositorio.
Clonar repositorio: abrir la terminal y ejecutar el comando git clone <urldelrepositorio>.
Instalar las dependencias necesarias, restaurarlas con `dotnet restore`, luego compilar el proyecto
con dotnet build y posteriormente ejecutarlo con dotnet run

# Descripción de como se hizo
Se comenzó con investigar sobre express.js ya que lo había utilizado un poco, después al darnos el cambio a .Net me di a la tarea de averiguar cómo se podría realizar y al darme cuenta de su parecido con SpringBoot comprendí un poco más desde mi punto de vista, después me dispuse a ver videos de manejor de CRUD en .Net para comprender y tratar de implementarlo y darme un ejemplo, sin embargo me di cuenta que se necesitaba algo más profundo para pdoer desarrollar mi proyecto. Una vez entendido un poco me dispuse a definir modelos, controllers, etc.
# Reporte de Code Coverage y de testing
##Code coverage
![alt text](image-11.png)

![alt text](image-12.png)

# Problemas conocidos
La base de datos, la conexión no se si es la correcta al hacerlo con Microsoft y no con credenciales.
Faltan controladores

# Retrospectiva

## ¿Qué hice bien?
Entender de manera básica .Net
Agregué una nueva funcionalidad a mi proyecto en el cual se puede agregar a favoritos en Angular.
Implementar controladores básicos como el de movies y funciones para toprated y upcoming movies desde la base de datos.

## ¿Qué no salió bien?
Tratar de hacer el backend en .Net lo que me llevó más tiempo para comprender como se debería de hacer.
Debido a que mi base de datos no me permitió conectar con password e id, lo tuve que hacer con autenticacion de Microsoft lo cual considero no es correcto y debería preguntarlo.
Me faltó implementar el login con base de datos.

## ¿Qué puedo hacer diferente?
Administrar mejor mi tiempo al querer aprender algo diferente, ya que esta vez me enfoqué en entender y no en avanzar y aplicarlo.
Aprender más a fondo sobre .Net
# Database diagram
![imagen](https://github.com/user-attachments/assets/2ad4c12b-ad72-45d9-9f7e-4d66ed9dcbf8)
