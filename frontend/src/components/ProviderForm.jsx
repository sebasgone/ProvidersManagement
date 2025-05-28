import React, { useState } from "react";

/**
 * Componente de formulario para registrar un nuevo proveedor.
 *
 * @param {Object} props
 * @param {boolean} props.isVisible - Define si el formulario debe mostrarse.
 * @param {Function} props.setIsVisible - Función para cerrar el formulario.
 */
export default function ProviderForm({ isVisible, setIsVisible }) {
  const [formData, setFormData] = useState({
    socialName: "",
    commercialName: "",
    tributeID: "",
    phoneNumber: "",
    email: "",
    webPage: "",
    address: "",
    country: "",
    annualBilling: 0,
    lastEdited: new Date().toISOString(), // Fecha actual ISO
  });

  /**
   * Maneja los cambios de los campos del formulario.
   * @param {React.ChangeEvent<HTMLInputElement | HTMLSelectElement>} e
   */
  const handleChange = (e) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value,
    });
  };

  /**
   * Envía los datos al backend para registrar un nuevo proveedor.
   * Muestra alertas según el resultado.
   * @param {React.FormEvent<HTMLFormElement>} e
   */
  const handleSubmit = (e) => {
    e.preventDefault();

    fetch("http://localhost:5232/api/backend", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(formData),
    })
      .then(async (res) => {
        if (!res.ok) {
          const errorData = await res.json();
          throw new Error(errorData.message || `Código de error ${res.status}`);
        }
        return res.json();
      })
      .then(() => {
        alert("✅ Proveedor agregado correctamente.");
        setIsVisible(false);
        window.location.reload();
      })
      .catch((err) => alert("❌ Error al guardar proveedor:", err));
  };

  if (!isVisible) return null;

  return (
    <div style={{
      position: "fixed", top: "10%", left: "50%", transform: "translateX(-50%)",
      backgroundColor: "#fff", padding: "20px", borderRadius: "10px", boxShadow: "0px 2px 5px rgba(0,0,0,0.2)"
    }}>
      <h2>📋 Agregar Proveedor</h2>
      <form onSubmit={handleSubmit} style={{
        display: "grid", gridTemplateColumns: "1fr 1fr", gap: "15px", textAlign: "left"
      }}>
        <label>Razón Social:</label>
        <input type="text" name="socialName" value={formData.socialName} onChange={handleChange} required />

        <label>Nombre Comercial:</label>
        <input type="text" name="commercialName" value={formData.commercialName} onChange={handleChange} required />

        <label>Identificación Tributaria:</label>
        <input type="text" name="tributeID" value={formData.tributeID} minLength="11" onChange={handleChange} required />

        <label>Teléfono:</label>
        <input type="tel" name="phoneNumber" value={formData.phoneNumber} onChange={handleChange} required />

        <label>Correo Electrónico:</label>
        <input type="email" name="email" value={formData.email} onChange={handleChange} required />

        <label>Sitio Web:</label>
        <input type="url" name="webPage" value={formData.webPage} onChange={handleChange} />

        <label>Dirección:</label>
        <input type="text" name="address" value={formData.address} onChange={handleChange} required />

        <label>País:</label>
        <select name="country" value={formData.country} onChange={handleChange} required>
          <option value="">Selecciona un país...</option>
          <option value="Perú">Perú</option>
          <option value="México">México</option>
          <option value="Argentina">Argentina</option>
        </select>

        <label>Facturación Anual:</label>
        <input type="number" name="annualBilling" value={formData.annualBilling} onChange={handleChange} required />

        <button type="submit" style={{ backgroundColor: "#28a745", color: "white", padding: "10px", marginTop: "10px" }}>
          ✅ Guardar
        </button>
        <button type="button" onClick={() => setIsVisible(false)} style={{ backgroundColor: "#dc3545", color: "white", padding: "10px", marginTop: "10px" }}>
          ❌ Cancelar
        </button>
      </form>
    </div>
  );
}
