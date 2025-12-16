import { apiClient } from './axios';
import type { Project } from '../types/project.types';

export const getProjects = async (): Promise<Project[]> => {
  const response = await apiClient.get('/Projects');
  return response.data;
};


