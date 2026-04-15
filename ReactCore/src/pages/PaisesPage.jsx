import { useState, useEffect } from 'react';
import { paisApi, confederacionApi, deporteApi } from '../api';
import DataTable from '../components/DataTable';
import Modal from '../components/Modal';
import Field, { Input, Select } from '../components/Field';
import PageHeader from '../components/PageHeader';

const EMPTY_FORM = {
  nombre: '',
  fechaFundacion: '',
  confederacionId: '',
  deporteId: '',
};

export default function PaisesPage({ showToast }) {
  const [data, setData]               = useState([]);
  const [confederaciones, setConf]    = useState([]);
  const [deportes, setDep]            = useState([]);
  const [loading, setLoading]         = useState(true);
  const [modal, setModal]             = useState(null);
  const [editing, setEditing]         = useState(null);
  const [form, setForm]               = useState(EMPTY_FORM);

  const load = async () => {
    setLoading(true);
    try {
      const [p, c, d] = await Promise.all([
        paisApi.getAll(),
        confederacionApi.getAll(),
        deporteApi.getAll(),
      ]);
      setData(p);
      setConf(c);
      setDep(d);
    } catch {
      showToast('Error al cargar datos', 'error');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => { load(); }, []);

  const openCreate = () => { setForm(EMPTY_FORM); setEditing(null); setModal('create'); };
  const openEdit   = (row) => {
    setForm({
      nombre: row.nombre ?? '',
      fechaFundacion: row.fechaFundacion ? row.fechaFundacion.split('T')[0] : '',
      confederacionId: row.confederacionId ?? '',
      deporteId: row.deporteId ?? '',
    });
    setEditing(row);
    setModal('edit');
  };
  const closeModal = () => { setModal(null); setEditing(null); };

  const set = (key) => (e) => setForm(f => ({ ...f, [key]: e.target.value }));

  const handleSubmit = async () => {
    if (!form.nombre.trim()) { showToast('El nombre es requerido', 'error'); return; }
    try {
      const payload = {
        id: editing?.id,
        nombre: form.nombre,
        fechaFundacion: form.fechaFundacion || null,
        confederacionId: form.confederacionId || null,
        deporteId: form.deporteId || null,
      };
      if (modal === 'edit') {
        payload.Id = editing.id; 
        console.log('Payload para actualización:', payload);
        await paisApi.update(payload.id, payload);
        showToast('País actualizado');
      } else {
        await paisApi.create(payload);
        showToast('País creado');
      }
      closeModal();
      load();
    } catch (ex) {
      showToast(`Error al guardar ${ex.message}`, 'error');
    }
  };

  const handleDelete = async (id) => {
    if (!window.confirm('¿Eliminar este país?')) return;
    try {
      await paisApi.delete(id);
      showToast('País eliminado');
      load();
    } catch {
      showToast('Error al eliminar', 'error');
    }
  };

  const confMap = Object.fromEntries(confederaciones.map(c => [c.id, c.nombre]));
  const depMap  = Object.fromEntries(deportes.map(d => [d.id, d.nombre]));

  const columns = [
    { key: 'nombre', label: 'Nombre',
      render: v => <span className="font-medium text-neutral-900">{v}</span> },
    { key: 'fechaFundacion', label: 'Fundación',
      render: v => v ? new Date(v).toLocaleDateString('es-UY', { year: 'numeric', month: 'short', day: 'numeric' }) : '—' },
    { key: 'confederacionId', label: 'Confederación',
      render: v => v
        ? <span className="inline-flex px-2.5 py-0.5 rounded-full text-xs font-medium bg-blue-100 text-blue-800">{confMap[v] ?? v}</span>
        : <span className="text-neutral-300">—</span> },
    { key: 'deporteId', label: 'Deporte',
      render: v => v
        ? <span className="inline-flex px-2.5 py-0.5 rounded-full text-xs font-medium bg-amber-100 text-amber-800">{depMap[v] ?? v}</span>
        : <span className="text-neutral-300">—</span> },
  ];

  const statCards = [
    { label: 'Países',          value: data.length,               icon: '🌍', bg: 'bg-emerald-50' },
    { label: 'Confederaciones', value: confederaciones.length,    icon: '🏛',  bg: 'bg-blue-50' },
    { label: 'Deportes',        value: deportes.length,           icon: '⚽',  bg: 'bg-amber-50' },
  ];

  return (
    <div>
      <PageHeader
        title="Países"
        subtitle={`${data.length} registros`}
        action={
          <button onClick={openCreate}
            className="px-4 py-2 bg-emerald-700 text-white text-sm font-medium rounded-xl
              hover:bg-emerald-800 transition-colors">
            + Nuevo país
          </button>
        }
      />

      {/* Stat cards */}
      <div className="grid grid-cols-3 gap-4 mb-6">
        {statCards.map(s => (
          <div key={s.label} className={`${s.bg} rounded-2xl p-4 flex items-center gap-4`}>
            <span className="text-2xl">{s.icon}</span>
            <div>
              <p className="text-xs text-neutral-500 uppercase tracking-wide">{s.label}</p>
              <p className="text-2xl font-serif text-neutral-900">{s.value}</p>
            </div>
          </div>
        ))}
      </div>

      {loading ? (
        <div className="text-sm text-neutral-400 py-8">Cargando...</div>
      ) : (
        <DataTable columns={columns} data={data} onEdit={openEdit} onDelete={handleDelete} />
      )}

      {modal && (
        <Modal
          title={modal === 'edit' ? 'Editar país' : 'Nuevo país'}
          onClose={closeModal}
          onSubmit={handleSubmit}
          submitLabel={modal === 'edit' ? 'Guardar cambios' : 'Crear'}
        >
          <Field label="Nombre">
            <Input value={form.nombre} onChange={set('nombre')} placeholder="Nombre del país" autoFocus />
          </Field>
          <Field label="Fecha de fundación">
            <Input type="date" value={form.fechaFundacion} onChange={set('fechaFundacion')} />
          </Field>
          <Field label="Confederación">
            <Select value={form.confederacionId} onChange={set('confederacionId')}>
              <option value="">Seleccionar...</option>
              {confederaciones.map(c => (
                <option key={c.id} value={c.id}>{c.nombre}</option>
              ))}
            </Select>
          </Field>
          <Field label="Deporte">
            <Select value={form.deporteId} onChange={set('deporteId')}>
              <option value="">Seleccionar...</option>
              {deportes.map(d => (
                <option key={d.id} value={d.id}>{d.nombre}</option>
              ))}
            </Select>
          </Field>
        </Modal>
      )}
    </div>
  );
}
