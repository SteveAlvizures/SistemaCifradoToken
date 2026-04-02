# Manual de usuario

## SistemaCifradoToken

Este manual explica de forma sencilla cómo usar la aplicación web **SistemaCifradoToken**.

---

## 1. Pantalla principal

Al ingresar al sistema se muestra la pantalla principal. En esta pantalla se pueden ver:

- total de usuarios
- total de mensajes
- total de historial

También aparecen botones para entrar a las funciones principales del sistema:

- Registrar usuario
- Crear mensaje
- Ver mensajes
- Consultar token
- Ver historial completo
- Historial del propietario

---

## 2. Registrar usuario

Para registrar un usuario se deben seguir estos pasos:

1. Entrar a la opción **Registrar usuario**.
2. Escribir los datos solicitados:
   - Nombre
   - Correo
   - Nombre de usuario
   - Contraseña
3. Presionar el botón **Registrar**.
4. Si los datos son válidos, el usuario queda guardado en la base de datos.

---

## 3. Crear mensaje

Para crear un mensaje cifrado se deben seguir estos pasos:

1. Entrar a la opción **Crear mensaje**.
2. Ingresar:
   - Id del usuario propietario
   - Texto a cifrar
   - Etiqueta
3. Presionar el botón **Guardar mensaje**.
4. El sistema cifra el texto en el servidor.
5. El sistema genera un token único para ese mensaje.
6. El mensaje queda almacenado en la base de datos.

---

## 4. Ver mensajes

La opción **Ver mensajes** muestra una tabla con los mensajes almacenados.

En esta tabla se puede ver:

- Id del mensaje
- Id del usuario propietario
- texto cifrado
- token
- etiqueta
- estado
- fecha de creación

Desde esta lista también se puede:

- realizar eliminación lógica
- consultar el historial relacionado con el propietario del mensaje

---

## 5. Consultar token

Para consultar un mensaje mediante token se deben seguir estos pasos:

1. Entrar a la opción **Consultar token**.
2. Ingresar:
   - Id del usuario que consulta
   - Token
3. Presionar el botón **Consultar**.

### Si el token es válido
El sistema muestra:

- texto descifrado
- usuario propietario
- fecha y hora del intento

### Si el token no existe
El sistema muestra un mensaje de error.

### Si el mensaje fue eliminado lógicamente
El sistema informa que el mensaje ya no se encuentra disponible para consulta normal.

---

## 6. Eliminación lógica

La aplicación permite realizar eliminación lógica de mensajes.

Esto significa que:

- el mensaje no se borra físicamente de la base de datos
- solo cambia su estado a **Eliminado**
- ya no debe mostrarse normalmente en una consulta válida

Para hacerlo:

1. Entrar a **Ver mensajes**.
2. Buscar el mensaje deseado.
3. Presionar el botón **Eliminar lógico**.

---

## 7. Ver historial

La opción **Ver historial completo** permite observar todos los intentos registrados en el sistema.

En el historial se muestran datos como:

- Id del historial
- Id del mensaje
- Id del usuario que realizó la acción
- token ingresado
- resultado
- motivo
- fecha y hora

El historial puede contener:

- intentos exitosos
- intentos fallidos

---

## 8. Historial filtrado por propietario

La aplicación también permite ver el historial relacionado con un usuario propietario específico.

Esto ayuda a separar la información por responsable del mensaje.

---

## 9. Recomendaciones de uso

- Verificar que el Id del usuario sea correcto antes de crear un mensaje.
- Guardar el token generado, ya que será necesario para consultar el mensaje.
- No compartir tokens con personas no autorizadas.
- Revisar el historial para verificar intentos correctos e incorrectos.

---

## 10. Observaciones

La aplicación fue desarrollada para ejecutarse en un servidor local y conectarse a SQL Server.

La dirección utilizada para ejecutar el sistema en la máquina personal fue:

`https://localhost:7002/`
