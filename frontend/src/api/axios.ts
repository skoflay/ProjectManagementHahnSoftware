import axios from 'axios';

export const apiClient = axios.create({
  baseURL: 'http://localhost:7033/api',
  headers: {
    'Content-Type': 'application/json'
  }
});
