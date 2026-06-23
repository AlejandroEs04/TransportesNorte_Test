# IA_Log.md – Registro de uso de IA

## Herramientas utilizadas
- OpenCode

## Casos de uso

### 1. Diseño del Stored Procedure
**Prompt/contexto:** Pedí ayuda para estructurar el control transaccional 
(BEGIN TRAN/COMMIT/ROLLBACK) en el SP de actualización de despacho.
**Resultado:** La IA propuso una estructura base con TRY/CATCH + 
XACT_ABORT; la adapté a los nombres de columna reales y agregué 
validación del parámetro @Peso_Bascula_Salida.

### 2. Binding de cálculo en tiempo real (WPF)
**Prompt/contexto:** Consulté cómo disparar el recálculo de 
Peso_Neto_Real al cambiar el TextBox sin perder el patrón MVVM.
**Resultado:** Usé la sugerencia de recalcular en el setter de la 
propiedad PesoBasculaSalida y exponer PesoNetoReal como propiedad 
calculada con OnPropertyChanged encadenado.

### 3. Manejo de excepciones de conexión SQL
**Prompt/contexto:** Pedí ejemplos de captura de SqlException para 
diferenciar errores de conexión de otros errores de BD.
**Resultado:** Adapté el catch para mostrar MessageBox amigable y 
loguear el detalle técnico en log.txt.

## Notas
Todo el código generado fue revisado, probado y modificado manualmente 
antes de incluirse en el proyecto.