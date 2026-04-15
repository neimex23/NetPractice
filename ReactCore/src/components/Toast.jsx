export default function Toast({ toast }) {
  if (!toast) return null;

  const bg = toast.type === 'error' ? 'bg-red-600' : 'bg-neutral-900';

  return (
    <div
      className={`fixed bottom-6 right-6 z-50 ${bg} text-white px-5 py-3 rounded-xl text-sm shadow-xl
        animate-[slideUp_0.2s_ease] flex items-center gap-2`}
    >
      {toast.type === 'error' ? '✕' : '✓'} {toast.message}
    </div>
  );
}
