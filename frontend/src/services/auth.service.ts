const TOKEN_KEY = 'token';

export const AuthService = {
  login: async (email: string, password: string) => {
    const res = await fetch('http://localhost:7033/api/auth/login', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ email, password }),
    });

    if (!res.ok) throw new Error('Login failed');

    const data = await res.json();
    localStorage.setItem(TOKEN_KEY, data.token);
    return data; 
  },

  logout: () => {
    localStorage.removeItem(TOKEN_KEY);
    localStorage.removeItem('user');
  },

  getCurrentUser: async (token: string) => {
    const res = await fetch('/api/auth/me', {
      headers: { Authorization: `Bearer ${token}` },
    });
    if (!res.ok) throw new Error('Failed to get user info');
    return res.json(); 
  },

  getToken: () => localStorage.getItem(TOKEN_KEY),
  isAuthenticated: () => !!localStorage.getItem(TOKEN_KEY),
};
