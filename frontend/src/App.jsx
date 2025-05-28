import { useState } from "react";
import ProviderTable from "./components/ProviderTable";
import ProviderForm from "./components/ProviderForm";
import ProviderEditForm from "./components/ProviderEditForm";
import CrossingTable from "./components/CrossingTable";

/**
 * Componente principal de la aplicación de gestión de proveedores.
 * Muestra una tabla de proveedores, permite crear, editar, eliminar y hacer screening cruzado.
 */
export default function App() {
  // Estado de búsqueda (input del usuario)
  const [search, setSearch] = useState("");

  // Controla la visibilidad del formulario para crear proveedor
  const [isVisible, setIsVisible] = useState(false);

  // Proveedor actualmente seleccionado por el usuario
  const [selectedProvider, setSelectedProvider] = useState(null);

  // Estado de edición (formulario de edición)
  const [isEditing, setIsEditing] = useState(false);

  // Modo de cruce de datos (screening con lista negra externa)
  const [isCrossing, setIsCrossing] = useState(false);

  return (
    <div style={{ fontFamily: "Arial, sans-serif", textAlign: "center", width: "100%", padding: "20px" }}>
      
      {/* Título dinámico según el modo activo */}
      <h1 style={{ display: !isCrossing ? "block" : "none", fontSize: "24px", marginBottom: "20px" }}>
        🔍 Gestión de Proveedores
      </h1>

      <h1 style={{ display: isCrossing ? "block" : "none", fontSize: "24px", marginBottom: "20px" }}>
        🔍 Coincidencias Banco Mundial
      </h1>

      {/* Input de búsqueda de proveedores */}
      <div style={{ display: !isCrossing ? "block" : "none", marginBottom: "20px" }}>
        <input
          type="text"
          placeholder="Buscar proveedor..."
          value={search}
          onChange={(e) => setSearch(e.target.value)}
          style={{ padding: "8px", width: "250px", borderRadius: "5px", border: "1px solid #ccc" }}
        />
      </div>

      {/* Tabla principal de proveedores */}
      <ProviderTable search={search} setSelectedProvider={setSelectedProvider} isCrossing={isCrossing} />

      {/* Botones de acción (Crear, Editar, Eliminar, Screening) */}
      <div style={{ display: "flex", justifyContent: "space-evenly", alignItems: "center", width: "100%" }}>
        
        <button
          onClick={() => setIsVisible(true)}
          style={{
            flexGrow: 1,
            padding: "10px 15px",
            backgroundColor: "#007bff",
            color: "white",
            border: "none",
            borderRadius: "5px",
            cursor: "pointer",
            display: !isCrossing ? "block" : "none"
          }}
        >
          ➕ Nuevo
        </button>

        <button
          onClick={() => {
            if (!selectedProvider) {
              alert("Seleccione un proveedor primero.");
              return;
            }
            setIsEditing(true);
          }}
          style={{
            flexGrow: 1,
            padding: "10px 15px",
            backgroundColor: "#28a745",
            color: "white",
            borderRadius: "5px",
            display: !isCrossing ? "block" : "none"
          }}
        >
          🔄 Actualizar
        </button>

        <button
          onClick={() => {
            if (!selectedProvider) {
              alert("Seleccione un proveedor primero.");
              return;
            }

            fetch(`http://localhost:5232/api/backend/delete/${selectedProvider.id}`, {
              method: "DELETE",
              headers: { "Content-Type": "application/json" },
            })
              .then((res) => console.log(res))
              .then(() => {
                alert("✅ Proveedor eliminado correctamente.");
                window.location.reload();
              })
              .catch((err) => alert("❌ Error al eliminar proveedor:", err));
          }}
          style={{
            flexGrow: 1,
            padding: "10px 15px",
            margin: "5px",
            backgroundColor: "#dc3545",
            color: "white",
            border: "none",
            borderRadius: "5px",
            cursor: "pointer",
            display: !isCrossing ? "block" : "none"
          }}
        >
          ❌ Eliminar
        </button>

        <button
          onClick={() => {
            if (!selectedProvider) {
              alert("Seleccione un proveedor primero.");
              return;
            }
            setIsCrossing(true);
          }}
          style={{
            display: !isCrossing ? "block" : "none",
            flexGrow: 1,
            padding: "10px 15px",
            backgroundColor: "#28a745",
            color: "white",
            borderRadius: "5px"
          }}
        >
          🛠️ Screening
        </button>
      </div>

      {/* Formulario para crear nuevo proveedor */}
      <ProviderForm isVisible={isVisible} setIsVisible={setIsVisible} />

      {/* Formulario para editar proveedor existente */}
      <ProviderEditForm
        isEditing={isEditing}
        setIsEditing={setIsEditing}
        selectedProvider={selectedProvider}
      />

      {/* Tabla de resultados cruzados */}
      <CrossingTable
        selectedProvider={selectedProvider}
        isCrossing={isCrossing}
      />
    </div>
  );
}
