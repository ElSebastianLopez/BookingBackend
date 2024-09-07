Documentación de las Rutas de los Controladores
CustomersController
GET /api/Customers/GetAllCustomer
Descripción: Obtiene una lista de todos los clientes.

URL: /api/Customers/GetAllCustomer
Method: GET
Authorization: Requerida (Bearer Token)
Response:
200 OK: Devuelve una lista de clientes.
400 Bad Request: Error en la base de datos.
500 Internal Server Error: Error en el servidor.
GET /api/Customers/GetByIdService/{id}
Descripción: Obtiene un cliente por su ID.

URL: /api/Customers/GetByIdService/{id}
Method: GET
Authorization: Requerida (Bearer Token)
Response:
200 OK: Detalles del cliente.
400 Bad Request: Error en la base de datos.
500 Internal Server Error: Error en el servidor.
LoginController
GET /api/Login/Login/{email}/{password}
Descripción: Autentica a un usuario con su correo electrónico y contraseña.

URL: /api/Login/Login/{email}/{password}
Method: GET
Authorization: No requerida.
Response:
200 OK: Información del usuario autenticado.
400 Bad Request: Error en la base de datos.
500 Internal Server Error: Error en el servidor.
ReservationsController
GET /api/Reservations/GetAllReservationByIdCustomer
Descripción: Obtiene todas las reservas del cliente autenticado.

URL: /api/Reservations/GetAllReservationByIdCustomer
Method: GET
Authorization: Requerida (Bearer Token)
Response:
200 OK: Lista de reservas.
400 Bad Request: Error en la base de datos.
500 Internal Server Error: Error en el servidor.
GET /api/Reservations/GetByIdReservation/{id}
Descripción: Obtiene una reserva por su ID.

URL: /api/Reservations/GetByIdReservation/{id}
Method: GET
Authorization: Requerida (Bearer Token)
Response:
200 OK: Detalles de la reserva.
400 Bad Request: Error en la base de datos.
500 Internal Server Error: Error en el servidor.
PATCH /api/Reservations/AddOrEditReservation
Descripción: Añade o edita una reserva.

URL: /api/Reservations/AddOrEditReservation
Method: PATCH
Authorization: Requerida (Bearer Token)
Response:
200 OK: Reserva añadida o editada.
400 Bad Request: Error en la base de datos.
500 Internal Server Error: Error en el servidor.
DELETE /api/Reservations/DeleteReservation/{id}
Descripción: Elimina una reserva por su ID.

URL: /api/Reservations/DeleteReservation/{id}
Method: DELETE
Authorization: Requerida (Bearer Token)
Response:
200 OK: Reserva eliminada.
400 Bad Request: Error en la base de datos.
500 Internal Server Error: Error en el servidor.
DELETE /api/Reservations/DeleteReservationDet/{idReservation}/{idReservationDet}
Descripción: Elimina un detalle de una reserva.

URL: /api/Reservations/DeleteReservationDet/{idReservation}/{idReservationDet}
Method: DELETE
Authorization: Requerida (Bearer Token)
Response:
200 OK: Detalle de reserva eliminado.
400 Bad Request: Error en la base de datos.
500 Internal Server Error: Error en el servidor.
GET /api/Reservations/CancelReservation/{idReservation}
Descripción: Cancela una reserva por su ID.

URL: /api/Reservations/CancelReservation/{idReservation}
Method: GET
Authorization: Requerida (Bearer Token)
Response:
200 OK: Reserva cancelada.
400 Bad Request: Error en la base de datos.
500 Internal Server Error: Error en el servidor.
GET /api/Reservations/BuyReservation/{idReservation}
Descripción: Compra una reserva por su ID.

URL: /api/Reservations/BuyReservation/{idReservation}
Method: GET
Authorization: Requerida (Bearer Token)
Response:
200 OK: Reserva comprada.
400 Bad Request: Error en la base de datos.
500 Internal Server Error: Error en el servidor.
ServicesController
GET /api/Services/GetAllService
Descripción: Obtiene una lista de todos los servicios disponibles.

URL: /api/Services/GetAllService
Method: GET
Authorization: Requerida (Bearer Token)
Response:
200 OK: Lista de servicios.
400 Bad Request: Error en la base de datos.
500 Internal Server Error: Error en el servidor.
GET /api/Services/GetByIdService/{id}
Descripción: Obtiene un servicio específico por su ID.

URL: /api/Services/GetByIdService/{id}
Method: GET
Authorization: Requerida (Bearer Token)
Response:
200 OK: Detalles del servicio.
400 Bad Request: Error en la base de datos.
500 Internal Server Error: Error en el servidor.
