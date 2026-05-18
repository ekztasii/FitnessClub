// ══════════════════════════════════════════════════════════════════════════
// ⚠️  ВАЖНО: укажите порт вашего ASP.NET API
//    Запустите Visual Studio (F5) и посмотрите в консоли:
//    "Now listening on: http://localhost:XXXX"
//    Вставьте этот порт ниже:
// ══════════════════════════════════════════════════════════════════════════
const API_BASE = 'http://localhost:5000/api';

// ── Универсальные fetch-обёртки ───────────────────────────────────────────
const api = {
  get: (url) =>
    fetch(`${API_BASE}${url}`)
      .then(r => r.ok ? r.json() : Promise.reject(r)),

  post: (url, body) =>
    fetch(`${API_BASE}${url}`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(body)
    }).then(r => r.ok ? r.json() : Promise.reject(r)),

  put: (url, body) =>
    fetch(`${API_BASE}${url}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(body)
    }).then(r => { if (!r.ok) return Promise.reject(r); return r.status === 204 ? null : r.json(); }),

  patch: (url, body) =>
    fetch(`${API_BASE}${url}`, {
      method: 'PATCH',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(body)
    }).then(r => { if (!r.ok) return Promise.reject(r); return null; }),

  delete: (url) =>
    fetch(`${API_BASE}${url}`, { method: 'DELETE' })
      .then(r => { if (!r.ok) return Promise.reject(r); return null; })
};

// ── Toast-уведомления ─────────────────────────────────────────────────────
function showToast(message, type = 'success') {
  const container = document.getElementById('toastContainer');
  if (!container) return;
  const id = 'toast_' + Date.now();
  const icons = { success: '✓', danger: '✕', warning: '⚠', info: 'ℹ' };
  container.insertAdjacentHTML('beforeend', `
    <div id="${id}" class="toast align-items-center text-bg-${type} border-0 mb-2" role="alert">
      <div class="d-flex">
        <div class="toast-body fw-semibold">${icons[type] || ''} ${message}</div>
        <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast"></button>
      </div>
    </div>`);
  const el = document.getElementById(id);
  new bootstrap.Toast(el, { delay: 3500 }).show();
  el.addEventListener('hidden.bs.toast', () => el.remove());
}

// ── Подтверждение удаления ────────────────────────────────────────────────
function confirmDelete(message, onConfirm) {
  document.getElementById('deleteModalMessage').textContent = message;
  document.getElementById('confirmDeleteBtn').onclick = () => {
    bootstrap.Modal.getInstance(document.getElementById('deleteModal')).hide();
    onConfirm();
  };
  new bootstrap.Modal(document.getElementById('deleteModal')).show();
}
