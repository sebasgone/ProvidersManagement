import React, { useState, useEffect } from 'react'
import axios from 'axios'
import ProviderTable from './ProviderTable'
import ProviderForm  from './ProviderForm'

export default function ProviderList() {
  const [providers, setProviders] = useState([])
  const [selectedId, setSelectedId] = useState(null)
  const [formVisible, setFormVisible] = useState(false)
  const [editInitial, setEditInitial] = useState(null)

  // 1. Cargar lista al montar
  useEffect(() => {
    axios.get('/api/provider')
      .then(({ data }) => setProviders(data))
      .catch(console.error)
  }, [])

  // 2. Función de crear o actualizar
  const handleSave = async prov => {
    try {
      if (prov.id) {
        await axios.put(`/api/provider/${prov.id}`, prov)
        setProviders(ps =>
          ps.map(p => (p.id === prov.id ? prov : p))
        )
      } else {
        const { data } = await axios.post('/api/provider', prov)
        setProviders(ps => [...ps, data])
      }
    } catch (e) {
      console.error(e)
      alert('Error al guardar')
    } finally {
      setFormVisible(false)
      setEditInitial(null)
    }
  }

  // 3. Función de borrado
  const handleDelete = async id => {
    if (!window.confirm('¿Eliminar este proveedor?')) return
    try {
      await axios.delete(`/api/provider/${id}`)
      setProviders(ps => ps.filter(p => p.id !== id))
      if (selectedId === id) setSelectedId(null)
    } catch {
      alert('Error al borrar')
    }
  }

  return (
    <div className="p-4 space-y-4">
      <button
        onClick={() => setFormVisible(true)}
        className="bg-blue-500 text-white px-4 py-2 rounded"
      >
        Nuevo Proveedor
      </button>

      <ProviderTable
        providers={providers}
        selectedId={selectedId}
        onSelect={setSelectedId}
        onEdit={prov => { setEditInitial(prov); setFormVisible(true) }}
        onDelete={handleDelete}
      />

      <ProviderForm
        visible={formVisible}
        initial={editInitial}
        onCancel={() => { setFormVisible(false); setEditInitial(null) }}
        onSave={handleSave}
      />
    </div>
  )
}
