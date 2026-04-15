import { useState } from 'react';

export default function DataTable({ columns, data, onEdit, onDelete, searchable = true }) {
  const [search, setSearch] = useState('');

  const filtered = search
    ? data.filter(row =>
        Object.values(row).some(v =>
          String(v ?? '').toLowerCase().includes(search.toLowerCase())
        )
      )
    : data;

  return (
    <div className="bg-white border border-neutral-200 rounded-2xl overflow-hidden">
      {searchable && (
        <div className="px-5 py-3 border-b border-neutral-100">
          <input
            value={search}
            onChange={e => setSearch(e.target.value)}
            placeholder="Buscar..."
            className="w-full px-3 py-2 text-sm border border-neutral-200 rounded-lg
              bg-neutral-50 focus:bg-white focus:border-emerald-500 focus:outline-none transition-colors"
          />
        </div>
      )}

      <div className="overflow-x-auto">
        <table className="w-full">
          <thead>
            <tr className="border-b border-neutral-100">
              {columns.map(col => (
                <th key={col.key}
                  className="px-5 py-3 text-left text-[11px] font-semibold text-neutral-400 uppercase tracking-wider">
                  {col.label}
                </th>
              ))}
              <th className="px-5 py-3 text-right text-[11px] font-semibold text-neutral-400 uppercase tracking-wider">
                Acciones
              </th>
            </tr>
          </thead>
          <tbody>
            {filtered.length === 0 ? (
              <tr>
                <td colSpan={columns.length + 1} className="px-5 py-12 text-center text-neutral-400 text-sm">
                  No hay registros{search ? ` para "${search}"` : ''}
                </td>
              </tr>
            ) : (
              filtered.map(row => (
                <tr key={row.id} className="border-b border-neutral-50 hover:bg-neutral-50 transition-colors">
                  {columns.map(col => (
                    <td key={col.key} className="px-5 py-3.5 text-sm text-neutral-700">
                      {col.render ? col.render(row[col.key], row) : (row[col.key] ?? '—')}
                    </td>
                  ))}
                  <td className="px-5 py-3.5">
                    <div className="flex justify-end gap-2">
                      <button onClick={() => onEdit(row)}
                        className="px-3 py-1.5 text-xs border border-neutral-200 rounded-lg
                          hover:bg-neutral-100 transition-colors text-neutral-600">
                        Editar
                      </button>
                      <button onClick={() => onDelete(row.id)}
                        className="px-3 py-1.5 text-xs border border-red-200 rounded-lg
                          hover:bg-red-50 text-red-600 transition-colors">
                        Eliminar
                      </button>
                    </div>
                  </td>
                </tr>
              ))
            )}
          </tbody>
        </table>
      </div>
    </div>
  );
}
