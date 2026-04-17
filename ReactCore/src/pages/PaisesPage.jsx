import { useState, useEffect, useCallback } from 'react';
import { paisApi, confederacionApi, deporteApi } from '../api';
import DataTable from '../components/DataTable';
import Modal from '../components/Modal';
import PageHeader from '../components/PageHeader';

const EMPTY_FORM = {
  id: null,
  nombre: '',
  fechaFundacion: '',
  confederacionId: '',
  deporteId: '',
};

export default function PaisesPage({ showToast }) {
  const [paises, setPaises]               = useState([]);
  const [confederaciones, setConfs]       = useState([]);
  const [deportes, setDeportes]           = useState([]);
  const [loading, setLoading]             = useState(true);
  const [mode, setMode]                   = useState(null); // 'create' | 'edit' | null
  const [form, setForm]                   = useState(EMPTY_FORM);

  // --------- data loading ---------
  const load = useCallback(async () => {
    setLoading(true);
    try {
      const [p, c, d] = await Promise.all([
        paisApi.getAll(),
        confederacionApi.getAll(),
        deporteApi.getAll(),
      ]);
      setPaises(p ?? []);
      setConfs(c ?? []);
      setDeportes(d ?? []);
    } catch (err) {
      console.error(err);
      showToast?.('Error al cargar datos', 'error');
    } finally {
      setLoading(false);
    }
  }, [showToast]);

  useEffect(() => { load(); }, [load]);

  // --------- helpers ---------
  const toDateInput = (iso) => (iso ? String(iso).split('T')[0] : '');

  const updateField = (key) => (e) =>
    setForm((f) => ({ ...f, [key]: e.target.value }));

  // --------- modal open/close ---------
  const openCreate = () => {
    setForm(EMPTY_FORM);
    setMode('create');
  };

  const openEdit = (row) => {
    // log para confirmar qué llega
    console.log('[openEdit] row:', row);
    setForm({
      id: row.id ?? row.Id ?? null,
      nombre: row.nombre ?? '',
      fechaFundacion: toDateInput(row.fechaFundacion),
      confederacionId: row.confederacionId ?? '',
      deporteId: row.deporteId ?? '',
    });
    setMode('edit');
  };

  const closeModal = () => {
    setMode(null);
    setForm(EMPTY_FORM);
  };

  // --------- submit ---------
  const handleSubmit = async () => {
    if (!form.nombre.trim()) {
      showToast?.('El nombre es requerido', 'error');
      return;
    }
    if (!form.confederacionId) {
      showToast?.('Seleccioná una confederación', 'error');
      return;
    }
    if (!form.deporteId) {
      showToast?.('Seleccioná un deporte', 'error');
      return;
    }

    const payload = {
      id: form.id,
      nombre: form.nombre.trim(),
      fechaFundacion: form.fechaFundacion || null,
      confederacionId: form.confederacionId,
      deporteId: form.deporteId,
    };

    console.log(`[handleSubmit] mode=${mode}`, payload);

    try {
      if (mode === 'edit') {
        await paisApi.update(form.id, payload);
        showToast?.('País actualizado');
      } else {
        await paisApi.create(payload);
        showToast?.('País creado');
      }
      closeModal();
      await load();
    } catch (ex) {
      console.error(ex);
      showToast?.(`Error al guardar: ${ex.message}`, 'error');
    }
  };

  const handleDelete = async (id) => {
    if (!window.confirm('¿Eliminar este país?')) return;
    try {
      await paisApi.delete(id);
      showToast?.('País eliminado');
      await load();
    } catch (err) {
      console.error(err);
      showToast?.('Error al eliminar', 'error');
    }
  };

  // --------- lookup maps para la tabla ---------
  const confMap = Object.fromEntries(confederaciones.map((c) => [c.id, c.nombre]));
  const depMap  = Object.fromEntries(deportes.map((d) => [d.id, d.nombre]));

  const columns = [
    {
      key: 'nombre',
      label: 'Nombre',
      render: (v) => <span className="font-medium text-neutral-900">{v}</span>,
    },
    {
      key: 'fechaFundacion',
      label: 'Fundación',
      render: (v) =>
        v
          ? new Date(v).toLocaleDateString('es-UY', {
              year: 'numeric', month: 'short', day: 'numeric',
            })
          : '—',
    },
    {
      key: 'confederacionId',
      label: 'Confederación',
      render: (v) =>
        v ? (
          <span className="inline-flex px-2.5 py-0.5 rounded-full text-xs font-medium bg-blue-100 text-blue-800">
            {confMap[v] ?? v}
          </span>
        ) : (
          <span className="text-neutral-300">—</span>
        ),
    },
    {
      key: 'deporteId',
      label: 'Deporte',
      render: (v) =>
        v ? (
          <span className="inline-flex px-2.5 py-0.5 rounded-full text-xs font-medium bg-amber-100 text-amber-800">
            {depMap[v] ?? v}
          </span>
        ) : (
          <span className="text-neutral-300">—</span>
        ),
    },
  ];

  const statCards = [
    { label: 'Países',          value: paises.length,          icon: '🌍', bg: 'bg-emerald-50' },
    { label: 'Confederaciones', value: confederaciones.length, icon: '🏛',  bg: 'bg-blue-50' },
    { label: 'Deportes',        value: deportes.length,        icon: '⚽',  bg: 'bg-amber-50' },
  ];

  // --------- render ---------
  return (
    <div>
      <PageHeader
        title="Países"
        subtitle={`${paises.length} registros`}
        action={
          <button
            onClick={openCreate}
            className="px-4 py-2 bg-emerald-700 text-white text-sm font-medium rounded-xl hover:bg-emerald-800 transition-colors"
          >
            + Nuevo país
          </button>
        }
      />

      <div className="grid grid-cols-3 gap-4 mb-6">
        {statCards.map((s) => (
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
        <DataTable
          columns={columns}
          data={paises}
          onEdit={openEdit}
          onDelete={handleDelete}
        />
      )}

      {mode && (
        <Modal
          title={mode === 'edit' ? 'Editar país' : 'Nuevo país'}
          onClose={closeModal}
          onSubmit={handleSubmit}
          submitLabel={mode === 'edit' ? 'Guardar cambios' : 'Crear'}
        >
          <div className="space-y-4">
            <label className="block">
              <span className="block text-sm font-medium text-neutral-700 mb-1">Nombre</span>
              <input
                type="text"
                value={form.nombre}
                onChange={updateField('nombre')}
                placeholder="Nombre del país"
                autoFocus
                className="w-full border border-neutral-300 rounded-lg px-3 py-2 focus:outline-none focus:ring-2 focus:ring-emerald-600"
              />
            </label>

            <label className="block">
              <span className="block text-sm font-medium text-neutral-700 mb-1">Fecha de fundación</span>
              <input
                type="date"
                value={form.fechaFundacion}
                onChange={updateField('fechaFundacion')}
                className="w-full border border-neutral-300 rounded-lg px-3 py-2 focus:outline-none focus:ring-2 focus:ring-emerald-600"
              />
            </label>

            <label className="block">
              <span className="block text-sm font-medium text-neutral-700 mb-1">Confederación</span>
              <select
                value={form.confederacionId}
                onChange={updateField('confederacionId')}
                className="w-full border border-neutral-300 rounded-lg px-3 py-2 focus:outline-none focus:ring-2 focus:ring-emerald-600"
              >
                <option value="">Seleccionar...</option>
                {confederaciones.map((c) => (
                  <option key={c.id} value={c.id}>{c.nombre}</option>
                ))}
              </select>
            </label>

            <label className="block">
              <span className="block text-sm font-medium text-neutral-700 mb-1">Deporte</span>
              <select
                value={form.deporteId}
                onChange={updateField('deporteId')}
                className="w-full border border-neutral-300 rounded-lg px-3 py-2 focus:outline-none focus:ring-2 focus:ring-emerald-600"
              >
                <option value="">Seleccionar...</option>
                {deportes.map((d) => (
                  <option key={d.id} value={d.id}>{d.nombre}</option>
                ))}
              </select>
            </label>
          </div>
        </Modal>
      )}
    </div>
  );
}