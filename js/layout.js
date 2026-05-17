// ── Генерация общего layout (sidebar + topbar + модальные окна) ───────────
function renderLayout(pageTitle, activePage) {
  const navItems = [
    { id: 'dashboard',     icon: '🏠', label: 'Главная',        href: '../index.html' },
    { id: 'clients',       icon: '👤', label: 'Клиенты',        href: 'clients.html' },
    { id: 'trainers',      icon: '💪', label: 'Тренеры',        href: 'trainers.html' },
    { id: 'workouts',      icon: '🗓️',  label: 'Тренировки',    href: 'workouts.html' },
    { id: 'workout-types', icon: '📋', label: 'Типы тренировок',href: 'workout-types.html' },
    { id: 'registrations', icon: '✅', label: 'Записи',         href: 'registrations.html' },
    { id: 'memberships',   icon: '🎫', label: 'Абонементы',     href: 'memberships.html' },
    { id: 'plans',         icon: '💳', label: 'Планы',          href: 'plans.html' },
  ];

  const navHTML = navItems.map(item => `
    <a href="${item.href}" class="nav-link ${activePage === item.id ? 'active' : ''}">
      <span class="nav-icon">${item.icon}</span>
      <span>${item.label}</span>
    </a>
  `).join('');

  document.body.insertAdjacentHTML('afterbegin', `
    <!-- Sidebar -->
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
      <div class="sidebar-footer">© 2026 FitnessClub CRM</div>
    </aside>

    <!-- Main -->
    <div class="main-wrapper">
      <header class="topbar">
        <button class="btn btn-sm btn-outline-secondary d-md-none me-2" onclick="document.getElementById('sidebar').classList.toggle('open')">☰</button>
        <span class="topbar-title">${pageTitle}</span>
      </header>
      <main class="page-content" id="pageContent"></main>
    </div>

    <!-- Toast контейнер -->
    <div id="toastContainer"></div>

    <!-- Модалка подтверждения удаления -->
    <div class="modal fade" id="deleteModal" tabindex="-1">
      <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">⚠️ Подтверждение удаления</h5>
            <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
          </div>
          <div class="modal-body">
            <p id="deleteModalMessage" class="mb-0">Вы уверены что хотите удалить запись?</p>
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
