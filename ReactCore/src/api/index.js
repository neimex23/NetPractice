const BASE = 'https://localhost:7250/api';

async function request(path, opts = {}) {
  const res = await fetch(BASE + path, {
    headers: { 'Content-Type': 'application/json' },
    ...opts,
  });
  if (!res.ok) throw new Error(`HTTP ${res.status}`);
  if (res.status === 204) return null;
  return res.json();
}

function withId(data, id) {
  return JSON.stringify({ ...data, id, Id: id });
}

// --- Confederacion ---
export const confederacionApi = {
  getAll:   ()        => request('/confederacion'),
  getById:  (id)      => request(`/confederacion/${id}`),
  search:   (q)       => request(`/confederacion/search?search=${encodeURIComponent(q)}`),
  create:   (data)    => request('/confederacion', { method: 'POST', body: JSON.stringify(data) }),
  update:   (id, data)=> request(`/confederacion/${id}`, { method: 'PUT', body: withId(data, id) }),
  delete:   (id)      => request(`/confederacion/${id}`, { method: 'DELETE' }),
};

// --- Deporte ---
export const deporteApi = {
  getAll:   ()        => request('/deporte'),
  getById:  (id)      => request(`/deporte/${id}`),
  search:   (q)       => request(`/deporte/search?search=${encodeURIComponent(q)}`),
  create:   (data)    => request('/deporte', { method: 'POST', body: JSON.stringify(data) }),
  update:   (id, data)=> request(`/deporte/${id}`, { method: 'PUT', body: withId(data, id) }),
  delete:   (id)      => request(`/deporte/${id}`, { method: 'DELETE' }),
};

// --- Pais ---
export const paisApi = {
  getAll:   ()        => request('/pais'),
  getById:  (id)      => request(`/pais/${id}`),
  search:   (q)       => request(`/pais/search?search=${encodeURIComponent(q)}`),
  create:   (data)    => request('/pais', { method: 'POST', body: JSON.stringify(data) }),
  update:   (id,data)=> request(`/pais/${id}`, { method: 'PUT', body: withId(data, id) }),
  delete:   (id)      => request(`/pais/${id}`, { method: 'DELETE' }),
};

async function request(path, opts = {}) {
  const res = await fetch(BASE + path, {
    headers: { 'Content-Type': 'application/json' },
    ...opts,
  });

  const text = await res.text();

  if (!res.ok) {
    console.error("ERROR BACK:", text);
    throw new Error(`HTTP ${res.status} - ${text}`);
  }

  if (res.status === 204) return null;
  return text ? JSON.parse(text) : null;
}