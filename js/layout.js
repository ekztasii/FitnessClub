// SVG icons (inline, no emoji)
const ICONS = {
  home: `<svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><path d="m3 9 9-7 9 7v11a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2z"/><polyline points="9 22 9 12 15 12 15 22"/></svg>`,
  users: `<svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><path d="M17 21v-2a4 4 0 0 0-4-4H5a4 4 0 0 0-4 4v2"/><circle cx="9" cy="7" r="4"/><path d="M23 21v-2a4 4 0 0 0-3-3.87"/><path d="M16 3.13a4 4 0 0 1 0 7.75"/></svg>`,
  trainer: `<svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><circle cx="12" cy="8" r="5"/><path d="M3 21a9 9 0 0 1 18 0"/></svg>`,
  workout: `<svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><rect x="3" y="4" width="18" height="18" rx="2" ry="2"/><line x1="16" y1="2" x2="16" y2="6"/><line x1="8" y1="2" x2="8" y2="6"/><line x1="3" y1="10" x2="21" y2="10"/></svg>`,
  type: `<svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><path d="M4 15s1-1 4-1 5 2 8 2 4-1 4-1V3s-1 1-4 1-5-2-8-2-4 1-4 1z"/><line x1="4" y1="22" x2="4" y2="15"/></svg>`,
  reg: `<svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><path d="M9 11l3 3L22 4"/><path d="M21 12v7a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h11"/></svg>`,
  member: `<svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><rect x="1" y="4" width="22" height="16" rx="2" ry="2"/><line x1="1" y1="10" x2="23" y2="10"/></svg>`,
  plan: `<svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><line x1="8" y1="6" x2="21" y2="6"/><line x1="8" y1="12" x2="21" y2="12"/><line x1="8" y1="18" x2="21" y2="18"/><line x1="3" y1="6" x2="3.01" y2="6"/><line x1="3" y1="12" x2="3.01" y2="12"/><line x1="3" y1="18" x2="3.01" y2="18"/></svg>`,
  analytics: `<svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><line x1="18" y1="20" x2="18" y2="10"/><line x1="12" y1="20" x2="12" y2="4"/><line x1="6" y1="20" x2="6" y2="14"/></svg>`,
  logout: `<svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><path d="M9 21H5a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h4"/><polyline points="16 17 21 12 16 7"/><line x1="21" y1="12" x2="9" y2="12"/></svg>`,
  lock: `<svg width="28" height="28" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.5"><rect x="3" y="11" width="18" height="11" rx="2" ry="2"/><path d="M7 11V7a5 5 0 0 1 10 0v4"/></svg>`,
  logo: `<svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5"><path d="M18 8h1a4 4 0 0 1 0 8h-1"/><path d="M2 8h16v9a4 4 0 0 1-4 4H6a4 4 0 0 1-4-4V8z"/><line x1="6" y1="1" x2="6" y2="4"/><line x1="10" y1="1" x2="10" y2="4"/><line x1="14" y1="1" x2="14" y2="4"/></svg>`,
};

// Navigation config by role
// roleId: 1=Admin, 2=Trainer, 3=Client
function getNavConfig(roleId) {
  const base = [
    { label: 'Главная', icon: 'home', href: 'index.html', key: 'dashboard' }
  ];

  if (roleId === 1) { // Admin — sees everything
    return [
      { section: 'Обзор' },
      { label: 'Главная', icon: 'home', href: 'index.html', key: 'dashboard' },
      { section: 'Управление' },
      { label: 'Клиенты', icon: 'users', href: 'pages/clients.html', key: 'clients' },
      { label: 'Тренеры', icon: 'trainer', href: 'pages/trainers.html', key: 'trainers' },
      { label: 'Тренировки', icon: 'workout', href: 'pages/workouts.html', key: 'workouts' },
      { label: 'Типы тренировок', icon: 'type', href: 'pages/workout-types.html', key: 'workout-types' },
      { section: 'Абонементы' },
      { label: 'Записи', icon: 'reg', href: 'pages/registrations.html', key: 'registrations' },
      { label: 'Абонементы', icon: 'member', href: 'pages/memberships.html', key: 'memberships' },
      { label: 'Планы', icon: 'plan', href: 'pages/plans.html', key: 'plans' },
      { section: 'Аналитика' },
      { label: 'Финансы и отчёты', icon: 'analytics', href: 'pages/analytics.html', key: 'analytics' },
    ];
  }

  if (roleId === 2) { // Trainer — sees workouts and registrations only
    return [
      { section: 'Обзор' },
      { label: 'Главная', icon: 'home', href: 'index.html', key: 'dashboard' },
      { section: 'Моя работа' },
      { label: 'Тренировки', icon: 'workout', href: 'pages/workouts.html', key: 'workouts' },
      { label: 'Типы тренировок', icon: 'type', href: 'pages/workout-types.html', key: 'workout-types' },
      { label: 'Записи на тренировки', icon: 'reg', href: 'pages/registrations.html', key: 'registrations' },
    ];
  }

  if (roleId === 3) { // Client — sees their own workouts and memberships
    return [
      { section: 'Обзор' },
      { label: 'Главная', icon: 'home', href: 'index.html', key: 'dashboard' },
      { section: 'Мои данные' },
      { label: 'Тренировки', icon: 'workout', href: 'pages/workouts.html', key: 'workouts' },
      { label: 'Мои записи', icon: 'reg', href: 'pages/registrations.html', key: 'registrations' },
      { label: 'Мой абонемент', icon: 'member', href: 'pages/memberships.html', key: 'memberships' },
    ];
  }

  return base;
}

function getRoleName(roleId) {
  return { 1: 'Администратор', 2: 'Тренер', 3: 'Клиент' }[roleId] || 'Пользователь';
}

function getRolePillClass(roleId) {
  return { 1: 'admin', 2: 'trainer', 3: 'client' }[roleId] || 'client';
}

function getInitials(name) {
  if (!name) return '?';
  const parts = name.trim().split(' ');
  return parts.length >= 2 ? (parts[0][0] + parts[1][0]).toUpperCase() : name[0].toUpperCase();
}

// Detect if we're in pages/ subfolder
function isInPages() {
  return window.location.pathname.includes('/pages/');
}

function resolveHref(href) {
  if (isInPages()) {
    if (href === 'index.html') return '../index.html';
    if (href.startsWith('pages/')) return href.replace('pages/', '');
    return href;
  }
  return href;
}

function renderLayout(pageTitle, activeKey) {
  const user = getCurrentUser();
  if (!user) {
    window.location.href = isInPages() ? '../login.html' : 'login.html';
    return;
  }

  const roleId = user.roleId;
  const navItems = getNavConfig(roleId);
  const roleName = getRoleName(roleId);
  const pillClass = getRolePillClass(roleId);

  let navHtml = '';
  for (const item of navItems) {
    if (item.section) {
      navHtml += `<div class="nav-section">${item.section}</div>`;
    } else {
      const active = item.key === activeKey ? 'active' : '';
      navHtml += `<a class="nav-item ${active}" href="${resolveHref(item.href)}">${ICONS[item.icon]} ${item.label}</a>`;
    }
  }

  const loginHref = isInPages() ? '../login.html' : 'login.html';

  document.body.innerHTML = `
    <div class="app-layout">
      <aside class="sidebar">
        <div class="sidebar-brand">
          <div class="brand-mark">${ICONS.logo}</div>
          <div>
            <div class="brand-name">Атлетика</div>
            <div class="brand-sub">CRM Система</div>
          </div>
        </div>

        <div class="role-pill ${pillClass}">
          <span class="role-dot"></span>
          ${roleName}
        </div>

        <nav class="sidebar-nav">${navHtml}</nav>

        <div class="sidebar-footer">
          <div class="user-block">
            <div class="user-ava">${getInitials(user.fullName)}</div>
            <div>
              <div class="user-name-sm">${user.fullName}</div>
              <div class="user-role-sm">${user.email}</div>
            </div>
          </div>
          <button class="btn-logout" onclick="doLogout()">
            ${ICONS.logout} Выйти
          </button>
        </div>
      </aside>

      <main class="main-content" id="pageContent"></main>
    </div>
  `;

  // Set page title
  document.title = pageTitle + ' — Атлетика CRM';

  window.doLogout = function() {
    localStorage.removeItem('fc_user');
    window.location.href = loginHref;
  };
}

// Access guard — call at top of page with required role IDs
// e.g. requireRole([1]) means admin only
function requireRole(allowedRoles) {
  const user = getCurrentUser();
  if (!user) {
    window.location.href = isInPages() ? '../login.html' : 'login.html';
    return false;
  }
  if (!allowedRoles.includes(user.roleId)) {
    const content = document.getElementById('pageContent');
    if (content) {
      content.innerHTML = `
        <div class="access-denied">
          <div class="access-denied-icon">${ICONS.lock}</div>
          <h3>Нет доступа</h3>
          <p>У вас нет прав для просмотра этого раздела.</p>
        </div>
      `;
    }
    return false;
  }
  return true;
}

// Modal helper
function openModal(id) {
  document.getElementById(id).classList.add('open');
}

function closeModal(id) {
  document.getElementById(id).classList.remove('open');
}
