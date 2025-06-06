import React, { useState, useEffect } from "react";

/**
 * Componente que muestra un formulario para editar un proveedor existente.
 *
 * @param {Object} props
 * @param {boolean} props.isEditing - Controla la visibilidad del formulario.
 * @param {Function} props.setIsEditing - Función para cerrar el formulario.
 * @param {Object} props.selectedProvider - Proveedor a editar.
 */
export default function ProviderEditForm({ isEditing, setIsEditing, selectedProvider }) {
  const [formData, setFormData] = useState(selectedProvider || {});

  // ⚡ Actualiza los datos del formulario cuando se selecciona un proveedor nuevo
  useEffect(() => {
    if (selectedProvider) {
      setFormData(selectedProvider);
    }
  }, [selectedProvider]);

  /**
   * Maneja cambios en los campos del formulario.
   * @param {React.ChangeEvent<HTMLInputElement | HTMLSelectElement>} e
   */
  const handleChange = (e) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value,
    });
  };

  /**
   * Envía los datos actualizados al backend.
   * @param {React.FormEvent} e
   */
  const handleSubmit = (e) => {
    e.preventDefault();

    fetch(`http://localhost:5232/api/backend/`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(formData),
    })
      .then((res) => console.log(res))
      .then(() => {
        alert("✅ Proveedor actualizado correctamente.");
        setIsEditing(false);
        window.location.reload(); // 🔄 Recarga para reflejar cambios
      })
      .catch((err) =>
        alert("❌ Error al actualizar proveedor:", err)
      );
  };

  // ❌ Si no está en modo edición, no renderiza nada
  if (!isEditing) return null;

  return (
    <div style={{
      position: "fixed", top: "10%", left: "50%", transform: "translateX(-50%)",
      backgroundColor: "#fff", padding: "20px", borderRadius: "10px", boxShadow: "0px 2px 5px rgba(0,0,0,0.2)"
    }}>
      <h2>✏️ Editar Proveedor</h2>
      <form onSubmit={handleSubmit} style={{
        display: "grid", gridTemplateColumns: "1fr 1fr", gap: "15px", textAlign: "left"
      }}>
        <label>Razón Social:</label>
        <input type="text" name="socialName" value={formData.socialName || ""} onChange={handleChange} required />

        <label>Nombre Comercial:</label>
        <input type="text" name="commercialName" value={formData.commercialName || ""} onChange={handleChange} required />

        <label>Identificación Tributaria:</label>
        <input type="number" name="tributeID" value={formData.tributeID || ""} onChange={handleChange} required />

        <label>Teléfono:</label>
        <input type="tel" name="phoneNumber" value={formData.phoneNumber || ""} onChange={handleChange} required />

        <label>Correo Electrónico:</label>
        <input type="email" name="email" value={formData.email || ""} onChange={handleChange} required />

        <label>Sitio Web:</label>
        <input type="url" name="webPage" value={formData.webPage || ""} onChange={handleChange} />

        <label>Dirección:</label>
        <input type="text" name="address" value={formData.address || ""} onChange={handleChange} required />

        <label>País:</label>
        <select name="country" value={formData.country || ""} onChange={handleChange} required>
          <option value="">Selecciona un país...</option>
          <option value="Perú">Perú</option>
          <option value="México">México</option>
          <option value="Argentina">Argentina</option>
        </select>

        <label>Facturación Anual:</label>
        <input type="number" name="annualBilling" value={formData.annualBilling || ""} onChange={handleChange} required />

        <button type="submit" style={{
          backgroundColor: "#28a745", color: "white", padding: "10px", marginTop: "10px"
        }}>
          ✅ Guardar
        </button>

        <button type="button" onClick={() => setIsEditing(false)} style={{
          backgroundColor: "#dc3545", color: "white", padding: "10px", marginTop: "10px"
        }}>
          ❌ Cancelar
        </button>
      </form>
    </div>
  );
}
