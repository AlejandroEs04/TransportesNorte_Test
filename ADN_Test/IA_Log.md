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

### 4. Nueva página de registro de embarque
**Prompt/contexto:** Agregar una nueva página para crear un nuevo 
embarque, con selects (ComboBox) para Conductor, Tracto y Centro 
Operativo que muestren el nombre/placa pero pasen el id; botón 
"Registrar" en el dashboard y actualizar IA_Log.md.
**Resultado:** Se crearon:
- `Repositories/ICentroOperativoRepository.cs` y `CentroOperativoRepository.cs`
- `Services/ICentroOperativoService.cs` y `CentroOperativoService.cs`
- `ViewModels/CreateEmbarqueViewModel.cs`
- `Views/CreateEmbarqueWindow.xaml` y `CreateEmbarqueWindow.xaml.cs`
Se modificaron:
- `ViewModels/DashboardViewModel.cs` — agregado comando `NuevoEmbarqueCommand`
- `Views/DashboardView.xaml` — agregado botón "Nuevo Embarque"
- `IA_Log.md` — este registro

### 5. Fix doble clic en DataGrid del dashboard
**Prompt/contexto:** El doble clic en el DataGrid de la pantalla 
principal no abría el detalle del embarque. 
**Resultado:** El código-behind `DashboardView.xaml.cs` verificaba 
`dg.SelectedItem is Models.Embarque`, pero el DataGrid está enlazado a 
`ObservableCollection<EmbarqueResponseDto>` (namespace `Dtos`). Se 
corrigió el tipo en la comparación a `EmbarqueResponseDto`.

### 6. Fill Database
**Prompt/contexto:** Pregunté a la IA si podía darme los statements
para generar los empleados, camiones y las salidas pendientes.
**Resultado:** Me retorno los scripts necesarios para insertar los
registros.

### 7. Estructura MVVM con navegación entre pantallas
**Prompt/contexto:** Pregunté cómo estructurar el patrón MVVM en WPF
con .NET 10 soportando múltiples pantallas (Dashboard y Auditoría de
Pesaje), y cómo navegar entre ellas sin usar Frame ni NavigationService.
**Resultado:** La IA propuso el patrón ContentControl + DataTemplate:
MainViewModel expone una propiedad `CurrentViewModel`, y MainWindow
enlaza un ContentControl a ella. Cambiar el valor de `CurrentViewModel`
en C# provoca que WPF seleccione automáticamente el DataTemplate
correspondiente al tipo, intercambiando la vista visible. La navegación
entre ViewModels se implementó mediante callbacks (Action<T>) inyectados
en el constructor, evitando acoplamiento directo entre ViewModels.
Se generaron: `ViewModelBase.cs`, `RelayCommand.cs` (con versión
`AsyncRelayCommand` para operaciones async), `MainViewModel.cs`,
`DashboardViewModel.cs`, `AuditoriaPesajeViewModel.cs` (esqueleto),
`MainWindow.xaml`, `App.xaml` y `App.xaml.cs`.

## Notas
Todo el código generado fue revisado, probado y modificado manualmente 
antes de incluirse en el proyecto.