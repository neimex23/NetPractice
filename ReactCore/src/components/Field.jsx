export default function Field({ label, children }) {
  return (
    <div className="flex flex-col gap-1.5">
      <label className="text-xs font-semibold text-neutral-500 uppercase tracking-wider">
        {label}
      </label>
      {children}
    </div>
  );
}

export function Input({ ...props }) {
  return (
    <input
      className="w-full px-3 py-2.5 border border-neutral-200 rounded-lg text-sm
        bg-neutral-50 focus:bg-white focus:border-emerald-500 focus:outline-none transition-colors"
      {...props}
    />
  );
}

export function Select({ children, ...props }) {
  return (
    <select
      className="w-full px-3 py-2.5 border border-neutral-200 rounded-lg text-sm
        bg-neutral-50 focus:bg-white focus:border-emerald-500 focus:outline-none transition-colors"
      {...props}
    >
      {children}
    </select>
  );
}
