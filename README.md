# GestorColas
Gestor de colas que permite ejecutar procesos .exe
Estado Gestión de Colas
 Id (autonumérico)
 IdClase - necesario para IFASE_SAP 
 IdOrigen - necesario para IFASE_SAP
 IdSociedad - necesario para IFASE_SAP
 IdDocumentoInterno - necesario para IFASE_SAP
 IdTipoEjecucionSAP - necesario para IFASE_SAP
 IdProceso - - Valor asignado por quién inserta el registro para seguimiento del trabajo
 Descripcion - Descripción del trabajo,
 Estado - Estado del trabajo (Ver posibles valores en el siguiente apartado),
 FechaInsercion - Fecha de cuando se inserta el registro,
 FechaInicio - Fecha de cuando se ejecutará el trabajo,
 FechaFinalizacion - Fecha de cuando ha finalizado el trabajo,
 IdCola - Identificador de la cola por donde se ejecutará el trabajo,
 ParametrosEx - Parametro de ejecución para trabajos que no sean IFASE_SAP (En este caso, todos los parámetros de IFASE_SAP han de estar sin informar)

 
Valores posibles del campo EstadoProceso de la tabla TrabajosBatches

     0 - Pendiente -> 			 Pendiente de procesar.
     1 - EnEjecucion -> 		 Ejecutandose
     2 - FinalizadoSinErrores -> Finalizado correcto
     3 - FinalizadoConErrores -> Finalizado con ERRORES (No ha podido ejecutar el proceso)
     4 - Abortado -> 			(Time out, parametrizable a nivel de cola en el config) 
									o
								Finalización erronea de proceso que ejecuta la cola.
  
  
  Crear cola:
  En el confing existe la sección <gestorColas>
  En ella se crearán tantas colas como se requieran. En nuestro caso tenemos suficiente con una.
  El nombre de la cola es indistinto. Se ha de tener en cuenta en el momento de insertar un nuevo trabajo en la tabla TrabajosBatches en el campo IdCola
  
  - path: Ubicación donde se encuentra el proceso a ejecutar
  - ejecutable: Nombre del ejecutable que se ejecutará al entrar el trabajo por la cola.
  - timeOut: Tiempo en milisegundos que el trabajo estará en ejecución. Por defecto 100000 (100 segundos)
   <gestorColas>
    <colas>
	  <cola nombreCola="IFASE_SAP" path="\\sintzv\BAPI_SAPFI\GestionwIfaseSap" ejecutable="GestionIfaseSap.exe" />
      <cola nombreCola="NOTEPAD" path="C:\Program Files (x86)\Notepad++" ejecutable="cmd.exe" timeOut="10000" />
      <cola nombreCola="NOTEPAD2" path="C:\Program Files (x86)\Notepad++" ejecutable="cmd.exe" timeOut="10000" />     
    </colas>
  </gestorColas>
