const API_BASE = 'http://localhost:5000/api';

const api = {
  async get(path) {
    const r = await fetch(API_BASE + path);
    if (!r.ok) throw new Error(await r.text());
    return r.json();
  },
  async post(path, body) {
    const r = await fetch(API_BASE + path, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(body)
    });
    if (!r.ok) throw new Error(await r.text());
    return r.json();
  },
  async put(path, body) {
    const r = await fetch(API_BASE + path, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(body)
    });
    if (!r.ok) throw new Error(await r.text());
    return r.status === 204 ? null : r.json().catch(() => null);
  },
  async patch(path, body) {
    const r = await fetch(API_BASE + path, {
      method: 'PATCH',
      headers: { 'Content-Type': 'application/json' },
      body: body !== undefined ? JSON.stringify(body) : undefined
    });
    if (!r.ok) throw new Error(await r.text());
    return r.status === 204 ? null : r.json().catch(() => null);
  },
  async delete(path) {
    const r = await fetch(API_BASE + path, { method: 'DELETE' });
    if (!r.ok) throw new Error(await r.text());
    return true;
  }
};

function getCurrentUser() {
  const u = localStorage.getItem('fc_user');
  return u ? JSON.parse(u) : null;
}
function getRoleId() { const u = getCurrentUser(); return u ? u.roleId : null; }
function isAdmin()   { return getRoleId() === 1; }
function isTrainer() { return getRoleId() === 2; }
function isClient()  { return getRoleId() === 3; }
