import { useState, useEffect } from 'react';
import { deporteApi } from '../api';
import DataTable from '../components/DataTable';
import Modal from '../components/Modal';
import Field, { Input } from '../components/Field';
import PageHeader from '../components/PageHeader';

const EMPTY_FORM = { nombre: '' };

export default function DeportesPage({ showToast }) {
  const [data, setData]       = useState([]);
  const [loading, setLoading] = useState(true);
  const [modal, setModal]     = useState(null);
  const [editing, setEditing] = useState(null);
  const [form, setForm]       = useState(EMPTY_FORM);

  const load = async () => {
    setLoading(true);
    try {
      setData(await deporteApi.getAll());
    } catch {
      showToast('Error al cargar deportes', 'error');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => { load(); }, []);

  const openCreate = () => { setForm(EMPTY_FORM); setEditing(null); setModal('create'); };
  const openEdit   = (row) => { setForm({ nombre: row.nombre }); setEditing(row); setModal('edit'); };
  const closeModal = () => { setModal(null); setEditing(null); };

  const handleSubmit = async () => {
    if (!form.nombre.trim()) { showToast('El nombre es requerido', 'error'); return; }
    try {
      if (modal === 'edit') {
        await deporteApi.update(editing.id, form);
        showToast('Deporte actualizado');
      } else {
        await deporteApi.create(form);
        showToast('Deporte creado');
      }
      closeModal();
      load();
    } catch {
      showToast('Error al guardar', 'error');
    }
  };

  const handleDelete = async (id) => {
    if (!window.confirm('¿Eliminar este deporte?')) return;
    try {
      await deporteApi.delete(id);
      showToast('Deporte eliminado');
      load();
    } catch {
      showToast('Error al eliminar', 'error');
    }
  };

  const columns = [
    { key: 'nombre', label: 'Nombre' },
    { key: 'id',     label: 'ID',
      render: v => <span className="font-mono text-xs text-neutral-400">{v}</span> },
  ];

  return (
    <div>
      <PageHeader
        title="Deportes"
        subtitle={`${data.length} registros`}
        action={
          <button onClick={openCreate}
            className="px-4 py-2 bg-emerald-700 text-white text-sm font-medium rounded-xl
              hover:bg-emerald-800 transition-colors">
            + Nuevo deporte
          </button>
        }
      />

      {loading ? (
        <div className="text-sm text-neutral-400 py-8">Cargando...</div>
      ) : (
        <DataTable columns={columns} data={data} onEdit={openEdit} onDelete={handleDelete} />
      )}

      {modal && (
        <Modal
          title={modal === 'edit' ? 'Editar deporte' : 'Nuevo deporte'}
          onClose={closeModal}
          onSubmit={handleSubmit}
          submitLabel={modal === 'edit' ? 'Guardar cambios' : 'Crear'}
        >
          <Field label="Nombre">
            <Input
              value={form.nombre}
              onChange={e => setForm({ ...form, nombre: e.target.value })}
              placeholder="Nombre del deporte"
              autoFocus
            />
          </Field>
        </Modal>
      )}
    </div>
  );
}
