import { useHashLocation, navigate } from 'wouter/use-hash-location';

const navItems = [
  { path: '/paises',          label: 'Países',         icon: '🌍' },
  { path: '/confederaciones', label: 'Confederaciones', icon: '🏛' },
  { path: '/deportes',        label: 'Deportes',        icon: '⚽' },
];

export default function Sidebar() {
  const [location] = useHashLocation();

  return (
    <aside className="w-56 shrink-0 bg-neutral-900 text-neutral-100 flex flex-col min-h-screen px-4 py-7 gap-8">
      {/* Logo */}
      <div className="px-2">
        <p className="font-serif text-xl leading-tight text-white">NetPractice</p>
        <p className="text-[10px] tracking-widest text-neutral-500 uppercase mt-1">Sports Manager</p>
      </div>

      {/* Nav */}
      <nav className="flex flex-col gap-1">
        {navItems.map(({ path, label, icon }) => {
          const active = location === path || location.startsWith(path + '/');
          return (
            <button
              key={path}
              onClick={() => navigate(path)}
              className={`flex items-center gap-3 px-3 py-2.5 rounded-lg text-sm transition-all w-full text-left
                ${active
                  ? 'bg-emerald-700 text-white font-medium'
                  : 'text-neutral-400 hover:bg-neutral-800 hover:text-white'}`}
            >
              <span className="text-base">{icon}</span>
              {label}
            </button>
          );
        })}
      </nav>

      <div className="mt-auto text-[11px] text-neutral-600 px-2">
        API REST · ASP.NET Core
      </div>
    </aside>
  );
}