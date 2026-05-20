// ── Общий модуль валидации форм ──────────────────────────────────────────────

const Validate = {

  // Email: обязателен @, домен и точка
  email(value) {
    const re = /^[^\s@]+@[^\s@]+\.[^\s@]{2,}$/;
    if (!value || !value.trim()) return 'Email обязателен';
    if (!re.test(value.trim())) return 'Введите корректный email (например: user@mail.ru)';
    return null;
  },

  // ФИО: минимум 2 слова, только буквы/пробелы/дефис
  fullName(value) {
    if (!value || !value.trim()) return 'ФИО обязательно';
    if (value.trim().length < 3) return 'ФИО слишком короткое';
    const parts = value.trim().split(/\s+/);
    if (parts.length < 2) return 'Введите имя и фамилию (минимум 2 слова)';
    if (!/^[а-яёА-ЯЁa-zA-Z\s\-]+$/.test(value.trim())) return 'ФИО может содержать только буквы, пробелы и дефис';
    return null;
  },

  // Пароль: минимум 6 символов
  password(value) {
    if (!value) return 'Пароль обязателен';
    if (value.length < 6) return 'Пароль должен содержать не менее 6 символов';
    return null;
  },

  // Подтверждение пароля
  passwordMatch(value, original) {
    if (!value) return 'Повторите пароль';
    if (value !== original) return 'Пароли не совпадают';
    return null;
  },

  // Телефон: +7/8 и 10 цифр (необязательный)
  phone(value) {
    if (!value || !value.trim()) return null; // необязательный
    const digits = value.replace(/\D/g, '');
    if (digits.length < 10 || digits.length > 11) return 'Введите корректный номер телефона (например: +7 999 000 00 00)';
    return null;
  },

  // Обязательное текстовое поле
  required(value, label) {
    if (!value || !value.trim()) return `${label || 'Поле'} обязательно для заполнения`;
    if (value.trim().length < 2) return `${label || 'Поле'} слишком короткое`;
    return null;
  },

  // Число > 0
  positiveNumber(value, label) {
    const n = parseFloat(value);
    if (isNaN(n) || n <= 0) return `${label || 'Значение'} должно быть положительным числом`;
    return null;
  },

  // Целое число > 0
  positiveInt(value, label) {
    const n = parseInt(value);
    if (isNaN(n) || n <= 0 || !Number.isInteger(n)) return `${label || 'Значение'} должно быть целым положительным числом`;
    return null;
  },

  // Дата — не пустая
  date(value, label) {
    if (!value) return `${label || 'Дата'} обязательна`;
    return null;
  },

  // ── Отображение ошибки под полем ────────────────────────────────────────────
  showError(inputEl, message) {
    // Убираем старую ошибку
    this.clearError(inputEl);
    if (!message) return;

    inputEl.style.borderColor = 'var(--red)';
    inputEl.style.boxShadow = '0 0 0 3px var(--red-dim)';

    const err = document.createElement('div');
    err.className = '_val_err';
    err.style.cssText = 'color:var(--red);font-size:11.5px;margin-top:4px;display:flex;align-items:center;gap:4px;';
    err.innerHTML = `<svg width="12" height="12" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><circle cx="12" cy="12" r="10"/><line x1="12" y1="8" x2="12" y2="12"/><line x1="12" y1="16" x2="12.01" y2="16"/></svg>${message}`;
    inputEl.parentNode.appendChild(err);
  },

  clearError(inputEl) {
    inputEl.style.borderColor = '';
    inputEl.style.boxShadow = '';
    const old = inputEl.parentNode.querySelector('._val_err');
    if (old) old.remove();
  },

  showSuccess(inputEl) {
    this.clearError(inputEl);
    inputEl.style.borderColor = 'var(--green)';
    inputEl.style.boxShadow = '0 0 0 3px var(--green-dim)';
  },

  // ── Валидация поля в реальном времени (oninput/onblur) ───────────────────────
  bindLive(inputEl, rulesFn) {
    const validate = () => {
      const err = rulesFn(inputEl.value);
      if (err) this.showError(inputEl, err);
      else this.showSuccess(inputEl);
    };
    inputEl.addEventListener('blur', validate);
    inputEl.addEventListener('input', () => {
      // Убираем ошибку сразу при вводе, показываем только при blur
      if (inputEl.style.borderColor === 'rgb(248, 113, 113)') validate();
    });
  },

  // ── Проверить поле и вернуть true если ок ────────────────────────────────────
  check(inputEl, rulesFn) {
    const err = rulesFn(inputEl.value);
    if (err) { this.showError(inputEl, err); return false; }
    this.showSuccess(inputEl);
    return true;
  }
};
