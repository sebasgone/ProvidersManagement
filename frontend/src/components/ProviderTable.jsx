import React, { useEffect, useState } from "react";

export default function ProviderTable({ search, setSelectedProvider, isCrossing }) {
  const [providers, setProviders] = useState([]);
  const [error, setError] = useState(null);
  const [selectedRow, setSelectedRow] = useState(null); // 🔹 Estado para manejar la fila seleccionada

  
  const fetchProviders = async () => {
      try {
        const res = await fetch("http://localhost:5232/api/backend/fetchall"); // 🔹 Espera la respuesta
        if (!res.ok) throw new Error(`Error ${res.status}: No se pudo obtener proveedores`);
        
        const data = await res.json(); // 🔹 Espera la conversión a JSON
        setProviders(data);
      } catch (err) {
        setError(`❌ ${err.message}`);
      }
    };
  
  useEffect(() => {
    fetchProviders(); // 🔹 Llamar a la función asincrónica
  }, []);

  if (isCrossing) return null;
  if (error) return <p style={{ color: "red" }}>{error}</p>;
  if (providers.length === 0) return <p>Cargando proveedores...</p>;

  return (
    <div style={{ overflowX: "auto", display: "flex", justifyContent: "center", marginTop: "60px" }}>
      <table style={{ width: "90vw", tableLayout: "fixed", borderCollapse: "collapse", border: "1px solid #ddd" }}>
        <thead style={{ position: "sticky", top: 0, backgroundColor: "white", zIndex: 10 }}>
          <tr>
            <th style={{ padding: "10px" }}>ID</th>
            <th style={{ padding: "10px" }}>Razón Social</th>
            <th style={{ padding: "10px" }}>Nombre Comercial</th>
            <th style={{ padding: "10px" }}>Identificación Tributaria</th>
            <th style={{ padding: "10px" }}>Teléfono</th>
            <th style={{ padding: "10px" }}>Correo Electrónico</th>
            <th style={{ padding: "10px" }}>Sitio Web</th>
            <th style={{ padding: "10px" }}>Dirección</th>
            <th style={{ padding: "10px" }}>País</th>
            <th style={{ padding: "10px" }}>Facturación Anual</th>
            <th style={{ padding: "10px" }}>Última Edición</th>
          </tr>
        </thead>
        <tbody>
          {providers
            .filter((p) => p.commercialName.toLowerCase().includes(search.toLowerCase()))
            .map((p) => (
              <tr 
                key={p.id} 
                onClick={() => { 
                  setSelectedProvider(p);
                  setSelectedRow(p.id); // 🔹 Guarda el ID de la fila seleccionada
                }}
                style={{
                  borderBottom: "1px solid #ddd",
                  backgroundColor: selectedRow === p.id ? "#d0e6ff" : "#fff", // 🔹 Solo marca la fila activa
                  cursor: "pointer",
                }}
              >
                <td style={{ padding: "10px", wordWrap: "break-word" }}>{p.id}</td>
                <td style={{ padding: "10px", wordWrap: "break-word" }}>{p.socialName}</td>
                <td style={{ padding: "10px", wordWrap: "break-word" }}>{p.commercialName}</td>
                <td style={{ padding: "10px", wordWrap: "break-word" }}>{p.tributeID}</td>
                <td style={{ padding: "10px", wordWrap: "break-word" }}>{p.phoneNumber}</td>
                <td style={{ padding: "10px", wordWrap: "break-word" }}>{p.email}</td>
                <td style={{ padding: "10px", wordWrap: "break-word" }}>
                  <a href={p.webPage} target="_blank" style={{ color: "#007bff", textDecoration: "none" }}>
                    {p.webPage}
                  </a>
                </td>
                <td style={{ padding: "10px", wordWrap: "break-word" }}>{p.address}</td>
                <td style={{ padding: "10px", wordWrap: "break-word" }}>{p.country}</td>
                <td style={{ padding: "10px", wordWrap: "break-word" }}>{p.annualBilling}</td>
                <td style={{ padding: "10px", wordWrap: "break-word" }}>{new Date(p.lastEdited).toLocaleDateString()}</td>
              </tr>
            ))}
        </tbody>
      </table>
    </div>
  );
}
