import React, { useEffect, useState } from "react";

/**
 * Componente CrossingTable
 * Muestra coincidencias de un proveedor seleccionado con la lista negra del Banco Mundial.
 *
 * @param {Object} props
 * @param {Object} props.selectedProvider - Proveedor actualmente seleccionado
 * @param {boolean} props.isCrossing - Indica si debe mostrarse la tabla de coincidencias
 */
export default function CrossingTable({ selectedProvider, isCrossing }) {
  const [error, setError] = useState(null); // Mensaje de error (si falla el fetch)
  const [matchs, setMatchs] = useState([]); // Resultados obtenidos desde el backend
  const [selectedRow, setSelectedRow] = useState(null); // ID de fila seleccionada

  /**
   * Llama al backend (FastAPI) para obtener coincidencias del proveedor seleccionado
   */
  const fetchMatchs = async () => {
    try {
      const res = await fetch(`http://localhost:8000/search/worldbank?name=${selectedProvider?.commercialName}`);
      if (!res.ok) throw new Error(`Error ${res.status}: No se pudo obtener coincidencias`);

      const data = await res.json();
      setMatchs(data);
    } catch (err) {
      setError(`❌ ${err.message}`);
    }
  };

  // Se ejecuta cada vez que cambia el proveedor seleccionado
  useEffect(() => {
    if (selectedProvider?.commercialName) {
      fetchMatchs();
    }
  }, [selectedProvider]);

  // Si el modo "cruce" no está activado, no renderiza nada
  if (!isCrossing) return null;

  // Mostrar error si ocurrió
  if (error) return <p style={{ color: "red" }}>{error}</p>;

  // Mostrar mensaje si no hay coincidencias
  if (matchs.length === 0) return <p>No hay coincidencias</p>;

  // Renderizado de tabla con resultados
  return (
    <div style={{ display: "flex", justifyContent: "center", marginTop: "60px" }}>
      <table style={{ borderCollapse: "collapse", width: "90%", border: "1px solid #ddd" }}>
        <thead>
          <tr>
            <th style={{ padding: "10px" }}>Nombre de Firma</th>
            <th style={{ padding: "10px" }}>Código</th>
            <th style={{ padding: "10px" }}>Dirección</th>
            <th style={{ padding: "10px" }}>País</th>
            <th style={{ padding: "10px" }}>Inicio Inhabilitación</th>
            <th style={{ padding: "10px" }}>Final Inhabilitación</th>
            <th style={{ padding: "10px" }}>Motivos</th>
          </tr>
        </thead>
        <tbody>
          {matchs.map((p) => (
            <tr
              key={`${p.FIRM_NAME}-${p.CODE}+${p.ADDRESS}`}
              onClick={() => setSelectedRow(p.id)}
              style={{
                borderBottom: "1px solid #ddd",
                backgroundColor: selectedRow === p.id ? "#d0e6ff" : "#fff",
                cursor: "pointer",
              }}
            >
              <td style={{ padding: "10px" }}>{p.FIRM_NAME}</td>
              <td style={{ padding: "10px" }}>{p.CODE}</td>
              <td style={{ padding: "10px" }}>{p.ADDRESS}</td>
              <td style={{ padding: "10px" }}>{p.COUNTRY}</td>
              <td style={{ padding: "10px" }}>{p.FROM_DATE}</td>
              <td style={{ padding: "10px" }}>{p.TO_DATE}</td>
              <td style={{ padding: "10px" }}>{p.GROUNDS}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
