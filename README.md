# retotecnico-cobol
# Introducción:
Breve descripción del reto y su propósito.
el reto consiste en crear una aplicación de consola en el cual pueda leer un archivo csv donde estan todos los movimientos de un trabajador  y asi permitir calcular su estado de cuenta , resumen de sus transacciones  y  la transaccion con mayor monto 
# Instrucciones de Ejecución:

ejecutar en el powershell 
1.- cd ReporteTransacciones
2.- donet build 
3.- donet run
4.- el sistema nos pedira el nombre del archivo : ingresar "transacciones.csv" -- verificar el separador de comas en windows 11 con español peru (se pone ";")

# Enfoque y Solución:
Explica de forma concisa la lógica implementada y las decisiones de diseño que tomaste.
se creo una clase segun los campos del archivo,  tambien se creo un metodo en el cual permita leer el archivo csv que devuelve el listado de transacciones que se encuentra en el archivo. en el "main" se procedio hacer la logica de negocio ya que el calculo es bien pequeño se maneja ahi y no en un metodo aparte.
se aplico programacion orientada objetos, para tener mapeado todos los atributos 
# Estructura del Proyecto:
ReporteTransacciones.csjproj --> es el archivo del proyecto en el cual debe ubicarse y ahi proceder ejecutar los comandos dotnet
|---Program.cs -->  ahi se pone la logica de leer el csv y hacer los calculos para mostrar via consola al usuario.
    |-- ReporteTransacciones.csv --> el archivo donde esta la información de las transacciones 
