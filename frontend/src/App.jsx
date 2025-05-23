import { useState } from "react";
import ProviderTable from "./components/ProviderTable";
import ProviderForm from "./components/ProviderForm";
import ProviderEditForm from "./components/ProviderEditForm";


export default function App() {
  const [search, setSearch] = useState("");
  const [isVisible, setIsVisible] = useState(false); // Estado que controla el formulario
  const [selectedProvider, setSelectedProvider] = useState(null);// Estado de proveedor seleccionado
  const [isEditing, setIsEditing] = useState(false);// Estado de edición
  

  return (
    <div style={{ fontFamily: "Arial, sans-serif", textAlign: "center", width: "100%", padding: "20px" }}>
      <h1 style={{ fontSize: "24px", marginBottom: "20px" }}>🔍 Gestión de Proveedores</h1>

      {/* Buscador */}
      <div style={{ marginBottom: "20px" }}>
        <input
          type="text"
          placeholder="Buscar proveedor..."
          value={search}
          onChange={(e) => setSearch(e.target.value)}
          style={{ padding: "8px", width: "250px", borderRadius: "5px", border: "1px solid #ccc" }}
        />
      </div>

      {/* Tabla de proveedores */}
      <ProviderTable search={search} setSelectedProvider={setSelectedProvider} />

      {/* Botones de acción */}
      <div style={{ marginTop: "20px" }}>
        <button
          onClick={() => setIsVisible(true)} // 🔹 Activa el formulario
          style={{
            padding: "10px 15px",
            backgroundColor: "#007bff",
            color: "white",
            border: "none",
            borderRadius: "5px",
            cursor: "pointer",
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
        console.log("Proveedor seleccionado:", selectedProvider); // 🔹 Verifica si el estado existe
        setIsEditing(true);
        }}
        style={{ padding: "10px 15px", backgroundColor: "#28a745", color: "white", borderRadius: "5px" }}
        >
          🔄 Actualizar
        </button>

        <button style={{ padding: "10px 15px", margin: "5px", backgroundColor: "#dc3545", color: "white", border: "none", borderRadius: "5px", cursor: "pointer" }}>
          ❌ Eliminar
        </button>
      </div>

      {/* Formulario de proveedor */}
      <ProviderForm isVisible={isVisible} setIsVisible={setIsVisible} />

      <ProviderEditForm 
        isEditing={isEditing} 
        setIsEditing={setIsEditing} 
        selectedProvider={selectedProvider} 
      />
    </div>
  );
}
