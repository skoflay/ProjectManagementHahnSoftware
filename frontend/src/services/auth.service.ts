import { apiClient } from '../api/axios';

const TOKEN_KEY = 'token';

export const AuthService = {
  login: async (email: string, password: string) => {
    const res = await apiClient.post('/auth/login', {
      email,
      password,
    });

    const { token, user } = res.data;

    localStorage.setItem(TOKEN_KEY, token);
    if (user) {
      localStorage.setItem('user', JSON.stringify(user));
    }

    return res.data;
  },

  logout: () => {
    localStorage.removeItem(TOKEN_KEY);
    localStorage.removeItem('user');
  },

  getCurrentUser: async () => {
    const res = await apiClient.get('/auth/me');
    return res.data;
  },

  getToken: () => localStorage.getItem(TOKEN_KEY),
  
isAuthenticated: () => Boolean(localStorage.getItem(TOKEN_KEY)),
  
};
