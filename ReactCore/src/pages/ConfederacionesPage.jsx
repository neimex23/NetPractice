import { useState, useEffect } from 'react';
import { confederacionApi } from '../api';
import DataTable from '../components/DataTable';
import Modal from '../components/Modal';
import Field, { Input } from '../components/Field';
import PageHeader from '../components/PageHeader';

const EMPTY_FORM = { nombre: '' };

export default function ConfederacionesPage({ showToast }) {
  const [data, setData]       = useState([]);
  const [loading, setLoading] = useState(true);
  const [modal, setModal]     = useState(null); // null | 'create' | 'edit'
  const [editing, setEditing] = useState(null);
  const [form, setForm]       = useState(EMPTY_FORM);

  const load = async () => {
    setLoading(true);
    try {
      setData(await confederacionApi.getAll());
    } catch {
      showToast('Error al cargar confederaciones', 'error');
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
        await confederacionApi.update(editing.id, form);
        showToast('Confederación actualizada');
      } else {
        await confederacionApi.create(form);
        showToast('Confederación creada');
      }
      closeModal();
      load();
    } catch {
      showToast('Error al guardar', 'error');
    }
  };

  const handleDelete = async (id) => {
    if (!window.confirm('¿Eliminar esta confederación?')) return;
    try {
      await confederacionApi.delete(id);
      showToast('Confederación eliminada');
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
        title="Confederaciones"
        subtitle={`${data.length} registros`}
        action={
          <button onClick={openCreate}
            className="px-4 py-2 bg-emerald-700 text-white text-sm font-medium rounded-xl
              hover:bg-emerald-800 transition-colors">
            + Nueva confederación
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
          title={modal === 'edit' ? 'Editar confederación' : 'Nueva confederación'}
          onClose={closeModal}
          onSubmit={handleSubmit}
          submitLabel={modal === 'edit' ? 'Guardar cambios' : 'Crear'}
        >
          <Field label="Nombre">
            <Input
              value={form.nombre}
              onChange={e => setForm({ ...form, nombre: e.target.value })}
              placeholder="Nombre de la confederación"
              autoFocus
            />
          </Field>
        </Modal>
      )}
    </div>
  );
}
