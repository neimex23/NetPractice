export default function Modal({ title, onClose, onSubmit, submitLabel = 'Guardar', children }) {
  return (
    <div className="fixed inset-0 z-40 bg-black/40 backdrop-blur-sm flex items-center justify-center p-4"
      onClick={(e) => e.target === e.currentTarget && onClose()}>
      <div className="bg-white rounded-2xl w-full max-w-md shadow-2xl overflow-hidden">

        {/* Header */}
        <div className="flex items-center justify-between px-6 py-4 border-b border-neutral-100">
          <h2 className="font-medium text-neutral-900">{title}</h2>
          <button onClick={onClose}
            className="text-neutral-400 hover:text-neutral-700 text-lg leading-none transition-colors">
            ✕
          </button>
        </div>

        {/* Body */}
        <div className="px-6 py-5 flex flex-col gap-4">
          {children}
        </div>

        {/* Footer */}
        <div className="flex justify-end gap-2 px-6 py-4 border-t border-neutral-100 bg-neutral-50">
          <button onClick={onClose}
            className="px-4 py-2 text-sm rounded-lg border border-neutral-200 text-neutral-600 hover:bg-neutral-100 transition-colors">
            Cancelar
          </button>
          <button onClick={onSubmit}
            className="px-4 py-2 text-sm rounded-lg bg-emerald-700 text-white font-medium hover:bg-emerald-800 transition-colors">
            {submitLabel}
          </button>
        </div>
      </div>
    </div>
  );
}
