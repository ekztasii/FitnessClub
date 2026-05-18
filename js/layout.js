// ── Определяем находимся ли мы в папке pages/ или в корне ────────────────
const isInPages = window.location.pathname.includes('/pages/');
const base = isInPages ? '../' : '';

// ── Генерация общего layout ───────────────────────────────────────────────
function renderLayout(pageTitle, activePage) {
  const navItems = [
    { id: 'dashboard',     icon: '🏠', label: 'Главная',         href: `${base}index.html` },
    { id: 'clients',       icon: '👤', label: 'Клиенты',         href: `${base}pages/clients.html` },
    { id: 'trainers',      icon: '💪', label: 'Тренеры',         href: `${base}pages/trainers.html` },
    { id: 'workouts',      icon: '🗓️',  label: 'Тренировки',     href: `${base}pages/workouts.html` },
    { id: 'workout-types', icon: '📋', label: 'Типы тренировок', href: `${base}pages/workout-types.html` },
    { id: 'registrations', icon: '✅', label: 'Записи',          href: `${base}pages/registrations.html` },
    { id: 'memberships',   icon: '🎫', label: 'Абонементы',      href: `${base}pages/memberships.html` },
    { id: 'plans',         icon: '💳', label: 'Планы',           href: `${base}pages/plans.html` },
  ];

  const navHTML = navItems.map(item => `
    <a href="${item.href}" class="nav-link ${activePage === item.id ? 'active' : ''}">
      <span class="nav-icon">${item.icon}</span>
      <span>${item.label}</span>
    </a>
  `).join('');

  const user = JSON.parse(localStorage.getItem('fc_user') || 'null');
  const userName = user ? user.fullName : 'Пользователь';

  document.body.insertAdjacentHTML('afterbegin', `
    <aside class="sidebar" id="sidebar">
      <div class="sidebar-brand">
        <div class="logo-icon">🏋️</div>
        <div class="brand-name">Атлетика</div>
        <div class="brand-sub">CRM Система</div>
      </div>
      <nav class="sidebar-nav">
        <div class="nav-section-title">Навигация</div>
        ${navHTML}
      </nav>
      <div class="sidebar-footer">
        <div style="color:rgba(255,255,255,.6);font-size:.8rem;margin-bottom:8px">👤 ${userName}</div>
        <a href="${base}login.html" onclick="localStorage.removeItem('fc_user')"
           style="color:rgba(255,100,100,.8);font-size:.75rem;text-decoration:none">🚪 Выйти</a>
        <div style="margin-top:6px">© 2026 FitnessClub CRM</div>
      </div>
    </aside>

    <div class="main-wrapper">
      <header class="topbar">
        <button class="btn btn-sm btn-outline-secondary d-md-none me-2"
          onclick="document.getElementById('sidebar').classList.toggle('open')">☰</button>
        <span class="topbar-title">${pageTitle}</span>
      </header>
      <main class="page-content" id="pageContent"></main>
    </div>

    <div id="toastContainer"></div>

    <div class="modal fade" id="deleteModal" tabindex="-1">
      <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">⚠️ Подтверждение удаления</h5>
            <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
          </div>
          <div class="modal-body">
            <p id="deleteModalMessage" class="mb-0">Вы уверены?</p>
          </div>
          <div class="modal-footer">
            <button class="btn btn-secondary btn-sm" data-bs-dismiss="modal">Отмена</button>
            <button class="btn btn-danger btn-sm" id="confirmDeleteBtn">Удалить</button>
          </div>
        </div>
      </div>
    </div>
  `);
}
